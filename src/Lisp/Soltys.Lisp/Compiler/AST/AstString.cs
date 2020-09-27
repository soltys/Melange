namespace Soltys.Lisp.Compiler
{
    internal class AstString: IAstNode
    {
        public string Value
        {
            get;
        }

        public AstString(string value)
        {
            Value = value;
        }

        public void Accept(IAstVisitor visitor) => visitor.VisitString(this);
        public override string ToString() => $"\"{Value}\"";
    }
}
