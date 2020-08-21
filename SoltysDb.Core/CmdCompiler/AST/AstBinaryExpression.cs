namespace SoltysDb.Core
{
    internal class AstBinaryExpression : AstExpression, IAstNode
    {
        public AstExpression LeftExpression { get; set; }
        public AstExpression RightExpression { get; set; }
        public TokenType Operator { get; set; }

        public override string ToString() => $"[{LeftExpression}] {Operator} [{RightExpression}]";
        public void Accept(IAstVisitor visitor) => visitor.VisitBinaryExpression(this);
    }
}