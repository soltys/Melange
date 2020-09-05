namespace SoltysDb
{
    internal class AstExpression : AstNode, IAstNode
    {
        void IAstNode.Accept(IAstVisitor visitor) => visitor.VisitExpression(this);
    }
}
