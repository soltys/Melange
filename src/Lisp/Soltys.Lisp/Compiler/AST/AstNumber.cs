namespace Soltys.Lisp.Compiler;

internal abstract class AstNumber : IAstNode
{
    protected AstNumber()
    {
            
    }

    public void Accept(IAstVisitor visitor) => visitor.VisitNumber(this);
    public abstract IAstNode Clone();
}