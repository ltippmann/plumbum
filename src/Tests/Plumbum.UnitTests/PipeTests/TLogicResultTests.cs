using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.PipeTests;

[TestFixture(TestOf = typeof(PipeExtensions))]
public class TLogicResultTests
{
    // T => ILogicResult

    [Test]
    public void Given_a_successful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_action_that_returns_a_successful_LogicResult_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = SuccessT().Pipe(_ => { actionExecuted = true; return Success(); });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public void Given_a_successful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_action_that_returns_an_unsuccessful_LogicResult_Then_the_action_should_be_executed_and_an_unsuccessful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = SuccessT().Pipe(_ => { actionExecuted = true; return TestError(); });

        AssertUnsuccessfulAction(result, actionExecuted);
    }

    [Test]
    public void Given_an_unsuccessful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_action_that_returns_a_successful_LogicResult_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = TestErrorT().Pipe(_ => { actionExecuted = true; return Success(); });

        AssertActionNotExecuted(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_action_that_returns_a_successful_LogicResult_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Pipe(_ => { actionExecuted = true; return Success().Async(); });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_action_that_returns_an_unsuccessful_LogicResult_Then_the_action_should_be_executed_and_an_unsuccessful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Pipe(_ => { actionExecuted = true; return TestError().Async(); });

        AssertUnsuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_an_unsuccessful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_action_that_returns_a_successful_LogicResult_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Pipe(_ => { actionExecuted = true; return Success().Async(); });

        AssertActionNotExecuted(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_action_that_returns_a_successful_LogicResult_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Async().Pipe(_ => { actionExecuted = true; return Success(); });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_action_that_returns_an_unsuccessful_LogicResult_Then_the_action_should_be_executed_and_an_unsuccessful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Async().Pipe(_ => { actionExecuted = true; return TestError(); });

        AssertUnsuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_action_that_returns_a_successful_LogicResult_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Async().Pipe(_ => { actionExecuted = true; return Success(); });

        AssertActionNotExecuted(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_action_that_returns_a_successful_LogicResult_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Async().Pipe(_ => { actionExecuted = true; return Success().Async(); });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_action_that_returns_an_unsuccessful_LogicResult_Then_the_action_should_be_executed_and_an_unsuccessful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Async().Pipe(_ => { actionExecuted = true; return TestError().Async(); });

        AssertUnsuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_action_that_returns_a_successful_LogicResult_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Async().Pipe(_ => { actionExecuted = true; return Success().Async(); });

        AssertActionNotExecuted(result, actionExecuted);
    }
}
