using System.Linq;
using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public class LexerTests
    {
        [Theory]
        [InlineData("42", TokenType.Number)]
        [InlineData("42.69", TokenType.Number)]
        [InlineData(">", TokenType.GreaterThan)]
        [InlineData(">=", TokenType.GreaterThanEqual)]
        [InlineData("<", TokenType.LessThan)]
        [InlineData("<=", TokenType.LessThanEqual)]
        [InlineData("=", TokenType.EqualSign)]
        [InlineData("==", TokenType.CompareEqual)]
        [InlineData("!=", TokenType.CompareNotEqual)]
        [InlineData(",", TokenType.Comma)]
        [InlineData("soltysdb_name", TokenType.Id)]
        internal void Lexer_OneTokenInInput_TokenRecognized(string input, TokenType expectedToken) =>
            AssertLexer(input,
                new Token(expectedToken, input));

        [Fact]
        internal void Lexer_DoingStringsSingleQuote() =>
            AssertLexer("'foobar'",
                new Token(TokenType.String, "foobar"));

        [Fact]
        internal void Lexer_DoingStringsDoubleQuote() =>
            AssertLexer("\"foobar\"",
                new Token(TokenType.String, "foobar"));

        [Fact]
        internal void Lexer_DoingSelectWithWhere() =>
            AssertLexer("select * from pool where x>2+2*2",
                new Token(TokenType.Select, "select"),
                new Token(TokenType.Star, "*"),
                new Token(TokenType.From, "from"),
                new Token(TokenType.Id, "pool"),
                new Token(TokenType.Where, "where"),
                new Token(TokenType.Id, "x"),
                new Token(TokenType.GreaterThan, ">"),
                new Token(TokenType.Number, "2"),
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.Number, "2"),
                new Token(TokenType.Star, "*"),
                new Token(TokenType.Number, "2"));

        [Fact]
        internal void Lexer_DoingInsertToGivenDictionary() =>
            AssertLexer("insert foo=bar into dict",
                new Token(TokenType.Insert, "insert"),
                new Token(TokenType.Id, "foo"),
                new Token(TokenType.EqualSign, "="),
                new Token(TokenType.Id, "bar"),
                new Token(TokenType.Into, "into"),
                new Token(TokenType.Id, "dict"));

        [Fact]
        internal void Lexer_DoingAssignmentWithEqual() =>
            AssertLexer("x = y == 42",
                new Token(TokenType.Id, "x"),
                new Token(TokenType.EqualSign, "="),
                new Token(TokenType.Id, "y"),
                new Token(TokenType.CompareEqual, "=="),
                new Token(TokenType.Number, "42"));

        private static Lexer LexerFactory(string input) =>
            new Lexer(new CommandInput(input));

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
            Assert.Equal(testCase.ExpectedTokenType, tokens[0].TokenType);
            Assert.Equal(testCase.Input, tokens[0].Value);
        }
    }
}
