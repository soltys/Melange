using System.Collections.Generic;

namespace SoltysDb.Core
{
    internal interface ILexer
    {
        IEnumerable<Token> GetTokens();
    }
}