using static Plumbum.Exceptions.NullChecks;

namespace Plumbum;

/// <summary>
/// Static functions that produce various flavors of <see cref="ILogicResult"/>.
/// </summary>
public static class LogicResult
{
    /// <summary>
    /// A convenience method to make an <see cref="ILogicResult"/> async by wrapping it in a <see cref="Task.FromResult{TResult}(TResult)">Task.FromResult</see>.
    /// </summary>
    /// <param name="input">The <see cref="ILogicResult"/> to be wrapped.</param>
    public static Task<ILogicResult> Async(this ILogicResult input)
        => Task.FromResult(Check(input));

    /// <summary>
    /// A convenience method to make an <see cref="ILogicResult{T}"/> async by wrapping it in a <see cref="Task.FromResult{TResult}(TResult)">Task.FromResult</see>.
    /// </summary>
    /// <typeparam name="T">The type of value the <see cref="ILogicResult{T}"/> presents.</typeparam>
    /// <param name="input">The <see cref="ILogicResult{T}"/> to be wrapped.</param>
    public static Task<ILogicResult<T>> Async<T>(this ILogicResult<T> input)
        => Task.FromResult(Check(input));

    // Success

    /// <summary>
    /// Return a new <see cref="ILogicSuccess"/>.
    /// </summary>
    /// <returns>A successful <see cref="ILogicResult"/> without a value.</returns>
    public static ILogicResult Success() => new LogicSuccess();

    /// <summary>
    /// Return a new <see cref="ILogicSuccess{T}"/> with a value of <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of value the <see cref="ILogicResult{T}"/> presents.</typeparam>
    /// <param name="value">The value the <see cref="ILogicSuccess{T}"/> presents.</param>
    /// <returns>A successful <see cref="ILogicResult{T}"/> with a value of <paramref name="value"/>.</returns>
    public static ILogicResult<T> Success<T>(T value) => new LogicSuccess<T> { Value = value };


    // Errors

    /// <summary>
    /// Returns a new <see cref="ILogicError"/>.
    /// </summary>
    /// <param name="errorCode">A string that identifies the type of error.</param>
    /// <param name="errorMessage">An optional human readable description of the error.</param>
    /// <param name="entityType">An optional name of the entity type the error relates to.</param>
    /// <returns>A new instance of <see cref="ILogicError"/>.</returns>
    public static ILogicResult Error(string errorCode = ErrorCodes.Unknown, string? errorMessage = null, string? entityType = null)
        => new LogicError { ErrorCode = errorCode, EntityType = entityType, ErrorMessage = errorMessage };

    /// <summary>
    /// Returns a new <see cref="ILogicError{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of value the <see cref="ILogicResult{T}"/> would have if it were not an error.</typeparam>
    /// <param name="errorCode">A string that identifies the type of error.</param>
    /// <param name="errorMessage">An optional human readable description of the error.</param>
    /// <param name="entityType">An optional name of the entity type the error relates to. Defaults to <see langword="typeof"/>(<typeparamref name="T"/>).<see cref="System.Reflection.MemberInfo.Name">Name</see> if not specified.</param>
    /// <returns>A new instance of <see cref="ILogicError{T}"/>.</returns>
    public static ILogicResult<T> Error<T>(string errorCode = ErrorCodes.Unknown, string? errorMessage = null, string? entityType = null)
        => new LogicError<T> { ErrorCode = errorCode, EntityType = entityType ?? typeof(T).Name, ErrorMessage = errorMessage };


    /// <summary>
    /// Returns a new <see cref="IUnhandledLogicError"/>.
    /// </summary>
    /// <param name="errorCode">A string that identifies the type of error. Defaults to <see cref="ErrorCodes.Unhandled"/>.</param>
    /// <param name="errorMessage">An optional human readable description of the error.</param>
    /// <param name="entityType">An optional name of the entity type the error relates to.</param>
    /// <param name="exception">An optional <see cref="Exception"/> that caused this <see cref="IUnhandledLogicError"/>.</param>
    /// <returns>A new instance of <see cref="IUnhandledLogicError"/>.</returns>
    public static ILogicResult Unhandled(string errorCode = ErrorCodes.Unhandled, string? errorMessage = null, string? entityType = null, Exception? exception = null)
        => new UnhandledLogicError { ErrorCode = errorCode, EntityType = entityType, ErrorMessage = errorMessage, Exception = exception };

    /// <summary>
    /// Returns a new <see cref="IUnhandledLogicError"/>.
    /// </summary>
    /// <typeparam name="T">The type of value the <see cref="ILogicResult{T}"/> would have if it were not an error.</typeparam>
    /// <param name="errorCode">A string that identifies the type of error. Defaults to <see cref="ErrorCodes.Unhandled"/>.</param>
    /// <param name="errorMessage">An optional human readable description of the error.</param>
    /// <param name="entityType">An optional name of the entity type the error relates to. Defaults to <see langword="typeof"/>(<typeparamref name="T"/>).<see cref="System.Reflection.MemberInfo.Name">Name</see> if not specified.</param>
    /// <param name="exception">An optional <see cref="Exception"/> that caused this <see cref="IUnhandledLogicError{T}"/>.</param>
    /// <returns>A new instance of <see cref="IUnhandledLogicError{T}"/>.</returns>
    public static ILogicResult<T> Unhandled<T>(string errorCode = ErrorCodes.Unhandled, string? errorMessage = null, string? entityType = null, Exception? exception = null)
        => new UnhandledLogicError<T> { ErrorCode = errorCode, EntityType = entityType ?? typeof(T).Name, ErrorMessage = errorMessage, Exception = exception };

    /// <summary>
    /// Returns a new <see cref="IUnhandledLogicError"/> that gets is values from <paramref name="exception"/>.
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/> that caused this <see cref="IUnhandledLogicError"/>.</param>
    /// <returns>A new instance of <see cref="IUnhandledLogicError"/>.</returns>
    /// <remarks>
    /// The resulting <see cref="IUnhandledLogicError"/> will have it's values set from <paramref name="exception"/>.
    /// <list type="bullet">
    /// <item><see cref="ILogicError.ErrorCode">ErrorCode</see> will be <see cref="ErrorCodes.Unhandled">Unhandled</see>.</item>
    /// <item><see cref="ILogicError.ErrorMessage">ErrorMessage</see> will be <paramref name="exception"/>.<see cref="Exception.Message">Message</see>.</item>
    /// <item><see cref="ILogicError.EntityType">EntityType</see> will be <see langword="null"/>.</item>
    /// </list>
    /// </remarks>
    public static ILogicResult Wrap(this Exception exception)
        => Unhandled(errorMessage: exception.Message, exception: exception);

    /// <summary>
    /// Returns a new <see cref="IUnhandledLogicError{T}"/> that gets is values from <paramref name="exception"/>.
    /// </summary>
    /// <typeparam name="T">The type of value the <see cref="ILogicResult{T}"/> would have if it were not an error.</typeparam>
    /// <param name="exception">The <see cref="Exception"/> that caused this <see cref="IUnhandledLogicError"/>.</param>
    /// <returns>A new instance of <see cref="IUnhandledLogicError"/>.</returns>
    /// <remarks>
    /// The resulting <see cref="IUnhandledLogicError"/> will have it's values set from <paramref name="exception"/>.
    /// <list type="bullet">
    /// <item><see cref="ILogicError.ErrorCode">ErrorCode</see> will be <see cref="ErrorCodes.Unhandled">Unhandled</see>.</item>
    /// <item><see cref="ILogicError.ErrorMessage">ErrorMessage</see> will be <paramref name="exception"/>.<see cref="Exception.Message">Message</see>.</item>
    /// <item><see cref="ILogicError.EntityType">EntityType</see> will be <see langword="null"/>.</item>
    /// </list>
    /// </remarks>
    public static ILogicResult<T> Wrap<T>(this Exception exception)
        => Unhandled<T>(errorMessage: exception.Message, entityType: typeof(T).Name, exception: exception);

    /// <summary>
    /// Returns a new <see cref="ILogicError"/> with an <see cref="ILogicError.ErrorCode">ErrorCode</see> of <see cref="ErrorCodes.NotFound"/>.
    /// </summary>
    /// <param name="errorMessage">An optional human readable description of the error.</param>
    /// <param name="entityType">An optional name of the entity type the error relates to.</param>
    /// <returns>A new instance of <see cref="ILogicError"/>.</returns>
    public static ILogicResult NotFound(string? errorMessage = null, string? entityType = null)
        => Error(ErrorCodes.NotFound, errorMessage, entityType);

    /// <summary>
    /// Returns a new <see cref="ILogicError{T}"/> with an <see cref="ILogicError.ErrorCode">ErrorCode</see> of <see cref="ErrorCodes.NotFound"/>.
    /// </summary>
    /// <param name="errorMessage">An optional human readable description of the error.</param>
    /// <param name="entityType">An optional name of the entity type the error relates to. Defaults to <see langword="typeof"/>(<typeparamref name="T"/>).<see cref="System.Reflection.MemberInfo.Name">Name</see> if not specified.</param>
    /// <returns>A new instance of <see cref="ILogicError{T}"/>.</returns>
    public static ILogicResult<T> NotFound<T>(string? errorMessage = null, string? entityType = null)
        => Error<T>(ErrorCodes.NotFound, errorMessage, entityType ?? typeof(T).Name);


    /// <summary>
    /// Returns a new <see cref="ILogicError"/> with an <see cref="ILogicError.ErrorCode">ErrorCode</see> of <see cref="ErrorCodes.NotValid"/>.
    /// </summary>
    /// <param name="errorMessage">An optional human readable description of the error.</param>
    /// <param name="entityType">An optional name of the entity type the error relates to.</param>
    /// <returns>A new instance of <see cref="ILogicError"/>.</returns>
    public static ILogicResult NotValid(string? errorMessage = null, string? entityType = null)
        => Error(ErrorCodes.NotValid, errorMessage, entityType);

    /// <summary>
    /// Returns a new <see cref="ILogicError{T}"/> with an <see cref="ILogicError.ErrorCode">ErrorCode</see> of <see cref="ErrorCodes.NotValid"/>.
    /// </summary>
    /// <param name="errorMessage">An optional human readable description of the error.</param>
    /// <param name="entityType">An optional name of the entity type the error relates to. Defaults to <see langword="typeof"/>(<typeparamref name="T"/>).<see cref="System.Reflection.MemberInfo.Name">Name</see> if not specified.</param>
    /// <returns>A new instance of <see cref="ILogicError{T}"/>.</returns>
    public static ILogicResult<T> NotValid<T>(string? errorMessage = null, string? entityType = null)
        => Error<T>(ErrorCodes.NotValid, errorMessage, entityType ?? typeof(T).Name);


    // Try methods

    // void => void

    /// <summary>
    /// Attempt to perform an <paramref name="activity"/> that does not return a value. 
    /// If an exception is thrown, it will be <see cref="Wrap">Wrapped</see> by an <see cref="IUnhandledLogicError"/>, otherwise <see cref="ILogicSuccess"/> is returned.
    /// </summary>
    /// <param name="activity">A function that does not return a value that may throw an exception.</param>
    /// <returns>Either an <see cref="ILogicSuccess"/> or an <see cref="IUnhandledLogicError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static ILogicResult Try(Action activity)
        => Try(activity, Wrap);

    /// <summary>
    /// Attempt to perform an <paramref name="activity"/> that does not return a value. 
    /// If an exception is thrown, it will be passed to <paramref name="handleError"/>.
    /// </summary>
    /// <param name="activity">A function that does not return a value that may throw an exception.</param>
    /// <param name="handleError">A function that converts an <see cref="Exception"/> to an <see cref="ILogicResult"/>.</param>
    /// <returns>Either an <see cref="ILogicSuccess"/> or the result of <paramref name="handleError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static ILogicResult Try(Action activity, Func<Exception, ILogicResult> handleError)
    {
        Check(activity);
        Check(handleError);

        try
        {
            activity();
            return Success();
        }
        catch (Exception ex)
        {
            return handleError(ex);
        }
    }

    /// <summary>
    /// Attempt to perform an <paramref name="activity"/> that does not return a value. 
    /// If an exception is thrown, it will be passed to <paramref name="handleError"/>.
    /// </summary>
    /// <param name="activity">A function that does not return a value that may throw an exception.</param>
    /// <param name="handleError">An async function that converts an <see cref="Exception"/> to an <see cref="ILogicResult"/>.</param>
    /// <returns>Either an <see cref="ILogicSuccess"/> or the result of <paramref name="handleError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static async Task <ILogicResult> Try(Action activity, Func<Exception, Task<ILogicResult>> handleError)
    {
        Check(activity);
        Check(handleError);

        try
        {
            activity();
            return Success();
        }
        catch (Exception ex)
        {
            return await handleError(ex);
        }
    }

    // async void => void

    /// <summary>
    /// Attempt to perform an async <paramref name="activity"/> that does not return a value. 
    /// If an exception is thrown, it will be <see cref="Wrap">Wrapped</see> by an <see cref="IUnhandledLogicError"/>, otherwise <see cref="ILogicSuccess"/> is returned.
    /// </summary>
    /// <param name="activity">An async function that does not return a value that may throw an exception.</param>
    /// <returns>Either an <see cref="ILogicSuccess"/> or an <see cref="IUnhandledLogicError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static async Task<ILogicResult> Try(Func<Task> activity)
        => await Try(activity, Wrap);

    /// <summary>
    /// Attempt to perform an async <paramref name="activity"/> that does not return a value. 
    /// If an exception is thrown, it will be passed to <paramref name="handleError"/>.
    /// </summary>
    /// <param name="activity">An async function that does not return a value that may throw an exception.</param>
    /// <param name="handleError">A function that converts an <see cref="Exception"/> to an <see cref="ILogicResult"/>.</param>
    /// <returns>Either an <see cref="ILogicSuccess"/> or the result of <paramref name="handleError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static async Task<ILogicResult> Try(Func<Task> activity, Func<Exception, ILogicResult> handleError)
    {
        Check(activity);
        Check(handleError);

        try
        {
            await activity();
            return Success();
        }
        catch (Exception ex)
        {
            return handleError(ex);
        }
    }

    /// <summary>
    /// Attempt to perform an <paramref name="activity"/> that does not return a value. 
    /// If an exception is thrown, it will be passed to <paramref name="handleError"/>.
    /// </summary>
    /// <param name="activity">An async function that does not return a value that may throw an exception.</param>
    /// <param name="handleError">An async function that converts an <see cref="Exception"/> to an <see cref="ILogicResult"/>.</param>
    /// <returns>Either an <see cref="ILogicSuccess"/> or the result of <paramref name="handleError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static async Task<ILogicResult> Try(Func<Task> activity, Func<Exception, Task<ILogicResult>> handleError)
    {
        Check(activity);
        Check(handleError);

        try
        {
            await activity();
            return Success();
        }
        catch (Exception ex)
        {
            return await handleError(ex);
        }
    }

    // void => T

    /// <summary>
    /// Attempt to perform an <paramref name="activity"/> that returns a value.
    /// If an exception is thrown, it will be <see cref="Wrap{T}">Wrapped</see> by an <see cref="IUnhandledLogicError{T}"/>, otherwise <see cref="ILogicSuccess{T}"/> is returned.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by <paramref name="activity"/>.</typeparam>
    /// <param name="activity">A function that returns a value of type <typeparamref name="T"/> that may throw an exception.</param>
    /// <returns>Either an <see cref="ILogicSuccess{T}"/> or an <see cref="IUnhandledLogicError{T}"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static ILogicResult<T> Try<T>(Func<T> activity)
        => Try(activity, Wrap<T>);

    /// <summary>
    /// Attempt to perform an <paramref name="activity"/> that returns a value.
    /// If an exception is thrown, it will be passed to <paramref name="handleError"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by <paramref name="activity"/>.</typeparam>
    /// <param name="activity">A function that returns a value of type <typeparamref name="T"/> that may throw an exception.</param>
    /// <param name="handleError">A function that converts an <see cref="Exception"/> to an <see cref="ILogicResult{T}"/>.</param>
    /// <returns>Either an <see cref="ILogicSuccess{T}"/> or the result of <paramref name="handleError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static ILogicResult<T> Try<T>(Func<T> activity, Func<Exception, ILogicResult<T>> handleError)
    {
        Check(activity);
        Check(handleError);

        try
        {
            return Success(activity());
        }
        catch (Exception ex)
        {
            return handleError(ex);
        }
    }

    /// <summary>
    /// Attempt to perform an <paramref name="activity"/> that returns a value.
    /// If an exception is thrown, it will be passed to <paramref name="handleError"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by <paramref name="activity"/>.</typeparam>
    /// <param name="activity">A function that returns a value of type <typeparamref name="T"/> that may throw an exception.</param>
    /// <param name="handleError">An async function that converts an <see cref="Exception"/> to an <see cref="ILogicResult{T}"/>.</param>
    /// <returns>Either an <see cref="ILogicSuccess{T}"/> or the result of <paramref name="handleError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static async Task<ILogicResult<T>> Try<T>(Func<T> activity, Func<Exception, Task<ILogicResult<T>>> handleError)
    {
        Check(activity);
        Check(handleError);

        try
        {
            return Success(activity());
        }
        catch (Exception ex)
        {
            return await handleError(ex);
        }
    }

    // async void => T

    /// <summary>
    /// Attempt to perform an async <paramref name="activity"/> that returns a value.
    /// If an exception is thrown, it will be <see cref="Wrap">Wrapped</see> by an <see cref="IUnhandledLogicError{T}"/>, otherwise <see cref="ILogicSuccess{T}"/> is returned.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by <paramref name="activity"/>.</typeparam>
    /// <param name="activity">An async function that returns a value of type <typeparamref name="T"/> that may throw an exception.</param>
    /// <returns>Either an <see cref="ILogicSuccess"/> or an <see cref="IUnhandledLogicError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static async Task<ILogicResult<T>> Try<T>(Func<Task<T>> activity)
        => await Try(activity, Wrap<T>);

    /// <summary>
    /// Attempt to perform an async <paramref name="activity"/> that returns a value.
    /// If an exception is thrown, it will be passed to <paramref name="handleError"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by <paramref name="activity"/>.</typeparam>
    /// <param name="activity">An async function that returns a value of type <typeparamref name="T"/> that may throw an exception.</param>
    /// <param name="handleError">A function that converts an <see cref="Exception"/> to an <see cref="ILogicResult{T}"/>.</param>
    /// <returns>Either an <see cref="ILogicSuccess{T}"/> or the result of <paramref name="handleError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static async Task<ILogicResult<T>> Try<T>(Func<Task<T>> activity, Func<Exception, ILogicResult<T>> handleError)
    {
        Check(activity);
        Check(handleError);

        try
        {
            return Success(await activity());
        }
        catch (Exception ex)
        {
            return handleError(ex);
        }
    }

    /// <summary>
    /// Attempt to perform an async <paramref name="activity"/> that returns a value.
    /// If an exception is thrown, it will be passed to <paramref name="handleError"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by <paramref name="activity"/>.</typeparam>
    /// <param name="activity">A async function that returns a value of type <typeparamref name="T"/> that may throw an exception.</param>
    /// <param name="handleError">An async function that converts an <see cref="Exception"/> to an <see cref="ILogicResult{T}"/>.</param>
    /// <returns>Either an <see cref="ILogicSuccess{T}"/> or the result of <paramref name="handleError"/>, depending on if <paramref name="activity"/> threw an exception or not.</returns>
    public static async Task<ILogicResult<T>> Try<T>(Func<Task<T>> activity, Func<Exception, Task<ILogicResult<T>>> handleError)
    {
        Check(activity);
        Check(handleError);

        try
        {
            return Success(await activity());
        }
        catch (Exception ex)
        {
            return await handleError(ex);
        }
    }
}
