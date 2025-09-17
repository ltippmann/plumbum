using static Plumbum.LogicResult;

namespace Plumbum;

internal readonly struct LogicSuccess : ILogicSuccess
{
    public bool Success => true;
}

internal readonly struct LogicSuccess<T> : ILogicSuccess<T>
{
    public bool Success => true;
    public T Value { get; init; }
}