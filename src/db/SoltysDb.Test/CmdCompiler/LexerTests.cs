using System.Linq;
using SoltysLib.TextAnalysis;
using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public class LexerTests
    {
        [Theory]
        [InlineData("42", TokenKind.Number)]
        [InlineData("42.69", TokenKind.Number)]
        [InlineData(">", TokenKind.GreaterThan)]
        [InlineData(">=", TokenKind.GreaterThanEqual)]
        [InlineData("<", TokenKind.LessThan)]
        [InlineData("<=", TokenKind.LessThanEqual)]
        [InlineData("=", TokenKind.EqualSign)]
        [InlineData("==", TokenKind.CompareEqual)]
        [InlineData("!=", TokenKind.CompareNotEqual)]
        [InlineData(",", TokenKind.Comma)]
        [InlineData("soltysdb_name", TokenKind.Id)]
        internal void Lexer_OneTokenInInput_TokenRecognized(string input, TokenKind expectedToken) =>
            AssertLexer(input,
                new Token(expectedToken, input));

        [Fact]
        internal void Lexer_DoingStringsSingleQuote() =>
            AssertLexer("'foobar'",
                new Token(TokenKind.String, "foobar"));

        [Fact]
        internal void Lexer_DoingStringsDoubleQuote() =>
            AssertLexer("\"foobar\"",
                new Token(TokenKind.String, "foobar"));

        [Theory]
        [InlineData("\"foo\"+\"bar\"")]
        [InlineData("'foo'+'bar'")]
        internal void Lexer_StringConcatenation(string input) =>
            AssertLexer(input,
                new Token(TokenKind.String, "foo"),
                new Token(TokenKind.Plus, "+"),
                new Token(TokenKind.String, "bar"));

        [Fact]
        internal void Lexer_DoingSelectWithWhere() =>
            AssertLexer("select * from pool where x>2+2*2",
                new Token(TokenKind.Select, "select"),
                new Token(TokenKind.Star, "*"),
                new Token(TokenKind.From, "from"),
                new Token(TokenKind.Id, "pool"),
                new Token(TokenKind.Where, "where"),
                new Token(TokenKind.Id, "x"),
                new Token(TokenKind.GreaterThan, ">"),
                new Token(TokenKind.Number, "2"),
                new Token(TokenKind.Plus, "+"),
                new Token(TokenKind.Number, "2"),
                new Token(TokenKind.Star, "*"),
                new Token(TokenKind.Number, "2"));

        [Fact]
        internal void Lexer_DoingInsertToGivenDictionary() =>
            AssertLexer("insert foo=bar into dict",
                new Token(TokenKind.Insert, "insert"),
                new Token(TokenKind.Id, "foo"),
                new Token(TokenKind.EqualSign, "="),
                new Token(TokenKind.Id, "bar"),
                new Token(TokenKind.Into, "into"),
                new Token(TokenKind.Id, "dict"));

        [Fact]
        internal void Lexer_DoingAssignmentWithEqual() =>
            AssertLexer("x = y == 42",
                new Token(TokenKind.Id, "x"),
                new Token(TokenKind.EqualSign, "="),
                new Token(TokenKind.Id, "y"),
                new Token(TokenKind.CompareEqual, "=="),
                new Token(TokenKind.Number, "42"));

        private static Lexer LexerFactory(string input) =>
            new Lexer(new TextSource(input));

        private static void AssertLexer(string input, params Token[] expectedTokens)
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
            Assert.Equal(testCase.ExpectedTokenKind, tokens[0].TokenKind);
            Assert.Equal(testCase.Input, tokens[0].Value);
        }
    }
}
