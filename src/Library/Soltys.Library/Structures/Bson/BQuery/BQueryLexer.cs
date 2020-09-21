using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Soltys.Library.TextAnalysis;

namespace Soltys.Library.Bson.BQuery
{
    internal class BQueryLexer : ILexer<BQueryToken>
    {
        private readonly ITextSource textSource;

        public BQueryLexer(ITextSource textSource)
        {
            this.textSource = textSource ?? throw new ArgumentNullException(nameof(textSource));
        }

        public IEnumerable<BQueryToken> GetTokens()
        {
            while (!this.textSource.IsEnded)
            {
                char c = this.textSource.Current;

                if (c == '.')
                {
                    yield return new BQueryToken(BQueryTokenKind.Dot, ".", this.textSource.GetPosition());
                }
                else if (c == '[')
                {
                    yield return new BQueryToken(BQueryTokenKind.LBracket, "[", this.textSource.GetPosition());
                }
                else if (c == ']')
                {
                    yield return new BQueryToken(BQueryTokenKind.RBracket, "]", this.textSource.GetPosition());
                }
                else
                {
                    if (char.IsLetter(c))
                    {
                        var (ident, offset) = GetAnyName(this.textSource.Slice());
                        yield return new BQueryToken(BQueryTokenKind.Id, ident, this.textSource.GetPosition());
                        this.textSource.AdvanceChar(offset);
                    }
                    else if (char.IsDigit(c))
                    {
                        var (token, offset) = GetNumber(this.textSource.Slice());
                        yield return token;
                        this.textSource.AdvanceChar(offset);
                    }
                    else if (c == '\'')
                    {
                        var match = Regex.Match(this.textSource.Slice().ToString(), "\'([^\']*)\'", RegexOptions.Compiled);
                        yield return new BQueryToken(BQueryTokenKind.String,
                            match.Groups[1].Value,
                            this.textSource.GetPosition());
                        this.textSource.AdvanceChar(match.Length - 1);
                    }
                    else if (c == '\"')
                    {
                        var match = Regex.Match(this.textSource.Slice().ToString(), "\"([^\"]*)\"", RegexOptions.Compiled);
                        yield return new BQueryToken(BQueryTokenKind.String,
                            match.Groups[1].Value,
                            this.textSource.GetPosition());
                        this.textSource.AdvanceChar(match.Length - 1);
                    }
                }

                this.textSource.AdvanceChar();
            }
        }

        public BQueryToken Empty => BQueryToken.Empty;

        private (string, int) GetAnyName(ReadOnlySpan<char> slice)
        {
            var builder = new StringBuilder();
            int i = 0;

            while (i < slice.Length && IsNameCharacter(slice[i]))
            {
                builder.Append(slice[i]);
                i++;
            }

            int offset = i - 1;

            if (offset < 0)
            {
                throw new InvalidOperationException("Something went wrong on here!");
            }

            return (builder.ToString(), i - 1);

            bool IsNameCharacter(char letter)
            {
                return char.IsLetterOrDigit(letter) ||
                       letter == '_';
            }
        }

        private (BQueryToken, int) GetNumber(ReadOnlySpan<char> slice)
        {
            var builder = new StringBuilder();
            int i = 0;

            while (i < slice.Length && char.IsDigit(slice[i]))
            {
                builder.Append(slice[i]);
                i++;
            }

            return (new BQueryToken(BQueryTokenKind.Number, builder.ToString(), this.textSource.GetPosition()), i - 1);
        }
    }
}
