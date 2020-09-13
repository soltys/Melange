using System.Collections.Generic;

namespace SoltysLib.Bson.BQuery
{
    internal interface IBQueryLexer
    {
        IEnumerable<BQueryToken> GetTokens();
    }
}
