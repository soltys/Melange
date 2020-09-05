using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public class LexerTests
    {
        private static Lexer LexerFactory(string input) =>
            new Lexer(new CommandInput(input));

        [Fact]
        internal void Lexer_DoingInteger()
        {
            AssertLexer("42",
                new []
                {
                    new Token(TokenType.Number, "42"),
                });
        }

        [Fact]
        internal void Lexer_DoingFloat()
        {
            AssertLexer("42.69", 
                new[]
                {
                    new Token(TokenType.Number, "42.69"),
                });
        }

        [Fact]
        internal void Lexer_DoingStringsSingleQuote()
        {
            AssertLexer("'foobar'",
                new[]
                {
                    new Token(TokenType.String, "foobar"),
                });
        }

        [Fact]
        internal void Lexer_DoingSelectWithWhere()
        {
            AssertLexer("select * from pool where x>2+2*2",
                new[]
                {
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
                    new Token(TokenType.Number, "2"),
                });
        }

        [Fact]
        internal void Lexer_DoingInsertToGivenDictionary()
        {
            AssertLexer("insert foo=bar into dict",
                new []
                {
                    new Token(TokenType.Insert, "insert"),
                    new Token(TokenType.Id, "foo"),
                    new Token(TokenType.EqualSign, "="),
                    new Token(TokenType.Id, "bar"),
                    new Token(TokenType.Into, "into"),
                    new Token(TokenType.Id, "dict"),
                });
        }

        private static void AssertLexer(string input, IReadOnlyList<Token> expectedTokens)
        {
            var lexer = LexerFactory(input);
            var tokens = lexer.GetTokens().ToArray();

            Assert.Equal(expectedTokens.Count, tokens.Length);

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.Equal(expectedTokens[i].TokenType, tokens[i].TokenType);
                Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
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
