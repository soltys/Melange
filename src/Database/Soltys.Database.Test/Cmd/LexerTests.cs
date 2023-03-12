using Soltys.Library.TextAnalysis;
using Xunit;

namespace Soltys.Database.Test.Cmd;

public class LexerTests
{
    [Theory]
    [InlineData("42", CmdTokenKind.Number)]
    [InlineData("42.69", CmdTokenKind.Number)]
    [InlineData(">", CmdTokenKind.GreaterThan)]
    [InlineData(">=", CmdTokenKind.GreaterThanEqual)]
    [InlineData("<", CmdTokenKind.LessThan)]
    [InlineData("<=", CmdTokenKind.LessThanEqual)]
    [InlineData("=", CmdTokenKind.EqualSign)]
    [InlineData("==", CmdTokenKind.CompareEqual)]
    [InlineData("!=", CmdTokenKind.CompareNotEqual)]
    [InlineData(",", CmdTokenKind.Comma)]
    [InlineData("soltysdb_name", CmdTokenKind.Id)]
    internal void Lexer_OneTokenInInput_TokenRecognized(string input, CmdTokenKind expectedCmdToken) =>
        AssertLexer(input,
            new CmdToken(expectedCmdToken, input));

    [Fact]
    internal void Lexer_DoingStringsSingleQuote() =>
        AssertLexer("'foobar'",
            new CmdToken(CmdTokenKind.String, "foobar"));

    [Fact]
    internal void Lexer_DoingStringsDoubleQuote() =>
        AssertLexer("\"foobar\"",
            new CmdToken(CmdTokenKind.String, "foobar"));

    [Theory]
    [InlineData("\"foo\"+\"bar\"")]
    [InlineData("'foo'+'bar'")]
    internal void Lexer_StringConcatenation(string input) =>
        AssertLexer(input,
            new CmdToken(CmdTokenKind.String, "foo"),
            new CmdToken(CmdTokenKind.Plus, "+"),
            new CmdToken(CmdTokenKind.String, "bar"));

    [Fact]
    internal void Lexer_DoingSelectWithWhere() =>
        AssertLexer("select * from pool where x>2+2*2",
            new CmdToken(CmdTokenKind.Select, "select"),
            new CmdToken(CmdTokenKind.Star, "*"),
            new CmdToken(CmdTokenKind.From, "from"),
            new CmdToken(CmdTokenKind.Id, "pool"),
            new CmdToken(CmdTokenKind.Where, "where"),
            new CmdToken(CmdTokenKind.Id, "x"),
            new CmdToken(CmdTokenKind.GreaterThan, ">"),
            new CmdToken(CmdTokenKind.Number, "2"),
            new CmdToken(CmdTokenKind.Plus, "+"),
            new CmdToken(CmdTokenKind.Number, "2"),
            new CmdToken(CmdTokenKind.Star, "*"),
            new CmdToken(CmdTokenKind.Number, "2"));

    [Fact]
    internal void Lexer_DoingInsertToGivenDictionary() =>
        AssertLexer("insert foo=bar into dict",
            new CmdToken(CmdTokenKind.Insert, "insert"),
            new CmdToken(CmdTokenKind.Id, "foo"),
            new CmdToken(CmdTokenKind.EqualSign, "="),
            new CmdToken(CmdTokenKind.Id, "bar"),
            new CmdToken(CmdTokenKind.Into, "into"),
            new CmdToken(CmdTokenKind.Id, "dict"));

    [Fact]
    internal void Lexer_DoingAssignmentWithEqual() =>
        AssertLexer("x = y == 42",
            new CmdToken(CmdTokenKind.Id, "x"),
            new CmdToken(CmdTokenKind.EqualSign, "="),
            new CmdToken(CmdTokenKind.Id, "y"),
            new CmdToken(CmdTokenKind.CompareEqual, "=="),
            new CmdToken(CmdTokenKind.Number, "42"));

    private static Lexer LexerFactory(string input) =>
        new Lexer(new TextSource(input));

    private static void AssertLexer(string input, params CmdToken[] expectedTokens)
    {
        var lexer = LexerFactory(input);
        var tokens = lexer.GetTokens().ToArray();

        Assert.True(expectedTokens.Length == tokens.Length, "The length of actual tokens and expected is different");

        for (int i = 0; i < expectedTokens.Length; i++)
        {
            Assert.Equal(expectedTokens[i], tokens[i]);
        }
    }

    [Theory]
    [ClassData(typeof(InsensitiveKeywordGenerator))]
    internal void GetTokens_Keyword_AreCaseInsensitiveRecognized(InputTokenTypePair testCase)
    {
        var lexer = LexerFactory(testCase.Input);
        var tokens = lexer.GetTokens().ToArray();

        Assert.Single(tokens);
        Assert.Equal(testCase.ExpectedCmdTokenKind, tokens[0].TokenKind);
        Assert.Equal(testCase.Input, tokens[0].Value);
    }
}
