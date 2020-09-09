using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public partial class ParserTests
    {
        [Fact]
        internal void ParseExpression_OperatorPrecedence_MultiplicationLowerInTreeAdditionHigher()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = TokenType.Plus,
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
                    Operator = TokenType.Star
                }
            };

            AstAssert.Expression(expectedAst, "2+2*2");
        }

        [Fact]
        internal void ParseExpression_OperatorPrecedence_MultiplicationLowerInTreeAdditionHigher_DifferentOrder()
        {
            var expectedAst = new AstBinaryExpression
            {
                Operator = TokenType.Plus,
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
                    Operator = TokenType.Star
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
                Operator = TokenType.Star,
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
                    Operator = TokenType.Plus
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
                Operator = TokenType.Plus,
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
                    Operator = TokenType.Plus
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
                Operator = TokenType.Star,
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
                    Operator = TokenType.Star
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
                Operator = TokenType.Minus,
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
                Operator = TokenType.Slash,
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
                Operator = TokenType.Star,
                Lhs = new AstUnaryExpression
                {
                    Operator = TokenType.Minus,
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
                Operator = TokenType.Plus,
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
                Operator = TokenType.GreaterThan,
                Lhs = new AstNumberExpression { Value = "3" },
                Rhs = new AstBinaryExpression
                {
                    Operator = TokenType.Plus,
                    Lhs = new AstNumberExpression { Value = "1" },
                    Rhs = new AstNumberExpression { Value = "2" },
                }
            };

            AstAssert.Expression(expectedAst, "3>1+2");
        }
    }
}
