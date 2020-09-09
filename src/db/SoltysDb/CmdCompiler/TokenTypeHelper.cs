using SoltysLib;

namespace SoltysDb
{
    public static class TokenTypeHelper
    {
        public static int GetPrecedence(this TokenType tokenType)
        {
            var operatorAttribute = EnumHelper.GetEnumFieldAttribute<OperatorAttribute>(tokenType);
            return operatorAttribute?.Precedence ?? 0;
        }

        public static Associativity GetAssociativity(this TokenType tokenType)
        {
            var operatorAttribute = EnumHelper.GetEnumFieldAttribute<OperatorAttribute>(tokenType);
            return operatorAttribute?.Associativity ?? Associativity.Left;
        }
    }
}
