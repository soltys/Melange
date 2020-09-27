using Xunit;

namespace Soltys.Database.Test.Cmd
{
    public partial class ParserTests
    {
        [Fact]
        internal void ParseExpression_OperatorPrecedence_MultiplicationLowerInTreeAdditionHigher()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.Plus,
                Lhs = new AstNumberExpression
                {
                    Value = "2"
                },
                Rhs = new AstBinaryExpression
                {
                    Lhs = new AstNumberExpression
                    {
                        Value = "2"
                    },
                    Rhs = new AstNumberExpression
                    {
                        Value = "2"
                    },
                    Operator = CmdTokenKind.Star
                }
            };

            AstAssert.Expression(expectedAst, "2+2*2");
        }

        [Fact]
        internal void ParseExpression_OperatorPrecedence_MultiplicationLowerInTreeAdditionHigher_DifferentOrder()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.Plus,
                Lhs = new AstBinaryExpression
                {
                    Lhs = new AstNumberExpression
                    {
                        Value = "2"
                    },
                    Rhs = new AstNumberExpression
                    {
                        Value = "2"
                    },
                    Operator = CmdTokenKind.Star
                },
                Rhs = new AstNumberExpression
                {
                    Value = "2"
                },
            };

            AstAssert.Expression(expectedAst, "2*2+2");
        }

        [Fact]
        internal void ParseExpression_ParenthesisPrecedence_AdditionInParenthesisLowerInAstTreeThanMultiplication()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.Star,
                Lhs = new AstBinaryExpression
                {
                    Lhs = new AstNumberExpression
                    {
                        Value = "1"
                    },
                    Rhs = new AstNumberExpression
                    {
                        Value = "2"
                    },
                    Operator = CmdTokenKind.Plus
                },
                Rhs = new AstNumberExpression
                {
                    Value = "3"
                },
            };

            AstAssert.Expression(expectedAst, "(1+2)*3");
        }

        [Fact]
        internal void ParseExpression_OddNumberOfAddition()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.Plus,
                Lhs = new AstBinaryExpression
                {
                    Lhs = new AstNumberExpression
                    {
                        Value = "1"
                    },
                    Rhs = new AstNumberExpression
                    {
                        Value = "2"
                    },
                    Operator = CmdTokenKind.Plus
                },
                Rhs = new AstNumberExpression
                {
                    Value = "3"
                }
            };
            AstAssert.Expression(expectedAst, "1+2+3");
        }

        [Fact]
        internal void ParseExpression_OddNumberOfMultiplication()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.Star,
                Lhs = new AstBinaryExpression
                {
                    Lhs = new AstNumberExpression
                    {
                        Value = "1"
                    },
                    Rhs = new AstNumberExpression
                    {
                        Value = "2"
                    },
                    Operator = CmdTokenKind.Star
                },
                Rhs = new AstNumberExpression
                {
                    Value = "3"
                },
            };

            AstAssert.Expression(expectedAst, "1*2*3");
        }

        [Fact]
        internal void ParseExpression_SubtractionSupport()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.Minus,
                Lhs = new AstNumberExpression
                {
                    Value = "1"
                },
                Rhs = new AstNumberExpression
                {
                    Value = "2"
                }
            };
            AstAssert.Expression(expectedAst, "1-2");
        }

        [Fact]
        internal void ParseExpression_DivisionSupport()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.Slash,
                Lhs = new AstNumberExpression
                {
                    Value = "1"
                },
                Rhs = new AstNumberExpression
                {
                    Value = "2"
                }
            };
            AstAssert.Expression(expectedAst, "1/2");
        }

        [Fact]
        internal void ParseExpression_UnaryExpression_Precedence()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.Star,
                Lhs = new AstUnaryExpression
                {
                    Operator = CmdTokenKind.Minus,
                    Expression = new AstNumberExpression
                    {
                        Value = "6"
                    }
                },
                Rhs = new AstNumberExpression
                {
                    Value = "2"
                },
            };

            AstAssert.Expression(expectedAst, "-6*2");
        }

        [Fact]
        internal void ParseExpression_UnaryExpression_SupportForPlusOperator()
        {
            var expectedAst = new AstUnaryExpression
            {
                Operator = CmdTokenKind.Plus,
                Expression = new AstNumberExpression
                {
                    Value = "6"
                }
            };

            AstAssert.Expression(expectedAst, "+6");
        }

        [Fact]
        public void ParseExpression_BooleanExpression_SupportFor()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = CmdTokenKind.GreaterThan,
                Lhs = new AstNumberExpression { Value = "3" },
                Rhs = new AstBinaryExpression
                {
                    Operator = CmdTokenKind.Plus,
                    Lhs = new AstNumberExpression { Value = "1" },
                    Rhs = new AstNumberExpression { Value = "2" },
                }
            };

            AstAssert.Expression(expectedAst, "3>1+2");
        }
    }
}
