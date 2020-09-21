namespace Soltys.Database
{
    internal class AstUnaryExpression : AstExpression, IAstNode
    {
        public TokenKind Operator { get; set; }
        public AstExpression Expression { get; set; }

        void IAstNode.Accept(IAstVisitor visitor) => visitor.VisitUnaryExpression(this);
    }
}
