namespace SoltysLib.TextAnalysis
{
    public interface IToken<out TTokenKind>
    {
        public TTokenKind TokenKind
        {
            get;
        }
        public string Value
        {
            get;
        }
    }
}
