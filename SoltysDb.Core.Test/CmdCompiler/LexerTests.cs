using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class LexerTests
    {
        private readonly Func<string, Lexer> lexerFactory;

        public LexerTests()
        {
            this.lexerFactory = (input)
                => new Lexer(new CommandInput(input));
        }

        [Theory]
        [ClassData(typeof(LexerTestsCasesProvider))]
        public void GetTokens_RunAllTestCases(LexerTestCase testCase)
        {
            if (testCase.IsBreakpointOn && Debugger.IsAttached)
            {
                Debugger.Break();
            }

            var lexer = this.lexerFactory(testCase.Input);
            var tokens = lexer.GetTokens().ToArray();

            var expectedTokens = testCase.ExpectedTokens.Select(x => x.ToToken()).ToArray();

            Assert.Equal(
                expectedTokens.Select(x => x.TokenType),
                tokens.Select(x => x.TokenType));

            Assert.Equal(
                expectedTokens.Select(x => x.Value),
                tokens.Select(x => x.Value));
        }
    }
}
