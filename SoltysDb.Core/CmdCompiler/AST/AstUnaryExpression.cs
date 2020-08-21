namespace SoltysDb.Core
{
    internal class AstUnaryExpression : AstExpression, IAstNode
    {
        public TokenType Operator { get; set; }
        public AstExpression Expression { get; set; }

        void IAstNode.Accept(IAstVisitor visitor) => visitor.VisitUnaryExpression(this);
    }
}