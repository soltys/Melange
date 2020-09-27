namespace Soltys.Lisp.Compiler
{
    internal class AstSymbol: IAstNode
    {
        public string Name
        {
            get;
        }
        public AstSymbol(string name)
        {
            Name = name;
        }

        public void Accept(IAstVisitor visitor) => visitor.VisitSymbol(this);
        public override string ToString() => Name;
    }
}
