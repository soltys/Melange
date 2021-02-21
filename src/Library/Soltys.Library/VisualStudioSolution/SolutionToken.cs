using System;
using Soltys.Library.TextAnalysis;

namespace Soltys.Library.VisualStudioSolution
{
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
}
