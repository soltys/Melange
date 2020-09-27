using Soltys.Library;
using Soltys.Library.TextAnalysis;

namespace Soltys.Database
{
    public static class TokenKindHelper
    {
        public static int GetPrecedence(this CmdTokenKind cmdTokenKind)
        {
            var operatorAttribute = EnumHelper.GetEnumFieldAttribute<OperatorAttribute>(cmdTokenKind);
            return operatorAttribute?.Precedence ?? 0;
        }

        public static Associativity GetAssociativity(this CmdTokenKind cmdTokenKind)
        {
            var operatorAttribute = EnumHelper.GetEnumFieldAttribute<OperatorAttribute>(cmdTokenKind);
            return operatorAttribute?.Associativity ?? Associativity.Left;
        }
    }
}
