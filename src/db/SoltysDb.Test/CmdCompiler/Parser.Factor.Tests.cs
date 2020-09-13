using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public partial class ParserTests
    {
        [Fact]
        public void ParseFactor_ParsingFunctionWithoutArgumentsCall()
        {
            var expectedAst = new AstFunctionCallExpression("GetKeys");
            AstAssert.Factor(expectedAst, "GetKeys()");
        }

        [Fact]
        public void ParseFactor_ParsingFunctionWithArgumentsCall()
        {
            var expectedArguments = new AstExpression[]
            {
                new AstNumberExpression {
                    Value = "1"
                },
                new AstBinaryExpression {
                    Operator = TokenKind.Plus,
                    Lhs = new AstNumberExpression {Value = "2"},
                    Rhs = new AstNumberExpression {Value = "3"},
                }
            };

            var expectedAst = new AstFunctionCallExpression("ExampleCall", expectedArguments);
            AstAssert.Factor(expectedAst, "ExampleCall(1, 2+3)");
        }

    }
}
