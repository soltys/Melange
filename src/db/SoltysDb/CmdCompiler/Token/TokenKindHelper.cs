using SoltysLib;

namespace SoltysDb
{
    public static class TokenKindHelper
    {
        public static int GetPrecedence(this TokenKind tokenKind)
        {
            var operatorAttribute = EnumHelper.GetEnumFieldAttribute<OperatorAttribute>(tokenKind);
            return operatorAttribute?.Precedence ?? 0;
        }

        public static Associativity GetAssociativity(this TokenKind tokenKind)
        {
            var operatorAttribute = EnumHelper.GetEnumFieldAttribute<OperatorAttribute>(tokenKind);
            return operatorAttribute?.Associativity ?? Associativity.Left;
        }
    }
}
