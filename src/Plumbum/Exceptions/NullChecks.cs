namespace Plumbum.Exceptions;

internal static class NullChecks
{
    // Check input for null

    public static T Check<T>(T input)
        where T : ILogicResult
        => input ?? throw new InputNullException();

    public static async Task<T> Check<T>(Task<T> input)
        where T : ILogicResult
        => (await (input ?? throw new InputNullException()))
            ?? throw new ActivityNullException();

    // Check activity for null

    public static Action Check(Action activity)
        => activity ?? throw new ActivityNullException();

    public static Func<ILogicResult> Check(Func<ILogicResult> activity)
        => activity ?? throw new ActivityNullException();

    public static Func<Task> Check(Func<Task> activity)
        => activity ?? throw new ActivityNullException();

    public static Func<Task<ILogicResult>> Check(Func<Task<ILogicResult>> activity)
        => activity ?? throw new ActivityNullException();

    public static Action<T> Check<T>(Action<T> activity)
        => activity ?? throw new ActivityNullException();

    public static Func<T, ILogicResult> Check<T>(Func<T, ILogicResult> activity)
        => activity ?? throw new ActivityNullException();

    public static Func<T, Task> Check<T>(Func<T, Task> activity)
        => activity ?? throw new ActivityNullException();

    public static Func<T, Task<ILogicResult>> Check<T>(Func<T, Task<ILogicResult>> activity)
        => activity ?? throw new ActivityNullException();

    public static Func<R> Check<R>(Func<R> activity)
        => activity ?? throw new ActivityNullException();

    public static Func<ILogicResult<R>> Check<R>(Func<ILogicResult<R>> activity)
        => activity ?? throw new ActivityNullException();

    public static Func<T, R> Check<T, R>(Func<T, R> activity)
        => activity ?? throw new ActivityNullException();

    // Check handleError for null

    public static Func<Exception, ILogicResult> Check(Func<Exception, ILogicResult> handleError)
        => handleError ?? throw new HandleErrorNullException();

    public static Func<Exception, Task<ILogicResult>> Check(Func<Exception, Task<ILogicResult>> handleError)
        => handleError ?? throw new HandleErrorNullException();

    public static Func<Exception, ILogicResult<T>> Check<T>(Func<Exception, ILogicResult<T>> handleError)
        => handleError ?? throw new HandleErrorNullException();

    public static Func<Exception, Task<ILogicResult<T>>> Check<T>(Func<Exception, Task<ILogicResult<T>>> handleError)
        => handleError ?? throw new HandleErrorNullException();

}
