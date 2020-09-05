namespace SoltysDb
{
    internal readonly struct Token
    {
        public static Token Empty = new Token(TokenType.Undefined, "");
        public Token(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

        public TokenType TokenType { get; }
        public string Value { get; }

        public override string ToString() => $"<{TokenType},{Value}>";
    }
}
