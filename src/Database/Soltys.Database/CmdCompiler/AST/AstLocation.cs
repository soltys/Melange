namespace Soltys.Database
{
    internal class AstLocation : AstNode,IAstNode
    {
        public void Accept(IAstVisitor visitor) => visitor.VisitLocation(this);

    }
}
