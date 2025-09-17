using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.PipeTests;

[TestFixture(TestOf = typeof(PipeExtensions))]
public class VoidVoidTests
{
    // void => void

    [Test]
    public void Given_a_successful_LogicResult_with_no_value_When_Pipe_is_called_with_a_parameterless_void_action_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = Success().Pipe(() => { actionExecuted = true; });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public void Given_an_unsuccessful_LogicResult_with_no_value_When_Pipe_is_called_with_a_parameterless_void_action_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = TestError().Pipe(() => { actionExecuted = true; });

        AssertActionNotExecuted(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_LogicResult_with_no_value_When_Pipe_is_called_with_a_parameterless_async_void_action_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await Success().Pipe(() => { actionExecuted = true; return Task.CompletedTask; });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_an_unsuccessful_LogicResult_with_no_value_When_Pipe_is_called_with_a_parameterless_async_void_action_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestError().Pipe(() => { actionExecuted = true; return Task.CompletedTask; });

        AssertActionNotExecuted(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_with_no_value_When_Pipe_is_called_with_a_parameterless_void_action_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await Success().Async().Pipe(() => { actionExecuted = true; });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResult_with_no_value_When_Pipe_is_called_with_a_parameterless_void_action_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestError().Async().Pipe(() => { actionExecuted = true; });

        AssertActionNotExecuted(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_with_no_value_When_Pipe_is_called_with_a_parameterless_async_void_action_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await Success().Async().Pipe(() => { actionExecuted = true; return Task.CompletedTask; });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_unsuccessful_async_LogicResult_with_no_value_When_Pipe_is_called_with_a_parameterless_async_void_action_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestError().Async().Pipe(() => { actionExecuted = true; return Task.CompletedTask; });

        AssertActionNotExecuted(result, actionExecuted);
    }
}
