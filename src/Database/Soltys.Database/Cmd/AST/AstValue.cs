namespace Soltys.Database;

internal class AstValue:IAstNode
{
    public ReadOnlySpan<AstExpression> Expressions => this.expressions;
    private readonly AstExpression[] expressions;

    public AstValue(AstExpression[] expressions)
    {
        this.expressions = expressions;
    }

    public void Accept(IAstVisitor visitor) => visitor.VisitValue(this);

}
