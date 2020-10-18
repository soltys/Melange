using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks.Sources;
using Soltys.Lisp.Compiler;
using Xunit;

namespace Soltys.Lisp.Test.Compiler
{
    public class AstTests
    {
        public static IEnumerable<object[]> AstObjectsSource =>
            new List<object[]>
            {
                new object[] { new AstList(), nameof(IAstVisitor.VisitList) },
                new object[] { new AstIntNumber(42), nameof(IAstVisitor.VisitNumber) },
                new object[] { new AstDoubleNumber(0.69), nameof(IAstVisitor.VisitNumber) },
                new object[] { new AstSymbol("foo"), nameof(IAstVisitor.VisitSymbol) },
                new object[] { new AstString("foo"), nameof(IAstVisitor.VisitString) },
            };

        [Theory]
        [MemberData(nameof(AstObjectsSource))]
        internal void Ast_VisitsCorrectMethod(IAstNode node, string expectedCalledMethod)
        {
            var visitorCallCounting = new AstVisitorCallCounting();
            node.Accept(visitorCallCounting);
            Assert.Single(visitorCallCounting.CallCount);
            Assert.Equal(1, visitorCallCounting.CallCount[expectedCalledMethod]);
        }

        [Fact]
        internal void AstSymbol_Clone_ClonesTheObject()
        {
            var ast = new AstSymbol("foobar");
            var cloned = ast.Clone();

            Assert.NotSame(ast, cloned);
            Assert.Equal(ast, cloned);
            Assert.Equal(ast.Name, ((AstSymbol)cloned).Name);
        }

        [Fact]
        internal void ToString_AstString_EqualToExpected() =>
            Assert.Equal("\"foobar\"", new AstString("foobar").ToString());

        [Fact]
        internal void ToString_AstIntNumber_EqualToExpected() =>
            Assert.Equal("42", new AstIntNumber(42).ToString());

        [Fact]
        internal void ToString_AstDoubleNumber_EqualToExpected() =>
            Assert.Equal("0.69", new AstDoubleNumber(0.69).ToString());

        [Fact]
        internal void ToString_AstSymbol_EqualToExpected() =>
            Assert.Equal("make-sth", new AstSymbol("make-sth").ToString());

        [Fact]
        internal void ToString_AstList_EqualToExpected() =>
            Assert.Equal("(add 1 \"foobar\")", new AstList(new AstSymbol("add"), new AstIntNumber(1), new AstString("foobar")).ToString());

        [Fact]
        internal void ToString_EmptyAstList_EqualToExpected() =>
            Assert.Equal("()", new AstList().ToString());
    }
}
