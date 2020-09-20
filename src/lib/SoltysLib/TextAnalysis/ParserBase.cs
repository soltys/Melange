using System;
using System.Linq;

namespace SoltysLib.TextAnalysis
{
    public class ParserBase<TToken, TTokenKind> where TToken: IToken<TTokenKind>
    {
        protected readonly ITokenSource<TToken, TTokenKind> ts;

        public ParserBase(ITokenSource<TToken, TTokenKind> ts)
        {
            this.ts = ts;
        }

        protected TTokenKind CurrentToken => this.ts.Current.TokenKind;
        protected TTokenKind NextToken => this.ts.PeekNextToken.TokenKind;

        protected void AdvanceToken(params TTokenKind[] tokens)
        {
            if (tokens != null && !IsToken(tokens))
            {
                throw new InvalidOperationException("Not expected token advancement");
            }
            this.ts.NextToken();
        }
        protected bool IsToken(params TTokenKind[] tokens) => tokens.Any(x => x.Equals(CurrentToken));
        protected bool IsNextToken(params TTokenKind[] tokens) => tokens.Any(x => x.Equals(NextToken));

        protected void AdvanceIfToken(params TTokenKind[] tokens)
        {
            if (IsToken(tokens))
            {
                AdvanceToken(tokens);
            }
        }
    }
}
