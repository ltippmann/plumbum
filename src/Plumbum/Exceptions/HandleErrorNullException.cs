namespace Plumbum.Exceptions;

/// <summary>
/// Thrown when the handleError parameter to a <see cref="LogicResult.Try(Action)">Try</see> method is null.
/// </summary>
public class HandleErrorNullException : ArgumentNullException
{
    /// <summary>
    /// Create a new <see cref="HandleErrorNullException"/>.
    /// </summary>
    public HandleErrorNullException() : base("handleError", "HandleError lambda cannot be null.") { }
}
