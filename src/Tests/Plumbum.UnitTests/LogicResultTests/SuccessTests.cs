namespace Plumbum.UnitTests.LogicResultTests;

[TestFixture(TestOf = typeof(LogicResult))]
public class SuccessTests
{
    [Test]
    public void Given_no_parameters_When_LogicResult_Success_is_called_Then_a_successful_LogicResult_should_be_returned()
    {
        var result = LogicResult.Success();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.True);
        Assert.That(result is ILogicResult, Is.True);
        Assert.That(result is ILogicSuccess, Is.True);
    }

    [Test]
    public void Given_a_value_parameter_When_LogicResult_Success_is_called_Then_a_successful_LogicResult_should_be_returned()
    {
        var result = LogicResult.Success("asdf");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Success, Is.True);
        Assert.That(result is ILogicResult, Is.True);
        Assert.That(result is ILogicResult<string>, Is.True);
        Assert.That(result is ILogicSuccess, Is.True);
        Assert.That(result is ILogicSuccess<string>, Is.True);
        Assert.That(result.Value, Is.EqualTo("asdf"));
    }
}
