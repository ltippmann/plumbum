using static Plumbum.LogicResult;
using static Plumbum.UnitTests.TestHelper;

namespace Plumbum.UnitTests;

[TestFixture]
public class StructuralTests
{
    [Test]
    public void ILogicSuccess_should_be_ILogicResult()
    {
        var result = Success();
        Assert.That(result, Is.AssignableTo<ILogicSuccess>());
        Assert.That(result.Success, Is.True);
        Assert.That(result, Is.AssignableTo<ILogicResult>());
    }

    [Test]
    public void ILogicSuccessT_should_be_ILogicResultT()
    {
        var result = SuccessT();
        Assert.That(result, Is.AssignableTo<ILogicSuccess<TestTypeT>>());
        Assert.That(result.Success, Is.True);
        Assert.That(result, Is.AssignableTo<ILogicResult<TestTypeT>>());
    }

    [Test]
    public void ILogicSuccessT_should_be_ILogicSuccess()
    {
        var result = SuccessT();
        Assert.That(result, Is.AssignableTo<ILogicSuccess<TestTypeT>>());
        Assert.That(result.Success, Is.True);
        Assert.That(result, Is.AssignableTo<ILogicSuccess>());
    }

    [Test]
    public void ILogicError_should_be_ILogicResult()
    {
        var result = Error();
        Assert.That(result, Is.AssignableTo<ILogicError>());
        Assert.That(result.Success, Is.False);
        Assert.That(result, Is.AssignableTo<ILogicResult>());
    }

    [Test]
    public void ILogicErrorT_should_be_ILogicResultT()
    {
        var result = TestErrorT();
        Assert.That(result, Is.AssignableTo<ILogicError<TestTypeT>>());
        Assert.That(result.Success, Is.False);
        Assert.That(result, Is.AssignableTo<ILogicResult<TestTypeT>>());
    }

    [Test]
    public void ILogicErrorT_should_be_ILogicError()
    {
        var result = TestErrorT();
        Assert.That(result, Is.AssignableTo<ILogicError<TestTypeT>>());
        Assert.That(result.Success, Is.False);
        Assert.That(result, Is.AssignableTo<ILogicError>());
    }

    [Test]
    public void ILogicUnhandledError_should_be_ILogicError()
    {
        var result = Unhandled(exception: new Exception("ASDF"));
        Assert.That(result, Is.AssignableTo<IUnhandledLogicError>());
        Assert.That(result.Success, Is.False);
        Assert.That(result, Is.AssignableTo<ILogicError>());
    }

    [Test]
    public void ILogicUnhandledErrorT_should_be_ILogicResultT()
    {
        var result = Unhandled<TestTypeT>(exception: new Exception("ASDF"));
        Assert.That(result, Is.AssignableTo<IUnhandledLogicError<TestTypeT>>());
        Assert.That(result.Success, Is.False);
        Assert.That(result, Is.AssignableTo<ILogicResult<TestTypeT>>());
    }

    [Test]
    public void ILogicUnhandledErrorT_should_be_ILogicUnhandledError()
    {
        var result = Unhandled<TestTypeT>(exception: new Exception("ASDF"));
        Assert.That(result, Is.AssignableTo<IUnhandledLogicError<TestTypeT>>());
        Assert.That(result.Success, Is.False);
        Assert.That(result, Is.AssignableTo<ILogicError>());
    }

    [Test]
    public void ILogicSuccessT_should_have_a_value()
    {
        var value = new TestTypeT();
        var result = Success(value);
        Assert.That(result.Value, Is.SameAs(value));
    }

    [Test]
    public void ILogicErrorT_Value_should_throw_an_exception()
    {
        var result = Error<TestTypeT>();
        Assert.That(() => result.Value, Throws.Exception);
    }

    [Test]
    public void ILogicUnhandledErrorT_Value_should_throw_an_exception()
    {
        var result = Unhandled<TestTypeT>();
        Assert.That(() => result.Value, Throws.Exception);
    }
}
