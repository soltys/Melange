namespace SoltysDb
{
    internal interface IAstVisitor
    {
        void VisitExpression(AstExpression expression);
        void VisitNumberExpression(AstNumberExpression number);
        void VisitBinaryExpression(AstBinaryExpression binaryExpression);
        void VisitUnaryExpression(AstUnaryExpression unaryExpression);
    } 
}
