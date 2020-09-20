using System;
using System.Collections.Generic;
using SoltysLib.TextAnalysis;
using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public class TokenStreamTests
    {
        private TokenSource<Token, TokenKind> TokenStreamFactory(Token[] tokens) =>
            new TokenSource<Token, TokenKind>(new TestTokenProvider(tokens));

        [Fact]
        internal void Current_WithGivenListAfterConstruction_ReturnsFirstToken()
        {
            var firstToken = new Token(TokenKind.Number, "3");
            var tokenStream = TokenStreamFactory(new[]
            {
                firstToken,
                new Token(TokenKind.Plus, "+"),
                new Token(TokenKind.Number, "2"),
            });

            Assert.Equal(tokenStream.Current.TokenKind, firstToken.TokenKind);
            Assert.Equal(tokenStream.Current.Value, firstToken.Value);
        }

        [Fact]
        internal void Current_WithEmptyTokenList_ReturnsTokenEmpty()
        {
            var tokenStream = TokenStreamFactory(Array.Empty<Token>());
            Assert.Equal(tokenStream.Current.TokenKind, Token.Empty.TokenKind);
            Assert.Equal(tokenStream.Current.Value, Token.Empty.Value);
        }

        [Fact]
        internal void PeekNextToken_WithGivenListAfterConstruction_ReturnsSecondToken()
        {
            var secondToken = new Token(TokenKind.Plus, "+");
            var tokenStream = TokenStreamFactory(new[]
            {
                new Token(TokenKind.Number, "3"),
                secondToken,
                new Token(TokenKind.Number, "2"),
            });

            Assert.Equal(tokenStream.PeekNextToken.TokenKind, secondToken.TokenKind);
            Assert.Equal(tokenStream.PeekNextToken.Value, secondToken.Value);
        }

        [Fact]
        internal void PeekNextToken_WithEmptyTokenList_ReturnsTokenEmpty()
        {
            var tokenStream = TokenStreamFactory(Array.Empty<Token>());
            Assert.Equal(tokenStream.PeekNextToken.TokenKind, Token.Empty.TokenKind);
            Assert.Equal(tokenStream.PeekNextToken.Value, Token.Empty.Value);
        }

        [Fact]
        internal void NextToken_ProgressIntoTokenList()
        {
            var secondToken = new Token(TokenKind.Plus, "+");
            var tokenStream = TokenStreamFactory(new[]
            {
                new Token(TokenKind.Number, "3"),
                secondToken,
                new Token(TokenKind.Number, "2"),
            });

            tokenStream.NextToken();
            Assert.Equal(tokenStream.Current.TokenKind, secondToken.TokenKind);
            Assert.Equal(tokenStream.Current.Value, secondToken.Value);
        }

        private class TestTokenProvider : ILexer<Token>
        {
            private readonly Token[] tokens;
            public TestTokenProvider(Token[] tokens)
            {
                this.tokens = tokens;
            }
            public IEnumerable<Token> GetTokens() => this.tokens;
            public Token Empty => Token.Empty;
        }
    }
}
