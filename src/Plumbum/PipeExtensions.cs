using static Plumbum.LogicResult;
using static Plumbum.Exceptions.NullChecks;

namespace Plumbum;

/// <summary>
/// <see cref="Pipe(ILogicResult, Action)">Pipe</see> and <see cref="Trap(ILogicResult, Action{ILogicError})">Trap</see> extension methods for <see cref="ILogicResult">ILogicResults</see>.
/// </summary>
public static partial class PipeExtensions
{
    // Handle type conversion of error states

    private static ILogicResult<T> ForwardError<T>(ILogicResult result)
        => result is ILogicResult<T> result2
            ? result2 
            : result is not IUnhandledLogicError
            ? new LogicError<T> 
            { 
                ErrorCode = ((ILogicError)result).ErrorCode,
                EntityType = ((ILogicError)result).EntityType,
                ErrorMessage = ((ILogicError)result).ErrorMessage 
            }
            : new UnhandledLogicError<T> 
            { 
                ErrorCode = ((ILogicError)result).ErrorCode,
                EntityType = ((ILogicError)result).EntityType,
                ErrorMessage = ((ILogicError)result).ErrorMessage, 
                Exception = ((IUnhandledLogicError)result).Exception 
            };

    // void => void

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static ILogicResult Pipe(this ILogicResult input, Action activity)
    {
        if (Check(input).Success) Check(activity)();
        return input;
    }

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Pipe(this ILogicResult input, Func<Task> activity)
    {
        if (Check(input).Success) await Check(activity)();
        return input;
    }

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed.</param>
    /// <param name="activity">A function to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Pipe(this Task<ILogicResult> input, Action activity)
        => Pipe(await Check(input), activity);


    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Pipe(this Task<ILogicResult> input, Func<Task> activity)
        => await Pipe(await Check(input), activity);

    // void => ILogicResult

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static ILogicResult Pipe(this ILogicResult input, Func<ILogicResult> activity)
        => Check(input).Success
            ? Check(activity)()
            : input;

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult> Pipe(this ILogicResult input, Func<Task<ILogicResult>> activity)
        => Check(input).Success
            ? await Check(activity)()
            : input;

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult> Pipe(this Task<ILogicResult> input, Func<ILogicResult> activity)
        => Pipe(await Check(input), activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult> Pipe(this Task<ILogicResult> input, Func<Task<ILogicResult>> activity)
        => await Pipe(await Check(input), activity);


    // T => void

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function to execute against a value of type <typeparamref name="T"/> only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding activity.</returns>
    public static ILogicResult<T> Pipe<T>(this ILogicResult<T> input, Action<T> activity)
    {
        if (Check(input).Success)
            Check(activity)(((ILogicSuccess<T>)input).Value);
        return input;
    }

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function to execute against a value of type <typeparamref name="T"/> only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult<T>> Pipe<T>(this ILogicResult<T> input, Func<T, Task> activity)
    {
        if (Check(input).Success) 
            await Check(activity)(((ILogicSuccess<T>)input).Value);
        return input;
    }

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function to execute against a value of type <typeparamref name="T"/> only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult<T>> Pipe<T>(this Task<ILogicResult<T>> input, Action<T> activity)
        => Pipe(await Check(input), activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function to execute against a value of type <typeparamref name="T"/> only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult<T>> Pipe<T>(this Task<ILogicResult<T>> input, Func<T, Task> activity)
        => await Pipe(await Check(input), activity);

    // T => ILogicResult

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function to execute against a value of type <typeparamref name="T"/> only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static ILogicResult Pipe<T>(this ILogicResult<T> input, Func<T, ILogicResult> activity)
        => Check(input).Success 
            ? Check(activity)(((ILogicSuccess<T>)input).Value)
            : input;

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function to execute against a value of type <typeparamref name="T"/> only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult> Pipe<T>(this ILogicResult<T> input, Func<T, Task<ILogicResult>> activity)
        => Check(input).Success
            ? await Check(activity)(((ILogicSuccess<T>)input).Value)
            : input;

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function to execute against a value of type <typeparamref name="T"/> only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult> Pipe<T>(this Task<ILogicResult<T>> input, Func<T, ILogicResult> activity)
        => Pipe(await Check(input), activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function to execute against a value of type <typeparamref name="T"/> only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult> Pipe<T>(this Task<ILogicResult<T>> input, Func<T, Task<ILogicResult>> activity)
        => await Pipe(await Check(input), activity);

    // void => R

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function that produces a value of type <typeparamref name="R"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static ILogicResult<R> Pipe<R>(this ILogicResult input, Func<R> activity)
        => Check(input).Success
            ? Success(Check(activity)())
            : ForwardError<R>(input);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function that produces a value of type <typeparamref name="R"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<R>(this ILogicResult input, Func<Task<R>> activity)
        => Check(input).Success
            ? Success(await Check(activity)())
            : ForwardError<R>(input);

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail to produce a value only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed.</param>
    /// <param name="activity">A function that produces a value of type <typeparamref name="R"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<R>(this Task<ILogicResult> input, Func<R> activity)
        => Pipe(await Check(input), activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail to produce a value only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function that produces a value of type <typeparamref name="R"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<R>(this Task<ILogicResult> input, Func<Task<R>> activity)
        => await Pipe(await Check(input), activity);

    // void => ILogicResult<R>

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function that produces a value of type <typeparamref name="R"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static ILogicResult<R> Pipe<R>(this ILogicResult input, Func<ILogicResult<R>> activity)
        => Check(input).Success
            ? Check(activity)()
            : ForwardError<R>(input);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function that produces a value of type <typeparamref name="R"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<R>(this ILogicResult input, Func<Task<ILogicResult<R>>> activity)
        => Check(input).Success
            ? await Check(activity)()
            : ForwardError<R>(input);

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function that produces a value of type <typeparamref name="R"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<R>(this Task<ILogicResult> input, Func<ILogicResult<R>> activity)
        => Pipe(await Check(input), activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function that produces a value of type <typeparamref name="R"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<R>(this Task<ILogicResult> input, Func<Task<ILogicResult<R>>> activity)
        => await Pipe(await Check(input), activity);

    // T => R

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function that produces a value of type <typeparamref name="R"/> from a value of type <typeparamref name="T"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static ILogicResult<R> Pipe<T, R>(this ILogicResult<T> input, Func<T, R> activity)
    {
        var input2 = Check(input);
        return input2.Success
            ? Success(Check(activity)(input2.Value))
            : ForwardError<R>(input2);
    }

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function that produces a value of type <typeparamref name="R"/> from a value of type <typeparamref name="T"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<T, R>(this ILogicResult<T> input, Func<T, Task<R>> activity)
    {
        var input2 = Check(input);
        return input2.Success
            ? Success(await Check(activity)(input2.Value))
            : ForwardError<R>(input2);
    }

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail to produce a value only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function that produces a value of type <typeparamref name="R"/> from a value of type <typeparamref name="T"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<T, R>(this Task<ILogicResult<T>> input, Func<T, R> activity)
        => Pipe(await Check(input), activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail to produce a value only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function that produces a value of type <typeparamref name="R"/> from a value of type <typeparamref name="T"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<T, R>(this Task<ILogicResult<T>> input, Func<T, Task<R>> activity)
        => await Pipe(await Check(input), activity);

    // T => ILogicResult<R>

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function that produces a value of type <typeparamref name="R"/> from a value of type <typeparamref name="T"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static ILogicResult<R> Pipe<T, R>(this ILogicResult<T> input, Func<T, ILogicResult<R>> activity)
    {
        var input2 = Check(input);
        return input2.Success
            ? Check(activity)(input2.Value)
            : ForwardError<R>(input2);
    }

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail to produce a value only if the <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function that produces a value of type <typeparamref name="R"/> from a value of type <typeparamref name="T"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<T, R>(this ILogicResult<T> input, Func<T, Task<ILogicResult<R>>> activity)
    {
        var input2 = Check(input);
        return input2.Success
            ? await Check(activity)(input2.Value)
            : ForwardError<R>(input2);
    }

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail to produce a value only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function that produces a value of type <typeparamref name="R"/> from a value of type <typeparamref name="T"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<T, R>(this Task<ILogicResult<T>> input, Func<T, ILogicResult<R>> activity)
        => Pipe(await Check(input), activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail to produce a value only if the async <paramref name="input"/> is successful.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="R">The type of the output value.</typeparam>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function that produces a value of type <typeparamref name="R"/> from a value of type <typeparamref name="T"/> to execute only if <paramref name="input"/> is successful.</param>
    /// <returns>An <see cref="ILogicResult{R}"/> that represents the result of the preceding and this <paramref name="activity"/>.</returns>
    public static async Task<ILogicResult<R>> Pipe<T, R>(this Task<ILogicResult<T>> input, Func<T, Task<ILogicResult<R>>> activity)
        => await Pipe(await Check(input), activity);
}
