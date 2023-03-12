using Xunit;

namespace Soltys.Database.Test.Cmd;

public class TokenTests
{
    public static IEnumerable<object[]> EqualOperatorData =>
        new List<object[]>
        {
            new object[] { new CmdToken(CmdTokenKind.Dot,"."), new CmdToken(CmdTokenKind.Dot, "."), true },
            new object[] { new CmdToken(CmdTokenKind.Dot, "."), new CmdToken(CmdTokenKind.Plus, "+"), false },
            new object[] { new CmdToken(CmdTokenKind.Id, "123"), new CmdToken(CmdTokenKind.Id, "abc"), false },
            new object[] { null, new CmdToken(CmdTokenKind.Dot, "."), false },
            new object[] { new CmdToken(CmdTokenKind.Dot, "."), null, false },
            new object[] { null, null, true },
        };

    [Theory]
    [MemberData(nameof(EqualOperatorData))]
    internal void EqualOperator_EqualObjects_ReturnsExpectedValue(CmdToken lhs, CmdToken rhs, bool expectedValue)
        => Assert.Equal(expectedValue, lhs == rhs);

    [Fact]
    public void Equals_WithNull_ReturnsFalse() => Assert.False(new CmdToken(CmdTokenKind.Dot, ".").Equals(null));

    [Fact]
    public void Token_Holds_Value()
    {
        var token = new CmdToken(CmdTokenKind.Insert, "insert");

        Assert.Equal(CmdTokenKind.Insert, token.TokenKind);
        Assert.Equal("insert", token.Value);
    }

    [Fact]
    public void Token_Default_Values()
    {
        var token = new CmdToken();
        Assert.Equal(CmdTokenKind.Undefined, token.TokenKind);
        Assert.Null(token.Value);
    }

    [Fact]
    public void ToString_HasProperFormat()
    {
        var token = new CmdToken(CmdTokenKind.Id, "Value");
        Assert.Equal("<Id,Value>", token.ToString());
    }
}
