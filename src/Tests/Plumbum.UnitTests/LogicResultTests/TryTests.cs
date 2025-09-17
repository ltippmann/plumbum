using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.LogicResultTests;

[TestFixture(TestOf = typeof(LogicResult))]
public class TryTests
{
    // void => ILogicResult

    [Test]
    public void Given_a_void_activity_and_no_errorHandler_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var result = Try(() => { actionExecuted = true; });

        AssertSuccessfulAction(result, actionExecuted);
        Assert.That(actionExecuted, Is.True);
    }

    [Test]
    public void Given_a_void_activity_and_no_errorHandler_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_IUnhandledLogicError_should_be_returned()
    {
        var actionExecuted = false;
        var result = Try(() => { actionExecuted = true; throw new Exception("ASDF"); return; });

        AssertUnhandledError(result);
        Assert.That(actionExecuted, Is.True);
    }

    [Test]
    public void Given_a_void_activity_and_an_errorHandler_that_returns_a_success_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = Try(() => { actionExecuted = true; }, HandleErrorSuccess(() => handlerExecuted = true));

        AssertSuccessfulAction(result, actionExecuted);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.False);
    }

    [Test]
    public void Given_a_void_activity_and_an_errorHandler_that_returns_an_error_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_unsuccessful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = Try(() => { actionExecuted = true; throw new Exception("ASDF"); return; }, HandleErrorError(() => handlerExecuted = true));

        AssertError(result);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.True);
    }

    [Test]
    public async Task Given_a_void_activity_and_an_errorHandler_that_returns_an_async_success_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; }, HandleErrorSuccessAsync(() => handlerExecuted = true));

        AssertSuccessfulAction(result, actionExecuted);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.False);
    }

    [Test]
    public async Task Given_a_void_activity_and_an_errorHandler_that_returns_an_async_error_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_unsuccessful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; throw new Exception("ASDF"); return; }, HandleErrorErrorAsync(() => handlerExecuted = true));

        AssertError(result);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.True);
    }

    // void => Task<ILogicResult>

    [Test]
    public async Task Given_an_async_void_activity_and_no_errorHandler_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var result = await Try(() => { actionExecuted = true; return Task.CompletedTask; });

        AssertSuccessfulAction(result, actionExecuted);
        Assert.That(actionExecuted, Is.True);
    }

    [Test]
    public async Task Given_an_async_void_activity_and_no_errorHandler_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_IUnhandledLogicError_should_be_returned()
    {
        var actionExecuted = false;
        var result = await Try(() => { actionExecuted = true; throw new Exception("ASDF"); return Task.CompletedTask; });

        AssertUnhandledError(result);
        Assert.That(actionExecuted, Is.True);
    }

    [Test]
    public async Task Given_an_async_void_activity_and_an_errorHandler_that_returns_a_success_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; return Task.CompletedTask; }, HandleErrorSuccess(() => handlerExecuted = true));

        AssertSuccessfulAction(result, actionExecuted);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.False);
    }

    [Test]
    public async Task Given_an_async_void_activity_and_an_errorHandler_that_returns_an_error_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_unsuccessful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; throw new Exception("ASDF"); return Task.CompletedTask; }, HandleErrorError(() => handlerExecuted = true));

        AssertError(result);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.True);
    }

    [Test]
    public async Task Given_an_async_void_activity_and_an_errorHandler_that_returns_an_async_success_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; return Task.CompletedTask; }, HandleErrorSuccessAsync(() => handlerExecuted = true));

        AssertSuccessfulAction(result, actionExecuted);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.False);
    }

    [Test]
    public async Task Given_an_async_void_activity_and_an_errorHandler_that_returns_an_async_error_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_unsuccessful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; throw new Exception("ASDF"); return Task.CompletedTask; }, HandleErrorErrorAsync(() => handlerExecuted = true));

        AssertError(result);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.True);
    }




    // T => ILogicResult<T>

    [Test]
    public void Given_an_activity_that_returns_a_value_and_no_errorHandler_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var result = Try(() => { actionExecuted = true; return new TestTypeT(); });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
        Assert.That(actionExecuted, Is.True);
    }

    [Test]
    public void Given_an_activity_that_returns_a_value_and_no_errorHandler_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_IUnhandledLogicError_should_be_returned()
    {
        var actionExecuted = false;
        var result = Try(() => { actionExecuted = true; throw new Exception("ASDF"); return new TestTypeT(); });

        AssertUnhandledError(result);
        Assert.That(actionExecuted, Is.True);
    }

    [Test]
    public void Given_an_activity_that_returns_a_value_and_an_errorHandler_that_returns_a_success_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = Try(() => { actionExecuted = true; return new TestTypeT(); }, HandleErrorSuccessT(() => handlerExecuted = true));

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.False);
    }

    [Test]
    public void Given_an_activity_that_returns_a_value_and_an_errorHandler_that_returns_an_error_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_unsuccessful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = Try(() => { actionExecuted = true; throw new Exception("ASDF"); return new TestTypeT(); }, HandleErrorErrorT(() => handlerExecuted = true));

        AssertError(result);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.True);
    }

    [Test]
    public async Task Given_an_activity_that_returns_a_value_and_an_errorHandler_that_returns_an_async_success_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; return new TestTypeT(); }, HandleErrorSuccessTAsync(() => handlerExecuted = true));

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.False);
    }

    [Test]
    public async Task Given_an_activity_that_returns_a_value_and_an_errorHandler_that_returns_an_async_error_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_unsuccessful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; throw new Exception("ASDF"); return new TestTypeT(); }, HandleErrorErrorTAsync(() => handlerExecuted = true));

        AssertError(result);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.True);
    }

    // Task<T> => Task<ILogicResult<T>>

    [Test]
    public async Task Given_an_async_activity_that_returns_a_value_and_no_errorHandler_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var result = await Try(() => { actionExecuted = true; return Task.FromResult(new TestTypeT()); });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
        Assert.That(actionExecuted, Is.True);
    }

    [Test]
    public async Task Given_an_async_activity_that_returns_a_value_and_no_errorHandler_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_IUnhandledLogicError_should_be_returned()
    {
        var actionExecuted = false;
        var result = await Try(() => { actionExecuted = true; throw new Exception("ASDF"); return Task.FromResult(new TestTypeT()); });

        AssertUnhandledError(result);
        Assert.That(actionExecuted, Is.True);
    }

    [Test]
    public async Task Given_an_async_activity_that_returns_a_value_and_an_errorHandler_that_returns_a_success_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; return Task.FromResult(new TestTypeT()); }, HandleErrorSuccessT(() => handlerExecuted = true));

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.False);
    }

    [Test]
    public async Task Given_an_async_activity_that_returns_a_value_and_an_errorHandler_that_returns_an_error_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_unsuccessful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; throw new Exception("ASDF"); return Task.FromResult(new TestTypeT()); }, HandleErrorErrorT(() => handlerExecuted = true));

        AssertError(result);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.True);
    }

    [Test]
    public async Task Given_an_async_activity_that_returns_a_value_and_an_errorHandler_that_returns_an_async_success_When_Try_is_called_and_the_activity_does_not_throw_an_exception_Then_a_successful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; return Task.FromResult(new TestTypeT()); }, HandleErrorSuccessTAsync(() => handlerExecuted = true));

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.False);
    }

    [Test]
    public async Task Given_an_async_activity_that_returns_a_value_and_an_errorHandler_that_returns_an_async_error_When_Try_is_called_and_the_activity_throws_an_exception_Then_an_unsuccessful_LogicResult_should_be_returned()
    {
        var actionExecuted = false;
        var handlerExecuted = false;
        var result = await Try(() => { actionExecuted = true; throw new Exception("ASDF"); return Task.FromResult(new TestTypeT()); }, HandleErrorErrorTAsync(() => handlerExecuted = true));

        AssertError(result);
        Assert.That(actionExecuted, Is.True);
        Assert.That(handlerExecuted, Is.True);
    }




    private Func<Exception, ILogicResult> HandleErrorSuccess(Action onExecuted)
        => _ => { onExecuted(); return Success(); };

    private Func<Exception, ILogicResult> HandleErrorError(Action onExecuted)
        => _ => { onExecuted(); return Error("ASDF", "SDFG", "DFGH"); };

    private Func<Exception, ILogicResult<TestTypeT>> HandleErrorSuccessT(Action onExecuted)
        => _ => { onExecuted(); return Success(new TestTypeT()); };

    private Func<Exception, ILogicResult<TestTypeT>> HandleErrorErrorT(Action onExecuted)
        => _ => { onExecuted(); return Error<TestTypeT>("ASDF", "SDFG", "DFGH"); };

    private Func<Exception, Task<ILogicResult>> HandleErrorSuccessAsync(Action onExecuted)
        => _ => { onExecuted(); return Success().Async(); };

    private Func<Exception, Task<ILogicResult>> HandleErrorErrorAsync(Action onExecuted)
        => _ => { onExecuted(); return Error("ASDF", "SDFG", "DFGH").Async(); };

    private Func<Exception, Task<ILogicResult<TestTypeT>>> HandleErrorSuccessTAsync(Action onExecuted)
        => _ => { onExecuted(); return Success(new TestTypeT()).Async(); };

    private Func<Exception, Task<ILogicResult<TestTypeT>>> HandleErrorErrorTAsync(Action onExecuted)
        => _ => { onExecuted(); return Error<TestTypeT>("ASDF", "SDFG", "DFGH").Async(); };

    private void AssertUnhandledError(ILogicResult result)
    {
        Assert.That(result, Is.Not.Null, () => "Pipe returned an unexpected null.");
        Assert.That(result.Success, Is.False, () => "Pipe returned an successful LogicResult.");
        Assert.That(result is ILogicSuccess, Is.False, () => "Pipe returned a LogicSuccess.");
        Assert.That(result is ILogicError, Is.True, () => "Pipe did not return a LogicError.");
        Assert.That(result is IUnhandledLogicError, Is.True, () => "Pipe did not return an UnhandledLogicError.");
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.ErrorMessage, Is.EqualTo("ASDF"));
        Assert.That(err.EntityType, Is.Null);
    }

    private void AssertUnhandledError<T>(ILogicResult<T> result)
    {
        Assert.That(result, Is.Not.Null, () => "Pipe returned an unexpected null.");
        Assert.That(result.Success, Is.False, () => "Pipe returned an successful LogicResult.");
        Assert.That(result is ILogicSuccess, Is.False, () => "Pipe returned a LogicSuccess.");
        Assert.That(result is ILogicError, Is.True, () => "Pipe did not return a LogicError.");
        Assert.That(result is IUnhandledLogicError, Is.True, () => "Pipe did not return an UnhandledLogicError.");
        Assert.That(result is ILogicError<T>, Is.True, () => "Pipe did not return a LogicError<T>.");
        Assert.That(result is IUnhandledLogicError<T>, Is.True, () => "Pipe did not return an UnhandledLogicError<T>.");
        var err = (IUnhandledLogicError<T>)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.ErrorMessage, Is.EqualTo("ASDF"));
        Assert.That(err.EntityType, Is.EqualTo(typeof(T).Name));
    }

}
