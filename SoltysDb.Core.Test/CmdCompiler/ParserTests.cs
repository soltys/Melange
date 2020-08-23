using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class ParserTests
    {
        [Fact]
        internal void ParseExpression_OperatorPrecedence_MultiplicationLowerInTreeAdditionHigher()
        {
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Plus,
                Lhs = new AstNumberExpression()
                {
                    Value = "1"
                },
                Rhs = new AstBinaryExpression()
                {
                    Lhs = new AstNumberExpression()
                    {
                        Value = "2"
                    },
                    Rhs = new AstNumberExpression()
                    {
                        Value = "3"
                    },
                    Operator = TokenType.Star
                }
            };

            AssertExpressionAstFromInput(expectedAst, "1+2*3");
        }

        [Fact]
        internal void ParseExpression_ParenthesisPrecedence_AdditionInParenthesisLowerInAstTreeThanMultiplication()
        {
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Star,
                Lhs = new AstBinaryExpression()
                {
                    Lhs = new AstNumberExpression()
                    {
                        Value = "1"
                    },
                    Rhs = new AstNumberExpression()
                    {
                        Value = "2"
                    },
                    Operator = TokenType.Plus
                },
                Rhs = new AstNumberExpression()
                {
                    Value = "3"
                },
            };

            AssertExpressionAstFromInput(expectedAst, "(1+2)*3");
        }

        [Fact]
        internal void ParseExpression_OddNumberOfAddition()
        {
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Plus,
                Lhs = new AstNumberExpression()
                {
                    Value = "1"
                },
                Rhs = new AstBinaryExpression()
                {
                    Lhs = new AstNumberExpression()
                    {
                        Value = "2"
                    },
                    Rhs = new AstNumberExpression()
                    {
                        Value = "3"
                    },
                    Operator = TokenType.Plus
                }
            };
            AssertExpressionAstFromInput(expectedAst, "1+2+3");
        }

        [Fact]
        internal void ParseExpression_OddNumberOfMultiplication()
        {
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Star,
                Lhs = new AstNumberExpression()
                {
                    Value = "1"
                },
                Rhs = new AstBinaryExpression()
                {
                    Lhs = new AstNumberExpression()
                    {
                        Value = "2"
                    },
                    Rhs = new AstNumberExpression()
                    {
                        Value = "3"
                    },
                    Operator = TokenType.Star
                }
            };

            AssertExpressionAstFromInput(expectedAst, "1*2*3");
        }

        [Fact]
        internal void ParseExpression_SubtractionSupport()
        {
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Minus,
                Lhs = new AstNumberExpression()
                {
                    Value = "1"
                },
                Rhs = new AstNumberExpression()
                {
                    Value = "2"
                }
            };
            AssertExpressionAstFromInput(expectedAst, "1-2");
        }

        [Fact]
        internal void ParseExpression_DivisionSupport()
        {
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Slash,
                Lhs = new AstNumberExpression()
                {
                    Value = "1"
                },
                Rhs = new AstNumberExpression()
                {
                    Value = "2"
                }
            };
            AssertExpressionAstFromInput(expectedAst, "1/2");
        }

        [Fact]
        internal void ParseExpression_UnaryExpression_Precedence()
        {
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Star,
                Lhs = new AstUnaryExpression()
                {
                    Operator = TokenType.Minus,
                    Expression = new AstNumberExpression()
                    {
                        Value = "6"
                    }
                },
                Rhs = new AstNumberExpression()
                {
                    Value = "2"
                },
            };

            AssertExpressionAstFromInput(expectedAst, "-6*2");
        }

        [Fact]
        internal void ParseExpression_UnaryExpression_SupportForPlusOperator()
        {
            var expectedAst = new AstUnaryExpression()
            {
                Operator = TokenType.Plus,
                Expression = new AstNumberExpression()
                {
                    Value = "6"
                }
            };

            AssertExpressionAstFromInput(expectedAst, "+6");
        }

        private void AssertExpressionAstFromInput(IAstNode expectedAst, string input) =>
            new TestParserVisitor()
                .AssertVisit(expectedAst, (IAstNode)ParserFactory(input).ParseExpression());

        Parser ParserFactory(string input) =>
            new Parser(
                new TokenSource(
                    new Lexer(
                        new CommandInput(input))));
    }
}
