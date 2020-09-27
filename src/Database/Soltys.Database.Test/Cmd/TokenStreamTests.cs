using System;
using System.Collections.Generic;
using Soltys.Library.TextAnalysis;
using Xunit;

namespace Soltys.Database.Test.Cmd
{
    public class TokenStreamTests
    {
        private TokenSource<CmdToken, CmdTokenKind> TokenStreamFactory(CmdToken[] tokens) =>
            new TokenSource<CmdToken, CmdTokenKind>(new TestTokenProvider(tokens));

        [Fact]
        internal void Current_WithGivenListAfterConstruction_ReturnsFirstToken()
        {
            var firstToken = new CmdToken(CmdTokenKind.Number, "3");
            var tokenStream = TokenStreamFactory(new[]
            {
                firstToken,
                new CmdToken(CmdTokenKind.Plus, "+"),
                new CmdToken(CmdTokenKind.Number, "2"),
            });

            Assert.Equal(tokenStream.Current.TokenKind, firstToken.TokenKind);
            Assert.Equal(tokenStream.Current.Value, firstToken.Value);
        }

        [Fact]
        internal void Current_WithEmptyTokenList_ReturnsTokenEmpty()
        {
            var tokenStream = TokenStreamFactory(Array.Empty<CmdToken>());
            Assert.Equal(tokenStream.Current.TokenKind, CmdToken.Empty.TokenKind);
            Assert.Equal(tokenStream.Current.Value, CmdToken.Empty.Value);
        }

        [Fact]
        internal void PeekNextToken_WithGivenListAfterConstruction_ReturnsSecondToken()
        {
            var secondToken = new CmdToken(CmdTokenKind.Plus, "+");
            var tokenStream = TokenStreamFactory(new[]
            {
                new CmdToken(CmdTokenKind.Number, "3"),
                secondToken,
                new CmdToken(CmdTokenKind.Number, "2"),
            });

            Assert.Equal(tokenStream.PeekNextToken.TokenKind, secondToken.TokenKind);
            Assert.Equal(tokenStream.PeekNextToken.Value, secondToken.Value);
        }

        [Fact]
        internal void PeekNextToken_WithEmptyTokenList_ReturnsTokenEmpty()
        {
            var tokenStream = TokenStreamFactory(Array.Empty<CmdToken>());
            Assert.Equal(tokenStream.PeekNextToken.TokenKind, CmdToken.Empty.TokenKind);
            Assert.Equal(tokenStream.PeekNextToken.Value, CmdToken.Empty.Value);
        }

        [Fact]
        internal void NextToken_ProgressIntoTokenList()
        {
            var secondToken = new CmdToken(CmdTokenKind.Plus, "+");
            var tokenStream = TokenStreamFactory(new[]
            {
                new CmdToken(CmdTokenKind.Number, "3"),
                secondToken,
                new CmdToken(CmdTokenKind.Number, "2"),
            });

            tokenStream.NextToken();
            Assert.Equal(tokenStream.Current.TokenKind, secondToken.TokenKind);
            Assert.Equal(tokenStream.Current.Value, secondToken.Value);
        }

        private class TestTokenProvider : ILexer<CmdToken>
        {
            private readonly CmdToken[] tokens;
            public TestTokenProvider(CmdToken[] tokens)
            {
                this.tokens = tokens;
            }
            public IEnumerable<CmdToken> GetTokens() => this.tokens;
            public CmdToken Empty => CmdToken.Empty;
        }
    }
}
