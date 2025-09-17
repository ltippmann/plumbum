namespace Plumbum;

/// <summary>
/// Well known error codes for <see cref="ILogicError">ILogicErrors</see>.
/// </summary>
public static class ErrorCodes
{
    /// <summary>
    /// An unspecified error occurred.
    /// </summary>
    public const string Unknown = "UNKNOWN";
    /// <summary>
    /// An exception was thrown.
    /// </summary>
    public const string Unhandled = "UNHANDLED";
    /// <summary>
    /// A requested entity was not found.
    /// </summary>
    public const string NotFound = "NOT_FOUND";
    /// <summary>
    /// Something did not pass validation checks.
    /// </summary>
    public const string NotValid = "NOT_VALID";
}
