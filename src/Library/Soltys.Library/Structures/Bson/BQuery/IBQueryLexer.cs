using System.Collections.Generic;

namespace Soltys.Library.Bson.BQuery
{
    internal interface IBQueryLexer
    {
        IEnumerable<BQueryToken> GetTokens();
    }
}
