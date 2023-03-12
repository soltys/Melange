using Xunit;

namespace Soltys.Library.Test;

public class ResultTests
{
    [Fact]
    public void Constructor_GivingValue_ValueHoldGivenValueAndIsSuccess()
    {
        const int resultValue = 420;
        var result = new Result<int>(resultValue);
        Assert.Equal(resultValue, result.Value);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
    }

    [Fact]
    public void Add_AddingErrorToReasonList_MakesResultAFailure()
    {
        var result = new Result<int>(420);
        result.Add(new Error("foobar"));
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Empty(result.Successes);
        Assert.Single(result.Errors);
        Assert.Equal("foobar", result.Errors.First().Message);
    }

    [Fact]
    public void Add_AddingSuccessToReasonList_ResultStaysAsSuccess()
    {
        var result = new Result<int>(420);
        result.Add(new Success("foobar"));
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
        Assert.Single(result.Successes);
        Assert.Equal("foobar", result.Successes.First().Message);
    }
    [Fact]
    public void StaticFail_SetsReason()
    {
        var result = Result<int>.Fail("foobar");
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Empty(result.Successes);
        Assert.Single(result.Errors);
        Assert.Equal("foobar", result.Errors.First().Message);
    }
}
