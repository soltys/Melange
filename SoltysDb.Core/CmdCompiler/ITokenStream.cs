namespace SoltysDb.Core
{
    internal interface ITokenStream
    {
        Token Current { get; }
        Token PeekNextToken { get; }
        void NextToken();
    }
}