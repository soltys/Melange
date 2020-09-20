namespace SoltysLib.TextAnalysis
{
    public interface ITokenSource<out TToken, TTokenKind> where TToken: IToken<TTokenKind>
    {
        TToken Current
        {
            get;
        }
        TToken PeekNextToken
        {
            get;
        }
        void NextToken();
    }
}
