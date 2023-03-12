namespace Soltys.Library.BQuery;

internal interface IBQueryLexer
{
    IEnumerable<BQueryToken> GetTokens();
}
