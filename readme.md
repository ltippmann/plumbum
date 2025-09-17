# Introduction
Plumbum is an implementation of the result pattern and a collection of extension methods for chaining programming logic in a concise way.  The main problem it solves is removing the cruft of checking whether some potentially fallible operation succeeded or not from the business logic.  The result can be fairly terse code that consists almost entirely of pure business logic.

I have written several iterations of this library for various jobs I've held.  I decided to open source the library mainly so I wouldn't have to rewrite it from scratch every time I changed employers.

### About the name
Plumbum is the Latin word for lead, as in the chemical element Pb.  This is the root of the word plumbing, as in ancient Rome, pipes were made of lead.  The library is used to plumb the flow of business logic, mainly using the overloads of the extension methods Pipe and Trap.

# Concepts

## Result Pattern
The main unit of work is the `ILogicResult` and `ILogicResult<T>` interfaces.  The `ILogicResult` interface represents the result of some activity that may have succeeded or failed. You do not implement this interface yourself, rather you rely on the static methods of the `LogicResult` class to create instances.  `ILogicResult` has a `Success` parameter and `ILogicResult<T>` has a value parameter.

If an `ILogicResult` is successful, it will also be an `ILogicSuccess` while if it was not successful, it will be an `ILogicError`.  An `ILogicError` and `ILogicError<T>` have `ErrorCode`, `ErrorMessage` and `EntityType` properties, of which, `ErrorCode` is the only required property.

The same pattern applies to `ILogicResult<T>`.  A successful `ILogicResult<T>` will be an `ILogicSuccess<T>` and an unsuccessful one will be an `ILogicError<T>`.

Additionally there are `IUnhandledLogicError` and `IUnhandledLogicError<T>` interfaces which wrap unhandled exceptions.

### Interface Hierarchy
```
ILogicResult
 ├── ILogicSuccess ◄─────────────────┐
 ├── ILogicError ◄──────────────────┐│
 ├── IUnhandledLogicError ◄────────┐││
 └── ILogicResult<T>               │││
     ├── IUnhandledLogicError<T> ──┘││
     ├── ILogicError<T> ────────────┘│
     └── ILogicSuccess<T> ───────────┘
```

## Static Methods
The `LogicResult` static class has number of methods to create different flavors of `ILogicResult`s.
* `Success()` Creates a successful `ILogicResult` without a value.
* `Success<T>(T value)` Creates a successful `ILogicResult<T>` with a value.
* `Error()` Creates an unsuccessful `ILogicResult` with a default error code of "UNKNOWN".
* `Error<T>()` Creates an unsuccessful `ILogicResult` that would have had a value of type `T` had it succeeded with a default error code of "UNKNOWN".

Additionally there are some static methods to create specific types `ILogicErrors`.  Except for the "Unhandled" flavors, these only differ by the value set for `ErrorCode` property.
* `NotFound()` Creates an `ILogicError` with a `ErrorCode` of "NOT_FOUND".
* `NotFound<T>()` Creates an `ILogicError<T>` with a `ErrorCode` of "NOT_FOUND" and an `EntityType` of `typeof(T).Name`.
* `NotValid()` Creates an `ILogicError` with a `ErrorCode` of "NOT_VALID".
* `NotValid<T>()` Creates an `ILogicError<T>` with a `ErrorCode` of "NOT_VALID" and an `EntityType` of `typeof(T).Name`.
* `Unhandled(Exception? ex)` Creates an `IUnhandledLogicError` with a `ErrorCode` of "UNHANDLED".
* `NotValid<T>(Exception? ex)` Creates an `IUnhandledLogicError<T>` with a `ErrorCode` of "UNHANDLED" and an `EntityType` of `typeof(T).Name`.

Finally there are the `Try` static methods to execute code that might throw an exception and convert the result to an `ILogicResult` or `ILogicResult<T>` that can be coded against with the provided extension methods.  These come in sync and async flavors, that return a value or not.  You have the option to add your own exception handler to convert an exception to an `ILogicResult` or you can let the default handler convert any exceptions to an `IUnhandledLogicResult`.

It is often useful to statically import the `LogicResult` static class so you can use the methods it contains without having to specify the `LogicResult.` to qualify the method.  This ends up looking like this:

```
using static Plumbum.LogicResult;

...

Success("some value")
    .Pipe(value => ...);

Error<string>()
    .Pipe(err => ...);
```

## Extension Methods
The two main extension methods are `Pipe` for executing code on success and `Trap` for executing code on failure.  Both come in multiple flavors for dealing with sync and async inputs, with or without a value and sync or async lambdas, with or without a value.  The overloads make it so you mostly don't have to think too much about what flavor the preceding chunk of logic came in.  You can Pipe a `Task<ILogicResult>` to an `ILogicResult<T>` and the end result will be a `Task<ILogicResult<T>>`.  

These extensions are strongly typed and the outputs will match the inputs.  A `Pipe` can change the output type while a `Trap` cannot.  A chain of `Pipes` will execute consecutively as long the previous call was successful.  If a `Pipe` is called against an `ILogicError`, the code in the `Pipe` lambda will not be executed and the returned `ILogicError` will be transmuted into the corresponding flavor of `ILogicResult` that would be returned by the `Pipe`, e.g.  `Error().Pipe(value => value + "foo")` would have a final type of `ILogicResult<string>`.

# Example
The person service has a method to get a Person model with the hierarchy of children underneath fully loaded.  Success will only be returned if the full hierarchy was loaded successfully.  If for example, the repository returned a "NOT_FOUND" error because the identifier did not match any known person, the call to load the children will not be made and the "NOT_FOUND" error will bubble up to the caller.

``` csharp
public class PersonService
{
    private IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    // Attempt to load a person model and recursively load all of it's children.  Log success or failure.
    public async Task<ILogicResult<Person>> GetPerson(string personIdentifier)
        => await _personRepository.GetPerson(personIdentifier)
            .Pipe(person => LoadChildren(person))
            .Pipe(person => LogPersonLoadedSuccessfully(person))
            .Trap(err => LogPersonLoadingError(err));

    private async Task<ILogicResult<Person>> LoadChildren(Person parent)
    {
        // Recursively loop through the ChildIdentifiers attempting to load the corresponding Person
        //  model.  Only if the child Person model can be loaded successfully, it will be added to the
        //  Children collection.  If an error occurs trying to load child, return a "CHILD_NOT_LOADED"
        //  error as the result of the whole operation.
        foreach(var childIdentifier in parent.ChildIdentifiers)
        {
            var childResult = await GetPerson(personIdentifier)
                .Pipe(child => parent.Children.Add(child));
            if(!childResult.Success)
                return Error<Person>("CHILD_NOT_LOADED", "A Child record could not be loaded.");
        }
        return Success(parent);
    }

    private void LogPersonLoadedSuccessfully(Person person)
        => Log.Info($"Person {person.Name} loaded from identifier {personIdentifier}.");

    private void LogPersonLoadingError(ILogicError err)
        => Log.Error($"Person with identifier \"{personIdentifier}\" could not be loaded: {err.ErrorMessage}");
}
```

Note how the private methods are named such that logic in the public `GetPerson` method reads as an almost plain english description of what the method is doing.  Structuring the code in this way makes it easy to understand the business logic.  It also ends up being a useful tool for enumerating the permutations that need to be unit tested.

# License
This project is licensed under the MIT license which allows you to use, modify and distribute this code and things you build with this code or library as long as you include the LICENSE.txt with your distribution.