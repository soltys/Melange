namespace SoltysDb
{
    internal interface IAstNode
    {
        void Accept(IAstVisitor visitor);
    }
}
