using static Plumbum.LogicResult;

namespace Plumbum.UnitTests;

internal class TestTypeT { }
internal class TestTypeR { }

internal enum TestTypes { T, R }

internal static class TestHelper
{
    public static ILogicResult<TestTypeT> SuccessT()
        => Success(new TestTypeT { });

    public static ILogicResult<TestTypeR> SuccessR()
        => Success(new TestTypeR { });

    public static ILogicResult TestError()
        => Error("ASDF", "SDFG", "DFGH");

    public static ILogicResult<TestTypeT> TestErrorT()
        => Error<TestTypeT>("ASDF", "SDFG", "DFGH");

    public static ILogicResult<TestTypeR> TestErrorR()
        => Error<TestTypeR>("ASDF", "SDFG", "DFGH");

    private static void AssertSuccess(ILogicResult result)
    {
        Assert.That(result, Is.Not.Null, () => "Pipe returned an unexpected null.");
        Assert.That(result.Success, Is.True, () => "Pipe returned an unsuccessful LogicResult.");
        Assert.That(result is ILogicSuccess, Is.True, () => "Pipe did not return a LogicSuccess.");
        Assert.That(result is ILogicError, Is.False, () => "Pipe returned a LogicError.");
        Assert.That(result.GetType().GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ILogicResult<>)).Any(),
            Is.False, () => "LogicResult is ILogicResult<T> when only ILogicResult was expected.");
    }

    private static void AssertSuccess<T>(ILogicResult<T> result, TestTypes testType)
    {
        Assert.That(result, Is.Not.Null, () => "Pipe returned an unexpected null.");
        Assert.That(result.Success, Is.True, () => "Pipe returned an unsuccessful LogicResult.");
        Assert.That(result is ILogicSuccess, Is.True, () => "Pipe did not return a LogicSuccess.");
        Assert.That(result is ILogicSuccess<T>, Is.True, () => "Pipe did not return a LogicSuccess<T>.");
        Assert.That(result is ILogicError, Is.False, () => "Pipe returned a LogicError.");
        Assert.That(result.Value, Is.Not.Null);
        switch (testType)
        {
            case TestTypes.T:
                Assert.That(result.Value, Is.TypeOf<TestTypeT>(), () => $"LogicResult value was the wrong type. ");
                break;
            case TestTypes.R:
                Assert.That(result.Value, Is.TypeOf<TestTypeR>(), () => $"LogicResult value was the wrong type. ");
                break;
            default:
                Assert.Fail($"Unexpected value for testType: {testType}");
                break;
        }
    }

    public static void AssertError(ILogicResult result)
    {
        Assert.That(result, Is.Not.Null, () => "Pipe returned an unexpected null.");
        Assert.That(result.Success, Is.False, () => "Pipe returned an successful LogicResult.");
        Assert.That(result is ILogicSuccess, Is.False, () => "Pipe returned a LogicSuccess.");
        Assert.That(result is ILogicError, Is.True, () => "Pipe did not return a LogicError.");
        var err = (ILogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo("ASDF"));
        Assert.That(err.ErrorMessage, Is.EqualTo("SDFG"));
        Assert.That(err.EntityType, Is.EqualTo("DFGH"));
    }

    public static void AssertError<T>(ILogicResult<T> result, TestTypes testType = TestTypes.T)
    {
        Assert.That(result, Is.Not.Null, () => "Pipe returned an unexpected null.");
        Assert.That(result.Success, Is.False, () => "Pipe returned an successful LogicResult.");
        Assert.That(result is ILogicSuccess, Is.False, () => "Pipe returned a LogicSuccess.");
        Assert.That(result is ILogicError, Is.True, () => "Pipe did not return a LogicError.");
        switch (testType)
        {
            case TestTypes.T:
                Assert.That(result is ILogicError<TestTypeT>, Is.True, () => "Pipe did not return a LogicError<T>.");
                break;
            case TestTypes.R:
                Assert.That(result is ILogicError<TestTypeR>, Is.True, () => "Pipe did not return a LogicError<R>.");
                break;
            default:
                Assert.Fail($"Unexpected value for testType: {testType}");
                break;
        }
        var err = (ILogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo("ASDF"));
        Assert.That(err.ErrorMessage, Is.EqualTo("SDFG"));
        Assert.That(err.EntityType, Is.EqualTo("DFGH"));
    }

    public static void AssertSuccessfulAction(ILogicResult result, bool actionExecuted)
    {
        AssertSuccess(result);
        Assert.That(actionExecuted, Is.True, () => "Action was not executed.");
    }

    public static void AssertSuccessfulAction<T>(ILogicResult<T> result, bool actionExecuted, TestTypes testType)
    {
        AssertSuccess(result, testType);
        Assert.That(actionExecuted, Is.True, () => "Action was not executed.");
    }

    public static void AssertActionNotExecuted(ILogicResult result, bool actionExecuted, bool successExpected = false)
    {
        if (successExpected)
            AssertSuccess(result);
        else
            AssertError(result);
        Assert.That(actionExecuted, Is.False, () => "Action was executed when it should not have been.");
    }

    public static void AssertActionNotExecuted<T>(ILogicResult<T> result, bool actionExecuted, bool successExpected = false, TestTypes testType = TestTypes.R)
    {
        if (successExpected)
            AssertSuccess(result, testType);
        else
            AssertError(result, testType);
        Assert.That(actionExecuted, Is.False, () => "Action was executed when it should not have been.");
    }

    public static void AssertUnsuccessfulAction(ILogicResult result, bool actionExecuted)
    {
        AssertError(result);
        Assert.That(actionExecuted, Is.True, () => "Action was not executed.");
   }

    public static void AssertUnsuccessfulAction<T>(ILogicResult<T> result, bool actionExecuted, TestTypes testType = TestTypes.R)
    {
        AssertError(result, testType);
        Assert.That(actionExecuted, Is.True, () => "Action was not executed.");
   }
}
