using Soltys.Library.TextAnalysis;

namespace Soltys.Library.BQuery;

internal class BQueryParser : ParserBase<BQueryToken,BQueryTokenKind>, IBQueryParser
{
       
    public BQueryParser(ITokenSource<BQueryToken, BQueryTokenKind> ts):base(ts)
    {
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
        //e.g. foo
        if (IsToken(BQueryTokenKind.Id))
        {
            var elementName = this.ts.Current.Value;
            AdvanceToken(BQueryTokenKind.Id);

            //e.g. foo[42]
            if (IsToken(BQueryTokenKind.LBracket))
            {
                return ParseArrayAccess(elementName);
            }

            return new AstValueAccess(elementName);
        }

        //e.g. ['foo'] or ["foo"]
        if (IsToken(BQueryTokenKind.LBracket))
        {
            AdvanceToken(BQueryTokenKind.LBracket);

            var elementName = this.ts.Current.Value;
            AdvanceToken(BQueryTokenKind.String);
            AdvanceToken(BQueryTokenKind.RBracket);

            //e.g. ['foo'][42] or ["foo"][42]
            if (IsToken(BQueryTokenKind.LBracket))
            {
                return ParseArrayAccess(elementName);
            }

            return new AstValueAccess(elementName);
        }

        throw new InvalidOperationException("Could not parse access");


        AstValueAccess ParseArrayAccess(string elementName)
        {
            AdvanceToken(BQueryTokenKind.LBracket);

            var value = int.Parse(this.ts.Current.Value);

            AdvanceToken(BQueryTokenKind.Number);
            AdvanceToken(BQueryTokenKind.RBracket);

            return new AstArrayAccess(elementName, value);
        }
    }
}
