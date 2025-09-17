using Plumbum.Exceptions;
using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.LogicResultTests;

[TestFixture(TestOf = typeof(LogicResult))]
public class NullCheckTests
{
    // void => void

    [Test]
    public void Given_a_null_void_activity_When_Try_is_called_with_no_handler_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Action)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_void_activity_When_Try_is_called_with_a_handler_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Action)null!, _ => Success()), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_void_activity_When_Try_is_called_with_a_null_handler_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try(() => { }, (Func<Exception, ILogicResult>)null!), Throws.TypeOf<HandleErrorNullException>());
    }

    [Test]
    public void Given_a_null_void_activity_When_Try_is_called_with_a_handler_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Action)null!, _ => Success().Async()), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_void_activity_When_Try_is_called_with_a_null_handler_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try(() => { }, (Func<Exception, Task<ILogicResult>>)null!), Throws.TypeOf<HandleErrorNullException>());
    }

    // async void => void

    [Test]
    public void Given_a_null_async_void_activity_When_Try_is_called_with_no_handler_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<Task>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_void_activity_When_Try_is_called_with_a_handler_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<Task>)null!, _ => Success()), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_an_async_void_activity_When_Try_is_called_with_a_null_handler_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try(() => Task.CompletedTask, (Func<Exception, ILogicResult>)null!), Throws.TypeOf<HandleErrorNullException>());
    }

    [Test]
    public void Given_a_null_async_void_activity_When_Try_is_called_with_a_handler_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<Task>)null!, _ => Success().Async()), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_an_async_void_activity_When_Try_is_called_with_a_null_handler_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try(() => Task.CompletedTask, (Func<Exception, Task<ILogicResult>>)null!), Throws.TypeOf<HandleErrorNullException>());
    }

    // void => T

    [Test]
    public void Given_a_null_activity_that_returns_a_value_When_Try_is_called_with_no_handler_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<TestTypeT>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_activity_that_returns_a_value_When_Try_is_called_with_a_handler_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<TestTypeT>)null!, _ => SuccessT()), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_activity_that_returns_a_value_When_Try_is_called_with_a_null_handler_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try(() => new TestTypeT(), (Func<Exception, ILogicResult<TestTypeT>>)null!), Throws.TypeOf<HandleErrorNullException>());
    }

    [Test]
    public void Given_a_null_activity_that_returns_a_value_When_Try_is_called_with_a_handler_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<TestTypeT>)null!, _ => SuccessT().Async()), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_activity_that_returns_a_value_When_Try_is_called_with_a_null_handler_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try(() => new TestTypeT(), (Func<Exception, Task<ILogicResult<TestTypeT>>>)null!), Throws.TypeOf<HandleErrorNullException>());
    }

    // async void => T

    [Test]
    public void Given_a_null_async_activity_that_returns_a_value_When_Try_is_called_with_no_handler_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<Task<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_activity_that_returns_a_value_When_Try_is_called_with_a_handler_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<Task<TestTypeT>>)null!, _ => SuccessT()), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_an_async_activity_that_returns_a_value_When_Try_is_called_with_a_null_handler_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try(() => Task.FromResult(new TestTypeT()), (Func<Exception, ILogicResult<TestTypeT>>)null!), Throws.TypeOf<HandleErrorNullException>());
    }

    [Test]
    public void Given_a_null_async_activity_that_returns_a_value_When_Try_is_called_with_a_handler_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try((Func<Task<TestTypeT>>)null!, _ => SuccessT().Async()), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_an_async_activity_that_returns_a_value_When_Try_is_called_with_a_null_handler_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Try(() => Task.FromResult(new TestTypeT()), (Func<Exception, Task<ILogicResult<TestTypeT>>>)null!), Throws.TypeOf<HandleErrorNullException>());
    }
}
