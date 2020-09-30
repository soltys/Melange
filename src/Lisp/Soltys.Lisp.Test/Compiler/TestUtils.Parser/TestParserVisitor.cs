using Soltys.Lisp.Compiler;
using Xunit;

namespace Soltys.Lisp.Test.Compiler
{
    internal class TestParserVisitor : IAstVisitor
    {
        private IAstNode expected;

        public void AssertVisit(IAstNode expectedVisit, IAstNode actualVisit)
        {
            this.expected = expectedVisit;
            Visit(actualVisit);
        }

        private void Visit(IAstNode node) => node.Accept(this);

        public void VisitList(AstList ast)
        {
            Assert.IsType<AstList>(this.expected);
            var expectedAst = (AstList)this.expected;
            Assert.True(expectedAst.Length == ast.Length, "Expected length of elements in the list is not equal");

            for (int i = 0; i < ast.Length; i++)
            {
                AssertVisit(expectedAst[i], ast[i]);
            }
        }

        public void VisitNumber(AstNumber ast)
        {
            switch (ast)
            {
                case AstIntNumber i:
                    Assert.IsType<AstIntNumber>(this.expected);
                    var expectedIntAst = (AstIntNumber)this.expected;
                    Assert.Equal(expectedIntAst.Value, i.Value);
                    break;
                case AstDoubleNumber d:
                    Assert.IsType<AstDoubleNumber>(this.expected);
                    var expectedDoubleAst = (AstDoubleNumber)this.expected;
                    Assert.Equal(expectedDoubleAst.Value, d.Value);
                    break;
            }
        }

        public void VisitSymbol(AstSymbol ast)
        {
            Assert.IsType<AstSymbol>(this.expected);
            var expectedAst = (AstSymbol)this.expected;
            Assert.Equal(expectedAst.Name, ast.Name);
        }

        public void VisitString(AstString ast)
        {
            Assert.IsType<AstString>(this.expected);
            var expectedAst = (AstString)this.expected;
            Assert.Equal(expectedAst.Value, ast.Value);
        }
    }
}
