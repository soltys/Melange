using System;
using System.Linq;
using SoltysLib.Bson.BQuery;
using Xunit;

namespace SoltysLib.Test.Bson
{
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
            var lexer = new BQueryLexer(input);
            var token = lexer.GetTokens().Single();

            Assert.Equal(expectedTokenKind, token.TokenType);
            Assert.Equal(input, token.Value);
        }

        [Fact]
        internal void GetTokens_MultipleTokensInInput()
        {
            const string input = "foo.bar.array[42].mike";

            var lexer = new BQueryLexer(input);
            var tokens = lexer.GetTokens();

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

            Assert.Equal(expectedTokens, tokens);
        }
    }
}
