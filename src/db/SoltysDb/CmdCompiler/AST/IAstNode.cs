namespace SoltysDb
{
    interface IAstNode
    {
        void Accept(IAstVisitor visitor);
    }
}
