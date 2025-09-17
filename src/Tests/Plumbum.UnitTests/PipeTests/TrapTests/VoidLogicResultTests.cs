using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.PipeTests.TrapTests;

[TestFixture(TestOf = typeof(PipeExtensions))]
public class VoidLogicResultTests
{
    // Void => LogicResult

    [Test]
    public void Given_a_successful_LogicResult_When_Trap_is_called_with_an_activity_that_returns_a_LogicResult_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = Success().Trap(err => { actionExecuted = true; return Success(); });

        AssertActionNotExecuted(result, actionExecuted, true);
    }

    [Test]
    public void Given_an_unsuccessful_LogicResult_When_Trap_is_called_with_an_activity_that_returns_a_LogicResult_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = TestError().Trap(err => { actionExecuted = true; return Success(); });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_When_Trap_is_called_with_an_activity_that_returns_a_LogicResult_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await Success().Async().Trap(err => { actionExecuted = true; return Success(); });

        AssertActionNotExecuted(result, actionExecuted, true);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResult_When_Trap_is_called_with_an_activity_that_returns_a_LogicResult_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestError().Async().Trap(err => { actionExecuted = true; return Success(); });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_LogicResult_When_Trap_is_called_with_an_async_activity_that_returns_a_LogicResult_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await Success().Trap(err => { actionExecuted = true; return Success().Async(); });

        AssertActionNotExecuted(result, actionExecuted, true);
    }

    [Test]
    public async Task Given_an_unsuccessful_LogicResult_When_Trap_is_called_with_an_async_activity_that_returns_a_LogicResult_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestError().Trap(err => { actionExecuted = true; return Success().Async(); });

        AssertSuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_When_Trap_is_called_with_an_async_activity_that_returns_a_LogicResult_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await Success().Async().Trap(err => { actionExecuted = true; return Success().Async(); });

        AssertActionNotExecuted(result, actionExecuted, true);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResult_When_Trap_is_called_with_an_async_activity_that_returns_a_LogicResult_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestError().Async().Trap(err => { actionExecuted = true; return Success().Async(); });

        AssertSuccessfulAction(result, actionExecuted);
    }
}

