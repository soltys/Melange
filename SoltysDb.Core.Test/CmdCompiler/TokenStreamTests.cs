using System;
using System.Collections.Generic;
using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class TokenStreamTests
    {
        TokenStream TokenStreamFactory(Token[] tokens) =>
            new TokenStream(new TestTokenProvider(tokens));

        [Fact]
        internal void Current_WithGivenListAfterConstruction_ReturnsFirstToken()
        {
            var firstToken = new Token(TokenType.Number, "3");
            var tokenStream = TokenStreamFactory(new[]
            {
                firstToken,
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.Number, "2"),
            });

            Assert.Equal(tokenStream.Current.TokenType, firstToken.TokenType);
            Assert.Equal(tokenStream.Current.Value, firstToken.Value);
        }

        [Fact]
        internal void Current_WithEmptyTokenList_ReturnsTokenEmpty()
        {
            var tokenStream = TokenStreamFactory(Array.Empty<Token>());
            Assert.Equal(tokenStream.Current.TokenType, Token.Empty.TokenType);
            Assert.Equal(tokenStream.Current.Value, Token.Empty.Value);
        }

        [Fact]
        internal void PeekNextToken_WithGivenListAfterConstruction_ReturnsSecondToken()
        {
            var secondToken = new Token(TokenType.Plus, "+");
            var tokenStream = TokenStreamFactory(new[]
            {
                new Token(TokenType.Number, "3"),
                secondToken,
                new Token(TokenType.Number, "2"),
            });

            Assert.Equal(tokenStream.PeekNextToken.TokenType, secondToken.TokenType);
            Assert.Equal(tokenStream.PeekNextToken.Value, secondToken.Value);
        }

        [Fact]
        internal void PeekNextToken_WithEmptyTokenList_ReturnsTokenEmpty()
        {
            var tokenStream =  TokenStreamFactory(Array.Empty<Token>());
            Assert.Equal(tokenStream.PeekNextToken.TokenType, Token.Empty.TokenType);
            Assert.Equal(tokenStream.PeekNextToken.Value, Token.Empty.Value);
        }

        [Fact]
        internal void NextToken_ProgressIntoTokenList()
        {
            var secondToken = new Token(TokenType.Plus, "+");
            var tokenStream = TokenStreamFactory(new[]
            {
                new Token(TokenType.Number, "3"),
                secondToken,
                new Token(TokenType.Number, "2"),
            });

            tokenStream.NextToken();
            Assert.Equal(tokenStream.Current.TokenType, secondToken.TokenType);
            Assert.Equal(tokenStream.Current.Value, secondToken.Value);
        }

        class TestTokenProvider : ILexer
        {
            private readonly Token[] tokens;

            public TestTokenProvider(Token[] tokens)
            {
                this.tokens = tokens;
            }

            public IEnumerable<Token> GetTokens() => this.tokens;
        }
    }
}
