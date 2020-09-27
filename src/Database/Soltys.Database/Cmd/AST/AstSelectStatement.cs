namespace Soltys.Database
{
    internal class AstSelectStatement : IAstNode
    {
        public AstExpression SelectWhat
        {
            get;
            set;
        }
        public AstExpression From
        {
            get;
            set;
        }

        public AstExpression Where
        {
            get;
            set;
        }

        public void Accept(IAstVisitor visitor) => visitor.VisitSelectStatement(this);
    }
}
