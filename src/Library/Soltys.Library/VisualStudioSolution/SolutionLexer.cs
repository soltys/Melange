using System;
using System.Collections.Generic;
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

                this.textSource.AdvanceChar();
            }

            yield break;
        }
        private SolutionToken MakeToken(SolutionTokenKind kind, string value) =>
            new SolutionToken(kind, value, this.textSource.GetPosition());

        public SolutionToken Empty => SolutionToken.Empty;

    }
    public class SolutionParser : ParserBase<SolutionToken, SolutionTokenKind>
    {
        public SolutionParser(ITokenSource<SolutionToken, SolutionTokenKind> ts) : base(ts)
        {
        }
    }
    public class SolutionToken : IToken<SolutionTokenKind>
    {
        public SolutionToken(SolutionTokenKind lispTokenKind, string value)
        {
            TokenKind = lispTokenKind;
            Value = value;
            Position = Position.Empty;
        }

        public SolutionToken(SolutionTokenKind tokenKind, string value, Position position)
        {
            TokenKind = tokenKind;
            Value = value;
            Position = position;
        }

        public SolutionTokenKind TokenKind
        {
            get;
        }

        public string Value
        {
            get;
        }

        public Position Position
        {
            get;
        }

        public static SolutionToken Empty
        {
            get;
        } = new SolutionToken(SolutionTokenKind.Undefined, string.Empty, Position.Empty);


        public override string ToString() => $"<{TokenKind}, {Value}>";

        public static bool operator ==(SolutionToken lhs, SolutionToken rhs) => lhs.Equals(rhs);
        public static bool operator !=(SolutionToken lhs, SolutionToken rhs) => !(lhs == rhs);

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(SolutionToken other) => TokenKind == other.TokenKind && Value == other.Value;
        public override int GetHashCode() => HashCode.Combine((int)TokenKind, Value);
    }

    public enum SolutionTokenKind
    {
        Undefined,
        /// <summary>
        /// Example: #
        /// </summary>
        Hash,
        /// <summary>
        /// Example: Project, Global, EndProject
        /// </summary>
        Id,
        /// <summary>
        /// Example: {C67CD1BA-F675-4559-B1FD-A886315A2D1B} 
        /// </summary>
        Guid,
        /// <summary>
        /// Example: "app"
        /// </summary>
        String,
        /// <summary>
        /// Example: (
        /// </summary>
        LParen,
        /// <summary>
        /// Example: )
        /// </summary>
        RParen,
        /// <summary>
        /// Example: =
        /// </summary>
        Equal,
        /// <summary>
        /// Example: ,
        /// </summary>
        Comma,
        /// <summary>
        /// Example: |
        /// </summary>
        Pipe,

        Number,   // Maybe there is no numbers in sln... only version
        /// <summary>
        /// Example:10.0.40219.1
        /// </summary>
        Version,
    }
}
