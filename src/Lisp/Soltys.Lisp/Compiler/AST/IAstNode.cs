namespace Soltys.Lisp.Compiler
{
    internal interface IAstNode
    {
        void Accept(IAstVisitor visitor);
        IAstNode Clone();
    }
}
