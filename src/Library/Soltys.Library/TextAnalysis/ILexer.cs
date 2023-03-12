namespace Soltys.Library.TextAnalysis;

public interface ILexer<out TToken>
{
    IEnumerable<TToken> GetTokens();
    TToken Empty
    {
        get;
    }
}
