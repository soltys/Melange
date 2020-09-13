namespace SoltysLib.TextAnalysis
{
    public interface ITokenSource<out TToken>
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
