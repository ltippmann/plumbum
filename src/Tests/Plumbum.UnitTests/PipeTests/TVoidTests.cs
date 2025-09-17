using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.PipeTests;

[TestFixture(TestOf = typeof(PipeExtensions))]
public class TVoidTests
{
    // T => void

    [Test]
    public void Given_a_successful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_void_action_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = SuccessT().Pipe(_ => { actionExecuted = true; });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
    }

    [Test]
    public void Given_an_unsuccessful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_void_action_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = TestErrorT().Pipe(_ => { actionExecuted = true; });

        AssertActionNotExecuted(result, actionExecuted, false, TestTypes.T);
    }

    [Test]
    public async Task Given_a_successful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_void_action_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Pipe(_ => { actionExecuted = true; return Task.CompletedTask; });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
    }

    [Test]
    public async Task Given_an_unsuccessful_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_void_action_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Pipe(_ => { actionExecuted = true; return Task.CompletedTask; });

        AssertActionNotExecuted(result, actionExecuted, false, TestTypes.T);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_void_action_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Async().Pipe(_ => { actionExecuted = true; });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_void_action_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Async().Pipe(_ => { actionExecuted = true; });

        AssertActionNotExecuted(result, actionExecuted, false, TestTypes.T);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_void_action_Then_the_action_should_be_executed_and_a_successful_LogicResult_returned()
    {
        var actionExecuted = false;
        var result = await SuccessT().Async().Pipe(_ => { actionExecuted = true; return Task.CompletedTask; });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
    }

    [Test]
    public async Task Given_a_unsuccessful_async_LogicResult_with_a_value_When_Pipe_is_called_with_a_parameterless_async_void_action_Then_the_action_should_not_be_executed_and_the_unsuccessful_LogicResult_should_be_forwarded()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Async().Pipe(_ => { actionExecuted = true; return Task.CompletedTask; });

        AssertActionNotExecuted(result, actionExecuted, false, TestTypes.T);
    }
}

