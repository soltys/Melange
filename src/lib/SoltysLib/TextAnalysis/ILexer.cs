using System.Collections.Generic;

namespace SoltysLib.TextAnalysis
{
    public interface ILexer<out TToken>
    {
        IEnumerable<TToken> GetTokens();
        TToken Empty
        {
            get;
        }
    }
}
