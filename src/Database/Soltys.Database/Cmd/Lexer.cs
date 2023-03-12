using Soltys.Library.TextAnalysis;

namespace Soltys.Database;

internal class Lexer : ILexer<CmdToken>
{
    private readonly ITextSource textSource;

    public Lexer(ITextSource textSource)
    {
        this.textSource = textSource;
    }

    public CmdToken Empty => CmdToken.Empty;

    public IEnumerable<CmdToken> GetTokens()
    {
        while (!this.textSource.IsEnded)
        {

            var currentChar = this.textSource.Current;
            if (currentChar == '=')
            {
                var nextChar = this.textSource.Next;
                if (nextChar == '=')
                {
                    yield return new CmdToken(CmdTokenKind.CompareEqual, "==");
                    this.textSource.AdvanceChar();
                }
                else
                {
                    yield return new CmdToken(CmdTokenKind.EqualSign, "=");
                }
            }

            else if (currentChar == '<')
            {
                var nextChar = this.textSource.Next;
                if (nextChar == '=')
                {
                    yield return new CmdToken(CmdTokenKind.LessThanEqual, "<=");
                    this.textSource.AdvanceChar();
                }
                else
                {
                    yield return new CmdToken(CmdTokenKind.LessThan, "<");
                }
            }
            else if (currentChar == '>')
            {
                var nextChar = this.textSource.Next;
                if (nextChar == '=')
                {
                    yield return new CmdToken(CmdTokenKind.GreaterThanEqual, ">=");
                    this.textSource.AdvanceChar();
                }
                else
                {
                    yield return new CmdToken(CmdTokenKind.GreaterThan, ">");
                }
            }

            else if (currentChar == '+')
            {
                yield return new CmdToken(CmdTokenKind.Plus, "+");
            }
            else if (currentChar == '-')
            {
                yield return new CmdToken(CmdTokenKind.Minus, "-");
            }
            else if (currentChar == '/')
            {
                yield return new CmdToken(CmdTokenKind.Slash, "/");
            }
            else if (currentChar == '*')
            {
                yield return new CmdToken(CmdTokenKind.Star, "*");
            }
            else if (currentChar == '(')
            {
                yield return new CmdToken(CmdTokenKind.LParen, "(");
            }
            else if (currentChar == ')')
            {
                yield return new CmdToken(CmdTokenKind.RParen, ")");
            }
            else if (currentChar == ',')
            {
                yield return new CmdToken(CmdTokenKind.Comma, ",");
            }
            else if (currentChar == '.')
            {
                yield return new CmdToken(CmdTokenKind.Dot, ".");
            }
            else if (currentChar == '!')
            {
                var nextChar = this.textSource.Next;
                if (nextChar == '=')
                {
                    yield return new CmdToken(CmdTokenKind.CompareNotEqual, "!=");
                    this.textSource.AdvanceChar();
                }
            }
            else
            {
                if (char.IsLetter(currentChar))
                {
                    var (ident, offset) = LexerHelper.GetAnyName(this.textSource.Slice());
                    yield return MakeToken(ident);
                    this.textSource.AdvanceChar(offset);
                }
                else if (char.IsDigit(currentChar))
                {
                    var (numberString, offset) = LexerHelper.GetNumber(this.textSource.Slice());
                    yield return new CmdToken(CmdTokenKind.Number, numberString);
                    this.textSource.AdvanceChar(offset);
                }
                else if (currentChar == '\'')
                {

                    var (str, offset) = LexerHelper.GetSingleQuoteString(this.textSource.Slice());
                    yield return new CmdToken(CmdTokenKind.String, str);
                    this.textSource.AdvanceChar(offset);
                }
                else if (currentChar == '\"')
                {
                    var (str,offset) = LexerHelper.GetDoubleQuoteString(this.textSource.Slice());
                    yield return new CmdToken(CmdTokenKind.String, str);
                    this.textSource.AdvanceChar(offset);
                }
            }

            this.textSource.AdvanceChar();
        }
    }


    private CmdToken MakeToken(string input)
    {
        //we are testing lower case to make it case insensitive but we are preserving casing in CmdToken Value
        var testString = input.ToLowerInvariant();
        switch (testString)
        {
            case "insert":
                return new CmdToken(CmdTokenKind.Insert, input);
            case "into":
                return new CmdToken(CmdTokenKind.Into, input);
            case "select":
                return new CmdToken(CmdTokenKind.Select, input);
            case "where":
                return new CmdToken(CmdTokenKind.Where, input);
            case "from":
                return new CmdToken(CmdTokenKind.From, input);
            case "and":
                return new CmdToken(CmdTokenKind.And, input);
            case "or":
                return new CmdToken(CmdTokenKind.Or, input);
            case "values":
                return new CmdToken(CmdTokenKind.Values, input);
            default:
                return new CmdToken(CmdTokenKind.Id, input);
        }
    }
}
