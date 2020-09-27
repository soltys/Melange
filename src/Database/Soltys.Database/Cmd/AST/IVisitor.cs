namespace Soltys.Database
{
    internal interface IAstVisitor
    {
        void VisitExpression(AstExpression expression);
        void VisitNumberExpression(AstNumberExpression number);
        void VisitBinaryExpression(AstBinaryExpression binaryExpression);
        void VisitUnaryExpression(AstUnaryExpression unaryExpression);
        void VisitFunctionCallExpression(AstFunctionCallExpression astFunctionCallExpression);
        void VisitSelectStatement(AstSelectStatement selectStatement);
        void VisitInsertStatement(AstInsertStatement insertStatement);
        void VisitLocation(AstLocation location);
        void VisitValue(AstValue value);
    } 
}
