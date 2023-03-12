using Soltys.Library.BQuery;
using Soltys.Library.TextAnalysis;
using Xunit;

namespace Soltys.Library.Test;

public class BQueryLexerTests
{
    [Fact]
    public void Constructor_NullQuery_RaiseArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new BQueryLexer(null));

    [Theory]
    [InlineData("foo", BQueryTokenKind.Id)]
    [InlineData("42", BQueryTokenKind.Number)]
    [InlineData(".", BQueryTokenKind.Dot)]
    [InlineData("[", BQueryTokenKind.LBracket)]
    [InlineData("]", BQueryTokenKind.RBracket)]
    internal void GetTokens_SingleTokenInputs(string input, BQueryTokenKind expectedTokenKind)
    {
        var lexer = new BQueryLexer(new TextSource(input));
        var token = lexer.GetTokens().Single();

        Assert.Equal(expectedTokenKind, token.TokenKind);
        Assert.Equal(input, token.Value);
    }

    [Theory]
    [InlineData("'foobar'", "foobar", BQueryTokenKind.String)]
    [InlineData("\"foobar\"", "foobar", BQueryTokenKind.String)]
    internal void GetTokens_SingleTokenInputs_String(string input, string expectedValue, BQueryTokenKind expectedTokenKind)
    {
        var lexer = new BQueryLexer(new TextSource(input));
        var token = lexer.GetTokens().Single();

        Assert.Equal(expectedTokenKind, token.TokenKind);
        Assert.Equal(expectedValue, token.Value);
    }

    [Fact]
    internal void GetTokens_StringAccess()
    {
        const string input = "['foo']";

        var lexer = new BQueryLexer(new TextSource(input));
        var actualTokens = lexer.GetTokens().ToArray();

        var expectedTokens = new[] {
            new BQueryToken(BQueryTokenKind.LBracket, "["), 
            new BQueryToken(BQueryTokenKind.String, "foo"),
            new BQueryToken(BQueryTokenKind.RBracket, "]"),
        };

        AssertTokens(expectedTokens, actualTokens);
    }

    [Fact]
    internal void GetTokens_MultipleTokensInInput()
    {
        const string input = "foo.bar.array[42].mike";

        var lexer = new BQueryLexer(new TextSource(input));
        var actualTokens = lexer.GetTokens().ToArray();

        var expectedTokens = new[] {
            new BQueryToken(BQueryTokenKind.Id, "foo"),
            new BQueryToken(BQueryTokenKind.Dot, "."),
            new BQueryToken(BQueryTokenKind.Id, "bar"),
            new BQueryToken(BQueryTokenKind.Dot, "."),
            new BQueryToken(BQueryTokenKind.Id, "array"),
            new BQueryToken(BQueryTokenKind.LBracket, "["),
            new BQueryToken(BQueryTokenKind.Number, "42"),
            new BQueryToken(BQueryTokenKind.RBracket, "]"),
            new BQueryToken(BQueryTokenKind.Dot, "."),
            new BQueryToken(BQueryTokenKind.Id, "mike"),
        };

        AssertTokens(expectedTokens, actualTokens);
    }

    private static void AssertTokens(BQueryToken[] expectedTokens, BQueryToken[] actualTokens)
    {
        Assert.Equal(expectedTokens.Length, actualTokens.Length);
        for (int i = 0; i < actualTokens.Length; i++)
        {
            var actualToken = actualTokens[i];
            var expectedToken = expectedTokens[i];
            Assert.True(expectedToken == actualToken, $"Difference between {expectedToken} != {actualToken}");
        }
    }
}
