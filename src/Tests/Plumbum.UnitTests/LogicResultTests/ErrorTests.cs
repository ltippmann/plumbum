namespace Plumbum.UnitTests.LogicResultTests;

[TestFixture(TestOf = typeof(LogicResult))]
public class ErrorTests
{
    [Test]
    public void Given_no_parameters_When_LogicResult_Error_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Error();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        var err = (ILogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unknown));
        Assert.That(err.EntityType, Is.Null);
        Assert.That(err.ErrorMessage, Is.Null);
    }

    [Test]
    public void Given_an_errorCode_parameter_When_LogicResult_Error_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Error(errorCode: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        var err = (ILogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo("ASDF"));
        Assert.That(err.EntityType, Is.Null);
        Assert.That(err.ErrorMessage, Is.Null);
    }

    [Test]
    public void Given_an_errorMessage_parameter_When_LogicResult_Error_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Error(errorMessage: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        var err = (ILogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unknown));
        Assert.That(err.EntityType, Is.Null);
        Assert.That(err.ErrorMessage, Is.EqualTo("ASDF"));
    }

    [Test]
    public void Given_an_entityType_parameter_When_LogicResult_Error_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Error(entityType: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        var err = (ILogicError)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unknown));
        Assert.That(err.EntityType, Is.EqualTo("ASDF"));
        Assert.That(err.ErrorMessage, Is.Null);
    }

    [Test]
    public void Given_Type_parameter_When_LogicResult_Error_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Error<TestType>();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is ILogicError<TestType>, Is.True);
        var err = (ILogicError<TestType>)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unknown));
        Assert.That(err.EntityType, Is.EqualTo(nameof(TestType)));
        Assert.That(err.ErrorMessage, Is.Null);
    }

    [Test]
    public void Given_an_errorCode_and_a_Type_parameter_When_LogicResult_Error_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Error<TestType>(errorCode: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is ILogicError<TestType>, Is.True);
        var err = (ILogicError<TestType>)result;
        Assert.That(err.ErrorCode, Is.EqualTo("ASDF"));
        Assert.That(err.EntityType, Is.EqualTo(nameof(TestType)));
        Assert.That(err.ErrorMessage, Is.Null);
    }

    [Test]
    public void Given_an_errorMessage_and_a_Type_parameter_When_LogicResult_Error_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Error<TestType>(errorMessage: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is ILogicError<TestType>, Is.True);
        var err = (ILogicError<TestType>)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unknown));
        Assert.That(err.EntityType, Is.EqualTo(nameof(TestType)));
        Assert.That(err.ErrorMessage, Is.EqualTo("ASDF"));
    }

    [Test]
    public void Given_an_entityType_and_a_Type_parameter_When_LogicResult_Error_is_called_Then_the_correct_ILogicError_should_be_returned()
    {
        var result = LogicResult.Error<TestType>(entityType: "ASDF");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.False);
        Assert.That(result is ILogicError, Is.True);
        Assert.That(result is ILogicError<TestType>, Is.True);
        var err = (ILogicError<TestType>)result;
        Assert.That(err.ErrorCode, Is.EqualTo(ErrorCodes.Unknown));
        Assert.That(err.EntityType, Is.EqualTo("ASDF"));
        Assert.That(err.ErrorMessage, Is.Null);
    }

    private class TestType { }
}
