using static Plumbum.Exceptions.NullChecks;

namespace Plumbum;

public static partial class PipeExtensions
{
    // void => void

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function to execute against the preceding <see cref="ILogicError"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static ILogicResult Trap(this ILogicResult input, Action<ILogicError> activity)
    {
        if (Check(input) is ILogicError err)
            Check(activity)(err);
        return input;
    }

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail only if the async <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed.</param>
    /// <param name="activity">A function to execute against the preceding <see cref="ILogicError"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Trap(this Task<ILogicResult> input, Action<ILogicError> activity)
        => Trap(await input, activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function to execute against the preceding <see cref="ILogicError"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Trap(this ILogicResult input, Func<ILogicError, Task> activity)
    {
        if (Check(input) is ILogicError err)
            await Check(activity)(err);
        return input;
    }

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail only if the async <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function to execute against the preceding <see cref="ILogicError"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Trap(this Task<ILogicResult> input, Func<ILogicError, Task> activity)
        => await Trap(await input, activity);

    // void => ILogicResult

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail only if the <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function to execute against the preceding <see cref="ILogicError"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static ILogicResult Trap(this ILogicResult input, Func<ILogicError, ILogicResult> activity)
    {
        if (Check(input) is ILogicError err)
            return Check(activity)(err);
        return input;
    }

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail only if the async <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">A function to execute against the preceding <see cref="ILogicError"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Trap(this Task<ILogicResult> input, Func<ILogicError, ILogicResult> activity)
        => Trap(await input, activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail only if the <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function to execute against the preceding <see cref="ILogicError"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Trap(this ILogicResult input, Func<ILogicError, Task<ILogicResult>> activity)
    {
        if (Check(input) is ILogicError err)
            return await Check(activity)(err);
        return input;
    }

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail only if the async <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed.</param>
    /// <param name="activity">An async function to execute against the preceding <see cref="ILogicError"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult> Trap(this Task<ILogicResult> input, Func<ILogicError, Task<ILogicResult>> activity)
        => await Trap(await input, activity);

    // T => void

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function to execute against the preceding <see cref="ILogicError{T}"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding activity.</returns>
    public static ILogicResult<T> Trap<T>(this ILogicResult<T> input, Action<ILogicError> activity)
    {
        if (Check(input) is ILogicError err)
            Check(activity)(err);
        return input;
    }

    /// <summary>
    /// Perform an <paramref name="activity"/> that is not expected to fail only if the async <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function to execute against the preceding <see cref="ILogicError{T}"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult<T>> Trap<T>(this Task<ILogicResult<T>> input, Action<ILogicError> activity)
        => Trap(await input, activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail only if the <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function to execute against the preceding <see cref="ILogicError{T}"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult<T>> Trap<T>(this ILogicResult<T> input, Func<ILogicError, Task> activity)
    {
        if (Check(input) is ILogicError err)
            await Check(activity)(err);
        return input;
    }

    /// <summary>
    /// Perform an async <paramref name="activity"/> that is not expected to fail only if the async <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function to execute against the preceding <see cref="ILogicError{T}"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding activity.</returns>
    public static async Task<ILogicResult<T>> Trap<T>(this Task<ILogicResult<T>> input, Func<ILogicError, Task> activity)
        => await Trap(await input, activity);

    // T => LogicResult<T>

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail to produce a value only if the <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function to execute against the preceding <see cref="ILogicError{T}"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding and this activity.</returns>
    public static ILogicResult<T> Trap<T>(this ILogicResult<T> input, Func<ILogicError, ILogicResult<T>> activity)
    {
        if (Check(input) is ILogicError err)
            return Check(activity)(err);
        return input;
    }

    /// <summary>
    /// Perform an <paramref name="activity"/> that may succeed or fail to produce a value only if the async <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">A function to execute against the preceding <see cref="ILogicError{T}"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding and this activity.</returns>
    public static async Task<ILogicResult<T>> Trap<T>(this Task<ILogicResult<T>> input, Func<ILogicError, ILogicResult<T>> activity)
        => Trap(await input, activity);

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail to produce a value only if the <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function to execute against the preceding <see cref="ILogicError{T}"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding and this activity.</returns>
    public static async Task<ILogicResult<T>> Trap<T>(this ILogicResult<T> input, Func<ILogicError, Task<ILogicResult<T>>> activity)
    {
        if (Check(input) is ILogicError err)
            return await Check(activity)(err);
        return input;
    }

    /// <summary>
    /// Perform an async <paramref name="activity"/> that may succeed or fail to produce a value only if the async <paramref name="input"/> is not successful.
    /// </summary>
    /// <param name="input">The result of a preceding async activity that may have succeeded or failed to produce a value.</param>
    /// <param name="activity">An async function to execute against the preceding <see cref="ILogicError{T}"/> only if <paramref name="input"/> is not successful.</param>
    /// <returns>An <see cref="ILogicResult{T}"/> that represents the result of the preceding and this activity.</returns>
    public static async Task<ILogicResult<T>> Trap<T>(this Task<ILogicResult<T>> input, Func<ILogicError, Task<ILogicResult<T>>> activity)
        => await Trap(await input, activity);
}
