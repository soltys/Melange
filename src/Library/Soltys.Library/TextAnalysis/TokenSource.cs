using System.Linq;

namespace Soltys.Library.TextAnalysis
{
    public class TokenSource<TToken, TTokenKind> : ITokenSource<TToken, TTokenKind> where TToken : IToken<TTokenKind>
    {
        private readonly ILexer<TToken> lexer;
        private readonly TToken[] allTokens;
        private int tokenIndex;

        public TokenSource(ILexer<TToken> lexer)
        {
            this.lexer = lexer;
            this.allTokens = this.lexer.GetTokens().ToArray();
            this.tokenIndex = 0;
        }

        public TToken Current
        {
            get 
            {
                if (this.tokenIndex >= this.allTokens.Length)
                {
                    return this.lexer.Empty;
                }
                return this.allTokens[this.tokenIndex];
            }
        }

        public TToken PeekNextToken
        {
            get 
            {
                if (this.tokenIndex + 1 >= this.allTokens.Length)
                {
                    return this.lexer.Empty;
                }
                return this.allTokens[this.tokenIndex + 1];
            }
        }

        public void NextToken() => this.tokenIndex += 1;
    }
}
