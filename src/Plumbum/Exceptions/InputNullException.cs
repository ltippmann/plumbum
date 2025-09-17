namespace Plumbum.Exceptions;

/// <summary>
/// Thrown when the activity parameter to a <see cref="PipeExtensions.Pipe(ILogicResult, Action)">Pipe</see> or <see cref="LogicResult.Try(Action)">Try</see> method is null.
/// </summary>
public class InputNullException : ArgumentNullException
{
    /// <summary>
    /// Create a new <see cref="InputNullException"/>.
    /// </summary>
    public InputNullException() : base("input", "Input LogicResult cannot be null.") { }
}
