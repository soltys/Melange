using System;
using System.Collections.Generic;
using System.Text;
using SoltysLib.TextAnalysis;

namespace SoltysLib.Bson.BQuery
{
    internal class BQueryLexer : ILexer<BQueryToken>
    {
        private readonly string query;

        public BQueryLexer(string query)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public IEnumerable<BQueryToken> GetTokens()
        {
            for (int i = 0; i < this.query.Length; i++)
            {
                char c = this.query[i];

                if (c == '.')
                {
                    yield return new BQueryToken(BQueryTokenKind.Dot, ".");
                }
                else if (c == '[')
                {
                    yield return new BQueryToken(BQueryTokenKind.LBracket, "[");
                }
                else if (c == ']')
                {
                    yield return new BQueryToken(BQueryTokenKind.RBracket, "]");
                }
                else
                {
                    if (char.IsLetter(c))
                    {
                        var (ident, offset) = GetAnyName(this.query.AsSpan().Slice(i));
                        yield return new BQueryToken(BQueryTokenKind.Id, ident);
                        i += offset;
                    }
                    else if (char.IsDigit(c))
                    {
                        var (token, offset) = GetNumber(this.query.AsSpan().Slice(i));
                        yield return token;
                        i += offset;
                    }
                }
            }
        }

        public BQueryToken GetEmpty() => BQueryToken.Empty;

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

            return (new BQueryToken(BQueryTokenKind.Number, builder.ToString()), i - 1);
        }
    }
}
