using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    internal class TestParserVisitor : IAstVisitor
    {
        private IAstNode expected;


        public void AssertVisit(IAstNode expectedVisit, IAstNode actualVisit)
        {
            this.expected = expectedVisit;
            Visit(actualVisit);
        }

        public void Visit(IAstNode node) => node.Accept(this);


        public void VisitExpression(AstExpression expression)
        {
            Assert.IsType<AstExpression>(this.expected);
            var expectedAst = (AstExpression) this.expected;

            Assert.Equal(expectedAst.Value, expression.Value);
        }

        public void VisitNumberExpression(AstNumberExpression number)
        {
            Assert.IsType<AstNumberExpression>(this.expected);
            var expectedAst = (AstNumberExpression)this.expected;
            Assert.Equal(expectedAst.Value, number.Value);
        }

        public void VisitBinaryExpression(AstBinaryExpression binaryExpression)
        {
            Assert.IsType<AstBinaryExpression>(this.expected);
            var expectedAst = (AstBinaryExpression)this.expected;
            Assert.Equal(expectedAst.Operator, binaryExpression.Operator);

            AssertVisit(expectedAst.RightExpression, binaryExpression.RightExpression);
            AssertVisit(expectedAst.LeftExpression, binaryExpression.LeftExpression);

        }

        public void VisitUnaryExpression(AstUnaryExpression unaryExpression)
        {
            Assert.IsType<AstUnaryExpression>(this.expected);
            var expectedAst = (AstUnaryExpression) this.expected;
            Assert.Equal(expectedAst.Operator, unaryExpression.Operator);

            AssertVisit(expectedAst.Expression, unaryExpression.Expression);
        }
    }
}