using System.Text.RegularExpressions;
using Soltys.Library.TextAnalysis;

namespace Soltys.Lisp.Compiler;

internal class LispLexer : ILexer<LispToken>
{
    private readonly ITextSource textSource;

    public LispLexer(ITextSource textSource)
    {
        this.textSource = textSource ?? throw new ArgumentNullException(nameof(textSource));
    }

    public IEnumerable<LispToken> GetTokens()
    {
        HashSet<char> charsSymbols = new HashSet<char>() {
            '+','*','/', //math
            '=','<','>' //boolean

        };
        while (!this.textSource.IsEnded)
        {
            var c = this.textSource.Current;
            if (c == ';')
            {
                //comment ignore until new line
                do
                {
                    this.textSource.AdvanceChar();
                } while (!this.textSource.IsEnded && this.textSource.Current != '\n');
            }
            else if (c == '(')
            {
                yield return new LispToken(LispTokenKind.LParen, "(", this.textSource.GetPosition());
            }
            else if (c == ')')
            {
                yield return new LispToken(LispTokenKind.RParen, ")", this.textSource.GetPosition());
            }
            else if (c == '\'')
            {
                yield return new LispToken(LispTokenKind.Quote, "'", this.textSource.GetPosition());
            }
            else if (charsSymbols.Contains(c))
            {
                yield return new LispToken(LispTokenKind.Symbol, c.ToString(), this.textSource.GetPosition());
            }
            else if (c == '-')
            {
                if (char.IsDigit(this.textSource.Next))
                {
                    this.textSource.AdvanceChar(); // for the '-' symbol
                    var (numberString, offset) = LexerHelper.GetNumber(this.textSource.Slice());
                    yield return new LispToken(LispTokenKind.Number, '-' + numberString, this.textSource.GetPosition());
                    this.textSource.AdvanceChar(offset);
                }
                else
                {
                    yield return new LispToken(LispTokenKind.Symbol, "-", this.textSource.GetPosition());

                }
            }
            else
            {
                if (char.IsLetter(c))
                {
                    var match = Regex.Match(this.textSource.Slice().ToString(), @"[\w!_]+", RegexOptions.Compiled);
                    yield return new LispToken(LispTokenKind.Symbol, match.Value, this.textSource.GetPosition());
                    this.textSource.AdvanceChar(match.Length - 1);
                }
                else if (char.IsDigit(c))
                {
                    var (numberString, offset) = LexerHelper.GetNumber(this.textSource.Slice());
                    yield return new LispToken(LispTokenKind.Number, numberString, this.textSource.GetPosition());
                    this.textSource.AdvanceChar(offset);
                }
                else if (c == '\"')
                {
                    var (str, offset) = LexerHelper.GetDoubleQuoteString(this.textSource.Slice());
                    yield return new LispToken(LispTokenKind.String, str, this.textSource.GetPosition());
                    this.textSource.AdvanceChar(offset);
                }
            }

            this.textSource.AdvanceChar();
        }
    }

    public LispToken Empty => LispToken.Empty;
}
