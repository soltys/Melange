using System.Linq;

namespace SoltysDb
{
    internal class TokenSource : ITokenSource
    {
        private readonly ILexer lexer;
        private readonly Token[] allTokens;
        private int tokenIndex;

        public TokenSource(ILexer lexer)
        {
            this.lexer = lexer;
            this.allTokens = this.lexer.GetTokens().ToArray();
            this.tokenIndex = 0;
        }

        public Token Current
        {
            get
            {
                if (this.tokenIndex >= this.allTokens.Length)
                {
                    return Token.Empty;
                }
                return this.allTokens[this.tokenIndex];
            }
        }

        public Token PeekNextToken
        {
            get
            {
                if (this.tokenIndex + 1 >= this.allTokens.Length)
                {
                    return Token.Empty;
                }
                return this.allTokens[this.tokenIndex + 1];
            }
        }

        public void NextToken() => this.tokenIndex += 1;
    }
}
