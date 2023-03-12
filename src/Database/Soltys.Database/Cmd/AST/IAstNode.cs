namespace Soltys.Database;

internal interface IAstNode
{
    void Accept(IAstVisitor visitor);
}