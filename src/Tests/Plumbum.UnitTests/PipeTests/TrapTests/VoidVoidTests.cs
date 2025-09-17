using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.PipeTests.TrapTests;

[TestFixture(TestOf = typeof(PipeExtensions))]
public class VoidVoidTests
{
    // Void => Void

    [Test]
    public void Given_a_successful_LogicResult_When_Trap_is_called_with_a_void_activity_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = Success().Trap(err => { actionExecuted = true; });

        AssertActionNotExecuted(result, actionExecuted, true);
    }

    [Test]
    public void Given_an_unsuccessful_LogicResult_When_Trap_is_called_with_a_void_activity_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = TestError().Trap(err => { actionExecuted = true; });

        AssertUnsuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_When_Trap_is_called_with_a_void_activity_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await Success().Async().Trap(err => { actionExecuted = true; });

        AssertActionNotExecuted(result, actionExecuted, true);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResult_When_Trap_is_called_with_a_void_activity_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestError().Async().Trap(err => { actionExecuted = true; });

        AssertUnsuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_LogicResult_When_Trap_is_called_with_an_async_void_activity_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await Success().Trap(err => { actionExecuted = true; return Task.CompletedTask; });

        AssertActionNotExecuted(result, actionExecuted, true);
    }

    [Test]
    public async Task Given_an_unsuccessful_LogicResult_When_Trap_is_called_with_an_async_void_activity_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestError().Trap(err => { actionExecuted = true; return Task.CompletedTask; });

        AssertUnsuccessfulAction(result, actionExecuted);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResult_When_Trap_is_called_with_an_async_void_activity_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await Success().Async().Trap(err => { actionExecuted = true; return Task.CompletedTask; });

        AssertActionNotExecuted(result, actionExecuted, true);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResult_When_Trap_is_called_with_an_async_void_activity_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestError().Async().Trap(err => { actionExecuted = true; return Task.CompletedTask; });

        AssertUnsuccessfulAction(result, actionExecuted);
    }
}

