using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.PipeTests.TrapTests;

[TestFixture(TestOf = typeof(PipeExtensions))]
public class TLogicResultTTests
{
    // T => ILogicResult<T>

    [Test]
    public void Given_a_successful_LogicResultT_When_Trap_is_called_with_an_activity_that_returns_a_LogicResultT_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = SuccessT().Trap(err => { actionExecuted = true; return SuccessT(); });

        AssertActionNotExecuted(result, actionExecuted, true, TestTypes.T);
    }

    [Test]
    public void Given_an_unsuccessful_LogicResultT_When_Trap_is_called_with_an_activity_that_returns_a_LogicResultT_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = TestErrorT().Trap(err => { actionExecuted = true; return SuccessT(); });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResultT_When_Trap_is_called_with_an_activity_that_returns_a_LogicResultT_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await SuccessT().Async().Trap(err => { actionExecuted = true; return SuccessT(); });

        AssertActionNotExecuted(result, actionExecuted, true, TestTypes.T);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResultT_When_Trap_is_called_with_an_activity_that_returns_a_LogicResultT_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Async().Trap(err => { actionExecuted = true; return SuccessT(); });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
    }

    [Test]
    public async Task Given_a_successful_LogicResultT_When_Trap_is_called_with_an_async_activity_that_returns_a_LogicResultT_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await SuccessT().Trap(err => { actionExecuted = true; return SuccessT().Async(); });

        AssertActionNotExecuted(result, actionExecuted, true, TestTypes.T);
    }

    [Test]
    public async Task Given_an_unsuccessful_LogicResultT_When_Trap_is_called_with_an_async_activity_that_returns_a_LogicResultT_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Trap(err => { actionExecuted = true; return SuccessT().Async(); });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
    }

    [Test]
    public async Task Given_a_successful_async_LogicResultT_When_Trap_is_called_with_an_async_activity_that_returns_a_LogicResultT_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await SuccessT().Async().Trap(err => { actionExecuted = true; return SuccessT().Async(); });

        AssertActionNotExecuted(result, actionExecuted, true, TestTypes.T);
    }

    [Test]
    public async Task Given_an_unsuccessful_async_LogicResultT_When_Trap_is_called_with_an_async_activity_that_returns_a_LogicResultT_Then_activity_should_not_be_executed()
    {
        var actionExecuted = false;
        var result = await TestErrorT().Async().Trap(err => { actionExecuted = true; return SuccessT().Async(); });

        AssertSuccessfulAction(result, actionExecuted, TestTypes.T);
    }

}

