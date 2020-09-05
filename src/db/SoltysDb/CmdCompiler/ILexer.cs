using System.Collections.Generic;

namespace SoltysDb
{
    internal interface ILexer
    {
        IEnumerable<Token> GetTokens();
    }
}
