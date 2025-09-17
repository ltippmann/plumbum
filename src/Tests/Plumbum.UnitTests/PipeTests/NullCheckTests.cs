using Plumbum.Exceptions;
using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests.PipeTests;

[TestFixture(TestOf = typeof(PipeExtensions))]
public class NullCheckTests
{
    // void => void

    [Test]
    public void Given_a_null_LogicResult_When_Pipe_is_called_with_a_void_activity_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult)null!).Pipe(() => { }), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResult_When_Pipe_is_called_with_a_null_Action_activity_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Pipe((Action)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_a_LogicResult_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult)null!).Pipe(() => Success()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Pipe((Func<ILogicResult>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // void => T

    [Test]
    public void Given_a_null_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_a_raw_value_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult)null!).Pipe(() => new TestTypeT()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_a_raw_value_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Pipe((Func<TestTypeT>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_a_LogicResultT_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult)null!).Pipe(() => SuccessT()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_a_LogicResultT_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Pipe((Func<ILogicResult<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // void => async void

    [Test]
    public void Given_a_null_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_an_async_void_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult)null!).Pipe(() => Task.CompletedTask), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_void_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Pipe((Func<Task>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_an_async_LogicResult_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult)null!).Pipe(() => Success().Async()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Pipe((Func<Task<ILogicResult>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // void => async T

    [Test]
    public void Given_a_null_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_an_async_raw_value_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult)null!).Pipe(() => Task.FromResult(new TestTypeT())), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_raw_value_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Pipe((Func<Task<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_an_async_LogicResultT_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult)null!).Pipe(() => SuccessT().Async()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_LogicResultT_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Pipe((Func<Task<ILogicResult<TestTypeT>>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // T => void

    [Test]
    public void Given_a_null_LogicResultT_When_Pipe_is_called_with_a_void_activity_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(() => { }), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResultT_When_Pipe_is_called_with_a_null_Action_activity_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Pipe((Action)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_a_LogicResult_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(() => Success()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Pipe((Func<ILogicResult>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // T => T

    [Test]
    public void Given_a_null_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_a_raw_value_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(_ => new TestTypeT()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_a_raw_value_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Pipe((Func<TestTypeT, TestTypeT>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_a_LogicResultT_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(_ => SuccessT()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_a_LogicResultT_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Pipe((Func<TestTypeT, ILogicResult<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // T => async void

    [Test]
    public void Given_a_null_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_an_async_void_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(_ => Task.CompletedTask), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_void_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Pipe((Func<TestTypeT, Task>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_an_async_LogicResult_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(_ => Success().Async()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Pipe((Func<TestTypeT, Task<ILogicResult>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // T => async T

    [Test]
    public void Given_a_null_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_an_async_raw_value_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(_ => Task.FromResult(new TestTypeT())), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_raw_value_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Pipe((Func<TestTypeT, Task<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_an_async_LogicResultT_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(_ => SuccessT().Async()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_LogicResultT_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Pipe((Func<TestTypeT, Task<ILogicResult<TestTypeT>>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // async void => void

    [Test]
    public void Given_a_null_async_LogicResult_When_Pipe_is_called_with_a_void_activity_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult>)null!).Pipe(() => { }), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResult_When_Pipe_is_called_with_a_null_Action_activity_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Async().Pipe((Action)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_a_LogicResult_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult>)null!).Pipe(() => Success()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Async().Pipe((Func<ILogicResult>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // async void => T

    [Test]
    public void Given_a_null_async_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_a_raw_value_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult>)null!).Pipe(() => new TestTypeT()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_a_raw_value_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Async().Pipe((Func<TestTypeT>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_a_LogicResultT_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult>)null!).Pipe(() => SuccessT()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_a_LogicResultT_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Async().Pipe((Func<ILogicResult<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // async void => async void

    [Test]
    public void Given_a_null_async_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_an_async_void_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult>)null!).Pipe(() => Task.CompletedTask), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_void_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Async().Pipe((Func<Task>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_an_async_LogicResult_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult>)null!).Pipe(() => Success().Async()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Async().Pipe((Func<Task<ILogicResult>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // async void => async T

    [Test]
    public void Given_a_null_async_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_an_async_raw_value_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult>)null!).Pipe(() => Task.FromResult(new TestTypeT())), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_raw_value_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Async().Pipe((Func<Task<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_LogicResult_When_Pipe_is_called_with_an_activity_that_returns_an_async_LogicResultT_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult>)null!).Pipe(() => SuccessT().Async()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResult_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_LogicResultT_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => Success().Async().Pipe((Func<Task<ILogicResult<TestTypeT>>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // async T => void

    [Test]
    public void Given_a_null_async_LogicResultT_When_Pipe_is_called_with_a_void_activity_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult<TestTypeT>>)null!).Pipe(_ => { }), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResultT_When_Pipe_is_called_with_a_null_Action_activity_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Async().Pipe((Action<TestTypeT>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_a_LogicResult_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((ILogicResult<TestTypeT>)null!).Pipe(() => Success()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_a_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Async().Pipe((Func<TestTypeT, ILogicResult>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // async T => T

    [Test]
    public void Given_a_null_async_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_a_raw_value_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult<TestTypeT>>)null!).Pipe(_ => new TestTypeT()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_a_raw_value_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Async().Pipe((Func<TestTypeT, TestTypeT>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_a_LogicResultT_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult<TestTypeT>>)null!).Pipe(_ => SuccessT()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_a_LogicResultT_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Async().Pipe((Func<TestTypeT, ILogicResult<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // async T => async void

    [Test]
    public void Given_a_null_async_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_an_async_void_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult<TestTypeT>>)null!).Pipe(_ => Task.CompletedTask), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_void_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Async().Pipe((Func<TestTypeT, Task>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_an_async_LogicResult_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult<TestTypeT>>)null!).Pipe(_ => Success().Async()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_LogicResult_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Async().Pipe((Func<TestTypeT, Task<ILogicResult>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    // async T => async T

    [Test]
    public void Given_a_null_async_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_an_async_raw_value_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult<TestTypeT>>)null!).Pipe(_ => Task.FromResult(new TestTypeT())), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_raw_value_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Async().Pipe((Func<TestTypeT, Task<TestTypeT>>)null!), Throws.TypeOf<ActivityNullException>());
    }

    [Test]
    public void Given_a_null_async_LogicResultT_When_Pipe_is_called_with_an_activity_that_returns_an_async_LogicResultT_Then_an_InputNullException_should_be_thrown()
    {
        Assert.That(() => ((Task<ILogicResult<TestTypeT>>)null!).Pipe(_ => SuccessT().Async()), Throws.TypeOf<InputNullException>());
    }

    [Test]
    public void Given_a_successful_async_LogicResultT_When_Pipe_is_called_with_a_null_activity_that_returns_an_async_LogicResultT_Then_an_ActivityNullException_should_be_thrown()
    {
        Assert.That(() => SuccessT().Async().Pipe((Func<TestTypeT, Task<ILogicResult<TestTypeT>>>)null!), Throws.TypeOf<ActivityNullException>());
    }


}
