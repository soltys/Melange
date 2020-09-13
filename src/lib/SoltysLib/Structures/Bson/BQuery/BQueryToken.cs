namespace SoltysLib.Bson.BQuery
{
    internal readonly struct BQueryToken
    {
        public BQueryToken(BQueryTokenKind kind, string value)
        {
            TokenType = kind;
            Value = value;
        }

        public string Value
        {
            get;
        }

        public BQueryTokenKind TokenType
        {
            get;
        }

        public static BQueryToken Empty
        {
            get;

        } = new BQueryToken(BQueryTokenKind.Undefined, "");
    }
}
