using System;
using SoltysLib.TextAnalysis;

namespace SoltysDb
{
    internal readonly struct Token : IToken<TokenKind>
    {
        public static Token Empty = new Token(TokenKind.Undefined, "");
        public Token(TokenKind tokenKind, string value)
        {
            TokenKind = tokenKind;
            Value = value;
        }

        public TokenKind TokenKind
        {
            get;
        }
        public string Value
        {
            get;
        }

        public override string ToString() => $"<{TokenKind},{Value}>";

        public static bool operator ==(Token lhs, Token rhs) => lhs.Equals(rhs);
        public static bool operator !=(Token lhs, Token rhs) => !(lhs == rhs);

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Token other) => TokenKind == other.TokenKind && Value == other.Value;
        public override int GetHashCode() => HashCode.Combine((int)TokenKind, Value);
    }
}
