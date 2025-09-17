namespace Plumbum;

internal readonly struct LogicError : ILogicError
{
    public LogicError()
    {
        if (string.IsNullOrWhiteSpace(ErrorCode))
            ErrorCode = ErrorCodes.Unknown;
#if NETSTANDARD
        ErrorCode = "";
#endif
    }

    public bool Success => false;
    public string ErrorCode { get; init; }
    public string? EntityType { get; init; }
    public string? ErrorMessage { get; init; }
}

internal readonly struct LogicError<T> : ILogicError<T>
{
    public LogicError()
    {
        if (string.IsNullOrWhiteSpace(ErrorCode))
            ErrorCode = ErrorCodes.Unknown;
#if NETSTANDARD
        ErrorCode = "";
#endif
    }

    public bool Success => false;
    public string ErrorCode { get; init; }
    public string? EntityType { get; init; }
    public string? ErrorMessage { get; init; }
    public T Value => throw new InvalidOperationException($"An unsuccessful {nameof(ILogicResult)} does not have a value.");
}

internal readonly struct UnhandledLogicError : IUnhandledLogicError
{
    public UnhandledLogicError()
    {
        if (string.IsNullOrWhiteSpace(ErrorCode))
            ErrorCode = ErrorCodes.Unhandled;
#if NETSTANDARD
        ErrorCode = "";
#endif
    }

    public bool Success => false;
    public string ErrorCode { get; init; }
    public string? EntityType { get; init; }
    public string? ErrorMessage { get; init; }
    public Exception? Exception { get; init; }
}

internal readonly struct UnhandledLogicError<T> : IUnhandledLogicError<T>
{
    public UnhandledLogicError()
    {
        if (string.IsNullOrWhiteSpace(ErrorCode))
            ErrorCode = ErrorCodes.Unhandled;
#if NETSTANDARD
        ErrorCode = "";
#endif
    }

    public bool Success => false;
    public string ErrorCode { get; init; }
    public string? EntityType { get; init; }
    public string? ErrorMessage { get; init; }
    public Exception? Exception { get; init; }
    public T Value => throw new InvalidOperationException($"An unsuccessful {nameof(ILogicResult)} does not have a value.");
}
