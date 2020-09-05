namespace SoltysDb
{
    internal interface ITokenSource
    {
        Token Current { get; }
        Token PeekNextToken { get; }
        void NextToken();
    }
}
