using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soltys.Library.TextAnalysis;
using Soltys.Library.VisualStudioSolution;
using Xunit;

namespace Soltys.Library.Test.VisualStudioSolution
{
    public class SolutionLexerTests
    {
        [Fact]
        internal void Constructor_NullPassedAsArgument_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>("textSource", () => new SolutionLexer(null!));
        }

        [Theory]
        [InlineData("foo", SolutionTokenKind.Id)]
        [InlineData("|", SolutionTokenKind.Pipe)]
        [InlineData("#", SolutionTokenKind.Hash)]
        [InlineData(",", SolutionTokenKind.Comma)]
        [InlineData("=", SolutionTokenKind.Equal)]
        [InlineData("(", SolutionTokenKind.LParen)]
        [InlineData(")", SolutionTokenKind.RParen)]
        [InlineData("12", SolutionTokenKind.Version)]
        [InlineData("22.20", SolutionTokenKind.Version)]
        [InlineData("32.21.550.2", SolutionTokenKind.Version)]
        [InlineData("{C67CD1BA-F675-4559-B1FD-A886315A2D1B}", SolutionTokenKind.Guid)]
        
        internal void GetTokens_SingleTokenInputs(string input, SolutionTokenKind expectedTokenKind)
        {
            var lexer = new SolutionLexer(new TextSource(input));
            var token = lexer.GetTokens().Single();

            Assert.Equal(expectedTokenKind, token.TokenKind);
            Assert.Equal(input, token.Value);
        }

        [Fact]
        internal void GetTokens_String_LexedCorrectly()
        {
            var lexer = new SolutionLexer(new TextSource("\"bar\""));
            var token = lexer.GetTokens().Single();

            Assert.Equal(SolutionTokenKind.String, token.TokenKind);
            Assert.Equal("bar", token.Value);
        }
    }
}
