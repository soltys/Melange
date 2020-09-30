using System;
using System.Linq;
using Soltys.Library.TextAnalysis;
using Soltys.Lisp.Compiler;
using Xunit;

namespace Soltys.Lisp.Test.Compiler
{
    public class LispLexerTests
    {
        [Fact]
        internal void Constructor_NullPassedAsArgument_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>("textSource", () => new LispLexer(null!));
        }

        [Theory]
        [InlineData("foo", LispTokenKind.Symbol)]
        [InlineData("+", LispTokenKind.Symbol)]
        [InlineData("-", LispTokenKind.Symbol)]
        [InlineData("*", LispTokenKind.Symbol)]
        [InlineData("/", LispTokenKind.Symbol)]
        [InlineData("=", LispTokenKind.Symbol)]
        [InlineData(">", LispTokenKind.Symbol)]
        [InlineData("<", LispTokenKind.Symbol)]
        [InlineData("def!", LispTokenKind.Symbol)]
        [InlineData("42", LispTokenKind.Number)]
        [InlineData("-42", LispTokenKind.Number)]
        [InlineData("42.69", LispTokenKind.Number)]
        [InlineData("-42.69", LispTokenKind.Number)]
        [InlineData("'", LispTokenKind.Quote)]
        [InlineData("(", LispTokenKind.LParen)]
        [InlineData(")", LispTokenKind.RParen)]
        internal void GetTokens_SingleTokenInputs(string input, LispTokenKind expectedTokenKind)
        {
            var lexer = new LispLexer(new TextSource(input));
            var token = lexer.GetTokens().Single();

            Assert.Equal(expectedTokenKind, token.TokenKind);
            Assert.Equal(input, token.Value);
        }

        [Fact]
        internal void GetTokens_TokenWithComment()
        {
            var lexer = new LispLexer(new TextSource("foo;bar"));
            var token = lexer.GetTokens().Single();

            Assert.Equal(LispTokenKind.Symbol, token.TokenKind);
            Assert.Equal("foo", token.Value);
        }

        [Fact]
        internal void GetTokens_String_ParsedCorrectly()
        {
            var lexer = new LispLexer(new TextSource("\"foobar\""));
            var token = lexer.GetTokens().Single();

            Assert.Equal("foobar", token.Value);
            Assert.Equal(LispTokenKind.String, token.TokenKind);
        }


        [Fact]
        internal void GetTokens_MultipleTokensInInput()
        {
            const string input = "(add (mul 2 2) 2)";

            var lexer = new LispLexer(new TextSource(input));
            var actualTokens = lexer.GetTokens().ToArray();

            var expectedTokens = new[] {
                new LispToken(LispTokenKind.LParen, "("),
                new LispToken(LispTokenKind.Symbol, "add"),
                new LispToken(LispTokenKind.LParen, "("),
                new LispToken(LispTokenKind.Symbol, "mul"),
                new LispToken(LispTokenKind.Number, "2"),
                new LispToken(LispTokenKind.Number, "2"),
                new LispToken(LispTokenKind.RParen, ")"),
                new LispToken(LispTokenKind.Number, "2"),
                new LispToken(LispTokenKind.RParen, ")"),
            };

            AssertTokens(expectedTokens, actualTokens);
        }

        private static void AssertTokens(LispToken[] expectedTokens, LispToken[] actualTokens)
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
}
