namespace SoltysDb.Core
{
    interface IAstVisitor
    {
        void Visit(IAstNode node);
        void VisitExpression(AstExpression expression);
        void VisitNumberExpression(AstNumberExpression number);
        void VisitBinaryExpression(AstBinaryExpression binaryExpression);
        void VisitUnaryExpression(AstUnaryExpression unaryExpression);
    } 
}
