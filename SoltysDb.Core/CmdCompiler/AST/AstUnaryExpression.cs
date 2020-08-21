namespace SoltysDb.Core
{
    internal class AstUnaryExpression : AstExpression
    {
        public TokenType Operator { get; set; }
        public AstExpression Expression { get; set; }
    }
}