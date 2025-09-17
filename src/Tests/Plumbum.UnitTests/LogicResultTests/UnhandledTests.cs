namespace Plumbum.UnitTests.LogicResultTests;

[TestFixture(TestOf = typeof(LogicResult))]
public class UnhandledTests
{
    [Test]
    public void Given_no_parameters_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.Null);
        Assert.That(err.ErrorMessage, Is.Null);
        Assert.That(err.Exception, Is.Null);
    }

    [Test]
    public void Given_an_errorCode_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled(errorCode: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo("ASDF"));
        Assert.That(err.EntityType, Is.Null);
        Assert.That(err.ErrorMessage, Is.Null);
        Assert.That(err.Exception, Is.Null);
    }

    [Test]
    public void Given_an_errorMessage_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled(errorMessage: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.Null);
        Assert.That(err.ErrorMessage, Is.EqualTo("ASDF"));
        Assert.That(err.Exception, Is.Null);
    }

    [Test]
    public void Given_an_entityType_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled(entityType: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.EqualTo("ASDF"));
        Assert.That(err.ErrorMessage, Is.Null);
        Assert.That(err.Exception, Is.Null);
    }

    [Test]
    public void Given_an_exception_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled(exception: new Exception("ASDF"));

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.Null);
        Assert.That(err.ErrorMessage, Is.Null);
        Assert.That(err.Exception, Is.Not.Null);
        Assert.That(err.Exception.Message, Is.EqualTo("ASDF"));
    }

    [Test]
    public void Given_a_Type_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled<TestType>();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is ILogicError<TestType>, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        Assert.That(result is IUnhandledLogicError<TestType>, Is.True);
        var err = (IUnhandledLogicError<TestType>)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.EqualTo(nameof(TestType)));
        Assert.That(err.ErrorMessage, Is.Null);
        Assert.That(err.Exception, Is.Null);
    }

    [Test]
    public void Given_an_errorCode_and_a_Type_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled<TestType>(errorCode: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is ILogicError<TestType>, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        Assert.That(result is IUnhandledLogicError<TestType>, Is.True);
        var err = (IUnhandledLogicError<TestType>)result;
        Assert.That(err.ErrorCode, Is.EqualTo("ASDF"));
        Assert.That(err.EntityType, Is.EqualTo(nameof(TestType)));
        Assert.That(err.ErrorMessage, Is.Null);
        Assert.That(err.Exception, Is.Null);
    }

    [Test]
    public void Given_an_errorMessage_and_a_Type_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled<TestType>(errorMessage: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is ILogicError<TestType>, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        Assert.That(result is IUnhandledLogicError<TestType>, Is.True);
        var err = (IUnhandledLogicError<TestType>)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.EqualTo(nameof(TestType)));
        Assert.That(err.ErrorMessage, Is.EqualTo("ASDF"));
        Assert.That(err.Exception, Is.Null);
    }

    [Test]
    public void Given_an_entityType_and_a_Type_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled<TestType>(entityType: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is ILogicError<TestType>, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        Assert.That(result is IUnhandledLogicError<TestType>, Is.True);
        var err = (IUnhandledLogicError<TestType>)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.EqualTo("ASDF"));
        Assert.That(err.ErrorMessage, Is.Null);
        Assert.That(err.Exception, Is.Null);
    }

    [Test]
    public void Given_an_exception_and_a_Type_parameter_When_LogicResult_Unhandled_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Unhandled<TestType>(exception: new Exception("ASDF"));

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.EqualTo(nameof(TestType)));
        Assert.That(err.ErrorMessage, Is.Null);
        Assert.That(err.Exception, Is.Not.Null);
        Assert.That(err.Exception.Message, Is.EqualTo("ASDF"));
    }

    [Test]
    public void Given_an_exception_When_Wrap_is_called_with_no_Type_parameter_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = new Exception("ASDF").Wrap();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.Null);
        Assert.That(err.ErrorMessage, Is.EqualTo("ASDF"));
        Assert.That(err.Exception, Is.Not.Null);
    }

    [Test]
    public void Given_an_exception_When_Wrap_is_called_with_a_Type_parameter_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = new Exception("ASDF").Wrap<TestType>();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is IUnhandledLogicError, Is.True);
        var err = (IUnhandledLogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unhandled));
        Assert.That(err.EntityType, Is.EqualTo(nameof(TestType)));
        Assert.That(err.ErrorMessage, Is.EqualTo("ASDF"));
        Assert.That(err.Exception, Is.Not.Null);
    }

    private class TestType { }
}
