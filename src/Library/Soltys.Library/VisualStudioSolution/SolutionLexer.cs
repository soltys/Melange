using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Soltys.Library.TextAnalysis;

namespace Soltys.Library.VisualStudioSolution
{
    public class SolutionLexer : ILexer<SolutionToken>
    {
        private readonly ITextSource textSource;

        public SolutionLexer(ITextSource textSource)
        {
            this.textSource = textSource ?? throw new ArgumentNullException(nameof(textSource));
        }

        public IEnumerable<SolutionToken> GetTokens()
        {
            while (!this.textSource.IsEnded)
            {
                var c = this.textSource.Current;

                if (c == '(')
                {
                    yield return MakeToken(SolutionTokenKind.LParen, "(");
                }
                else if (c == ')')
                {
                    yield return MakeToken(SolutionTokenKind.RParen, ")");
                }
                else if (c == '|')
                {
                    yield return MakeToken(SolutionTokenKind.Pipe, "|");
                }
                else if (c == '\n')
                {
                    yield return MakeToken(SolutionTokenKind.NewLine, "\n");
                }
                else if (c == ',')
                {
                    yield return MakeToken(SolutionTokenKind.Comma, ",");
                }
                else if (c == '=')
                {
                    yield return MakeToken(SolutionTokenKind.Equal, "=");
                }
                else if (c == '#')
                {
                    yield return MakeToken(SolutionTokenKind.Hash, "#");
                }
                else if (c == '\"')
                {
                    var (str, offset) = LexerHelper.GetDoubleQuoteString(this.textSource.Slice());
                    yield return MakeToken(SolutionTokenKind.String, str);
                    this.textSource.AdvanceChar(offset);
                }
                else if (char.IsLetter(c))
                {
                    var match = Regex.Match(this.textSource.Slice().ToString(), @"[\w!_]+", RegexOptions.Compiled);
                    yield return MakeToken(SolutionTokenKind.Id, match.Value);
                    this.textSource.AdvanceChar(match.Length - 1);
                }
                else if (char.IsDigit(c))
                {
                    var match = Regex.Match(this.textSource.Slice().ToString(), @"[1-9][0-9\.]*",
                        RegexOptions.Compiled);
                    yield return MakeToken(SolutionTokenKind.Version, match.Value);
                    this.textSource.AdvanceChar(match.Length - 1);
                }
                else if (c == '{')
                {
                    var match = Regex.Match(this.textSource.Slice().ToString(), @"{[0-9A-Za-z\-]+}",
                        RegexOptions.Compiled);
                    yield return MakeToken(SolutionTokenKind.Guid, match.Value);
                    this.textSource.AdvanceChar(match.Length - 1);
                }

                this.textSource.AdvanceChar();
            }
        }
        private SolutionToken MakeToken(SolutionTokenKind kind, string value) =>
            new SolutionToken(kind, value, this.textSource.GetPosition());

        public SolutionToken Empty => SolutionToken.Empty;

    }
}
