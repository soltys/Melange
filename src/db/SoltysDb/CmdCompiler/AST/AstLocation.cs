namespace SoltysDb
{
    internal class AstLocation : AstNode,IAstNode
    {
        public void Accept(IAstVisitor visitor) => visitor.VisitLocation(this);

    }
}
