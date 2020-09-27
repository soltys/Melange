namespace Soltys.Lisp.Compiler
{
    internal class AstNumber : IAstNode
    {
        public AstNumber()
        {
            
        }

        public void Accept(IAstVisitor visitor) => visitor.VisitNumber(this);
        
    }
}
