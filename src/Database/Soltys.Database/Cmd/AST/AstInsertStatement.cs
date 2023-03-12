namespace Soltys.Database;

internal class AstInsertStatement : IAstNode
{
    public AstLocation Location
    {
        get;
        set;
    }

    public AstValue Values
    {
        get;
        set;
    }

    public void Accept(IAstVisitor visitor) => visitor.VisitInsertStatement(this);
}