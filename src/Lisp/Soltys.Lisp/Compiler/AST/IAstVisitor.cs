namespace Soltys.Lisp.Compiler
{
    internal interface IAstVisitor
    {
        void VisitList(AstList ast);
        void VisitNumber(AstNumber ast);
        void VisitSymbol(AstSymbol ast);
        void VisitString(AstString ast);
    }
}