namespace SoltysDb
{
    internal class AstBinaryExpression : AstExpression, IAstNode
    {
        public AstExpression Lhs { get; set; }
        public AstExpression Rhs { get; set; }
        public TokenKind Operator { get; set; }

        public override string ToString() => $"[{Lhs}] {Operator} [{Rhs}]";
        public void Accept(IAstVisitor visitor) => visitor.VisitBinaryExpression(this);
    }
}
