namespace SoltysDb
{
    internal readonly struct Token
    {
        public static Token Empty = new Token(TokenKind.Undefined, "");
        public Token(TokenKind tokenKind, string value)
        {
            TokenKind = tokenKind;
            Value = value;
        }

        public TokenKind TokenKind { get; }
        public string Value { get; }

        public override string ToString() => $"<{TokenKind},{Value}>";
    }
}
