namespace Soltys.Library.TextAnalysis
{
    public interface ITokenSource<out TToken, TTokenKind> where TToken: IToken<TTokenKind>
    {
        TToken Current
        {
            get;
        }
        void NextToken();
    }
}
