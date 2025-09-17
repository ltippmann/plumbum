namespace Plumbum;

/// <summary>
/// Represents the result of an activity that may succeed or fail.
/// </summary>
public interface ILogicResult
{
    /// <summary>
    /// True if the activity succeeded.
    /// </summary>
    public bool Success { get; }
}

/// <summary>
/// Represents the result of an activity that may succeed or fail to produce a value.
/// </summary>
/// <typeparam name="T">The type of value to be produced on success.</typeparam>
public interface ILogicResult<T> : ILogicResult
{
    /// <summary>
    /// The value produced by the activity if successful.  Throws an <see cref="InvalidOperationException"/> if accessed when <see cref="ILogicResult.Success">Success</see> is false.
    /// </summary>
    public T Value { get; }
}

/// <summary>
/// Represents the result of an activity that succeeded.
/// </summary>
public interface ILogicSuccess : ILogicResult
{ }

/// <summary>
/// Represents the result of an activity that succeeded in producing a value.
/// </summary>
public interface ILogicSuccess<T> : ILogicSuccess, ILogicResult<T>
{ }

/// <summary>
/// Represents the result of an activity that was not successful.
/// </summary>
public interface ILogicError : ILogicResult
{
    /// <summary>
    /// A string that identifies the type of error that occurred.
    /// </summary>
    public string ErrorCode { get; }
    /// <summary>
    /// An optional name of the entity type the error relates to.
    /// </summary>
    public string? EntityType { get; }
    /// <summary>
    /// An optional human readable description of the error.
    /// </summary>
    public string? ErrorMessage { get; }
}

/// <summary>
/// Represents the result of an activity that was not successful in producing a value.
/// </summary>
/// <typeparam name="T">The type of value that would have been produced on success.</typeparam>
public interface ILogicError<T> : ILogicError, ILogicResult<T>
{ }

/// <summary>
/// Represents the result of an activity that threw an exception.
/// </summary>
public interface IUnhandledLogicError : ILogicError
{ 
    /// <summary>
    /// The optional <see cref="Exception"/> that caused the error.
    /// </summary>
    public Exception? Exception { get; }
}

/// <summary>
/// Represents the result of an activity that threw an exception instead of producing a value.
/// </summary>
/// <typeparam name="T">The type of value that would have been produced on success.</typeparam>
public interface IUnhandledLogicError<T> : ILogicError<T>, IUnhandledLogicError
{}