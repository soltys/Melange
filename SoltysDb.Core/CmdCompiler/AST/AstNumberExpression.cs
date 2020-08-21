namespace SoltysDb.Core
{
    internal class AstNumberExpression : AstExpression, IAstNode
    {
        public void Accept(IAstVisitor visitor) => visitor.VisitNumberExpression(this);
    }
}
