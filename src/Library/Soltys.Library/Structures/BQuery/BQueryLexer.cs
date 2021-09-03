using System;
using System.Collections.Generic;
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
                        var (ident, offset) = LexerHelper.GetAnyName(this.textSource.Slice());
                        yield return new BQueryToken(BQueryTokenKind.Id, ident, this.textSource.GetPosition());
                        this.textSource.AdvanceChar(offset);
                    }
                    else if (char.IsDigit(c))
                    {
                        var (numberString, offset) = LexerHelper.GetNumber(this.textSource.Slice());

                        yield return (new BQueryToken(BQueryTokenKind.Number, numberString, this.textSource.GetPosition()));
                        this.textSource.AdvanceChar(offset);
                    }
                    else if (c == '\'')
                    {
                        var (str, offset) = LexerHelper.GetSingleQuoteString(this.textSource.Slice());
                        yield return new BQueryToken(BQueryTokenKind.String, str, this.textSource.GetPosition());
                        this.textSource.AdvanceChar(offset);
                        
                    }
                    else if (c == '\"')
                    {
                        var (str, offset) = LexerHelper.GetDoubleQuoteString(this.textSource.Slice());
                        yield return new BQueryToken(BQueryTokenKind.String, str, this.textSource.GetPosition());
                        this.textSource.AdvanceChar(offset);
                    }
                }

                this.textSource.AdvanceChar();
            }
        }

        public BQueryToken Empty => BQueryToken.Empty;

        
    }
}
