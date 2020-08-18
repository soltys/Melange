using System.Diagnostics;
using System.Linq;
using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class LexerTests
    {
        private Lexer LexerFactory(string input) =>
            new Lexer(new CommandInput(input));

        [Theory]
        [ClassData(typeof(LexerTestsCasesProvider))]
        internal void GetTokens_RunAllTestCases(ILexerTestCase testCase)
        {
            if (testCase.IsBreakpointOn && Debugger.IsAttached)
            {
                Debugger.Break();
            }

            var lexer = LexerFactory(testCase.Input);
            var tokens = lexer.GetTokens().ToArray();

            Assert.Equal(testCase.ExpectedTokens.Length, tokens.Length);

            for (int i = 0; i < testCase.ExpectedTokens.Length; i++)
            {
                Assert.Equal(testCase.ExpectedTokens[i].TokenType, tokens[i].TokenType);
                Assert.Equal(testCase.ExpectedTokens[i].Value, tokens[i].Value);
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
