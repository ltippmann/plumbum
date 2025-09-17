namespace Plumbum.Exceptions;

/// <summary>
/// Thrown when the activity parameter to a <see cref="PipeExtensions.Pipe(ILogicResult, Action)">Pipe</see> or <see cref="LogicResult.Try(Action)">Try</see> method is null.
/// </summary>
public class ActivityNullException : ArgumentNullException
{
    /// <summary>
    /// Create a new <see cref="ActivityNullException"/>.
    /// </summary>
    public ActivityNullException() : base("activity", "Activity lambda cannot be null.") { }
}
