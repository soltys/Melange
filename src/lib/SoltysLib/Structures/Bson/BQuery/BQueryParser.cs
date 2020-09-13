using System;
using System.Linq;
using SoltysLib.TextAnalysis;

namespace SoltysLib.Bson.BQuery
{
    internal class BQueryParser : IBQueryParser
    {
        private ITokenSource<BQueryToken> ts;
        private BQueryTokenKind CurrentToken => this.ts.Current.TokenType;

        private void AdvanceToken(params BQueryTokenKind[] tokens)
        {
            if (tokens != null && !IsToken(tokens))
            {
                throw new InvalidOperationException("Not expected token advancement");
            }
            this.ts.NextToken();
        }
        private bool IsToken(params BQueryTokenKind[] tokens) => tokens.Any(x => x == CurrentToken);
        public BQueryParser(ITokenSource<BQueryToken> tokenSource)
        {
            this.ts = tokenSource;
        }

        public AstValueAccess ParseValueQuery()
        {
            var access = ParseAccess();

            while (IsToken(BQueryTokenKind.Dot))
            {
                AdvanceToken(BQueryTokenKind.Dot);

                access.SubAccess = ParseValueQuery();
            }

            return access;

        }

        private AstValueAccess ParseAccess()
        {
            if (IsToken(BQueryTokenKind.Id))
            {
                var elementName = this.ts.Current.Value;
                AdvanceToken(BQueryTokenKind.Id);

                if (IsToken(BQueryTokenKind.LBracket))
                {
                    AdvanceToken(BQueryTokenKind.LBracket);

                    if (IsToken(BQueryTokenKind.Number))
                    {
                        var value = int.Parse(this.ts.Current.Value);

                        AdvanceToken(BQueryTokenKind.Number);
                        AdvanceToken(BQueryTokenKind.RBracket);

                        return new AstArrayAccess(elementName, value);
                    }
                    else
                    {
                        throw new InvalidOperationException("Expected number in array access");
                    }
                }

                return new AstValueAccess(elementName);
            }

            throw new InvalidOperationException("Could not parse access");
        }
    }
}
