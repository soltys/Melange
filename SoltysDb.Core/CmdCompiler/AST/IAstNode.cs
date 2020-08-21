namespace SoltysDb.Core
{
    interface IAstNode
    {
        void Accept(IAstVisitor visitor);
    }
}
