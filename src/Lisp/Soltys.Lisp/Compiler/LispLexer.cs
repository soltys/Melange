using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Soltys.Library.TextAnalysis;

namespace Soltys.Lisp.Compiler
{
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
                    yield return MakeToken(LispTokenKind.LParen, "(");
                }
                else if (c == ')')
                {
                    yield return MakeToken(LispTokenKind.RParen, ")");
                }
                else if (c == '\'')
                {
                    yield return MakeToken(LispTokenKind.Quote, "'");
                }
                else if (charsSymbols.Contains(c))
                {
                    yield return MakeToken(LispTokenKind.Symbol, c.ToString());
                }
                else if (c == '-')
                {
                    if (char.IsDigit(this.textSource.Next))
                    {
                        this.textSource.AdvanceChar(); // for the '-' symbol
                        var (numberString, offset) = LexerHelper.GetNumber(this.textSource.Slice());
                        yield return MakeToken(LispTokenKind.Number, '-' + numberString);
                        this.textSource.AdvanceChar(offset);
                    }
                    else
                    {
                        yield return new LispToken(LispTokenKind.Symbol, "-");

                    }
                }
                else
                {
                    if (char.IsLetter(c))
                    {
                        var match = Regex.Match(this.textSource.Slice().ToString(), @"[\w!_]+", RegexOptions.Compiled);
                        yield return MakeToken(LispTokenKind.Symbol, match.Value);
                        this.textSource.AdvanceChar(match.Length - 1);
                    }
                    else if (char.IsDigit(c))
                    {
                        var (numberString, offset) = LexerHelper.GetNumber(this.textSource.Slice());
                        yield return MakeToken(LispTokenKind.Number, numberString);
                        this.textSource.AdvanceChar(offset);
                    }
                    else if (c == '\"')
                    {
                        var (str, offset) = LexerHelper.GetDoubleQuoteString(this.textSource.Slice());
                        yield return MakeToken(LispTokenKind.String, str);
                        this.textSource.AdvanceChar(offset);
                    }
                }

                this.textSource.AdvanceChar();
            }
        }

        private LispToken MakeToken(LispTokenKind kind, string value) =>
            new LispToken(kind, value, this.textSource.GetPosition());

        public LispToken Empty => LispToken.Empty;
    }
}
