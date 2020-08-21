using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class ParserTests
    {
        [Fact]
        internal void ParseExpression_OperatorPrecedence_MultiplicationLowerInTreeAdditionHigher()
        {
            var parser = ParserFactory("1+2*3");
            var ast = parser.ParseExpression();
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Plus,
                LeftExpression = new AstNumberExpression()
                {
                    Value = "1"
                },
                RightExpression = new AstBinaryExpression()
                {
                    LeftExpression = new AstNumberExpression()
                    {
                        Value = "2"
                    },
                    RightExpression = new AstNumberExpression()
                    {
                        Value = "3"
                    },
                    Operator = TokenType.Star
                }
            };
            var assertVisitor = new TestParserVisitor();
            assertVisitor.AssertVisit(expectedAst, (IAstNode)ast);
        }

        [Fact]
        internal void ParseExpression_ParenthesisPrecedence_AdditionInParenthesisLowerInAstTreeThanMultiplication()
        {
            var parser = ParserFactory("(1+2)*3");

            var ast = parser.ParseExpression();
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Star,
                LeftExpression = new AstBinaryExpression()
                {
                    LeftExpression = new AstNumberExpression()
                    {
                        Value = "1"
                    },
                    RightExpression = new AstNumberExpression()
                    {
                        Value = "2"
                    },
                    Operator = TokenType.Plus
                },
                RightExpression = new AstNumberExpression()
                {
                    Value = "3"
                },
            };
            var assertVisitor = new TestParserVisitor();
            assertVisitor.AssertVisit(expectedAst, (IAstNode)ast);
        }

        [Fact]
        internal void ParseExpression_OddNumberOfAddition()
        {
            var parser = ParserFactory("1+2+3");
            var ast = parser.ParseExpression();
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Plus,
                LeftExpression = new AstNumberExpression()
                {
                    Value = "1"
                },
                RightExpression = new AstBinaryExpression()
                {
                    LeftExpression = new AstNumberExpression()
                    {
                        Value = "2"
                    },
                    RightExpression = new AstNumberExpression()
                    {
                        Value = "3"
                    },
                    Operator = TokenType.Plus
                }
            };
            var assertVisitor = new TestParserVisitor();
            assertVisitor.AssertVisit(expectedAst, (IAstNode)ast);
        }

        [Fact]
        internal void ParseExpression_OddNumberOfMultiplication()
        {
            var parser = ParserFactory("1*2*3");
            var ast = parser.ParseExpression();
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Star,
                LeftExpression = new AstNumberExpression()
                {
                    Value = "1"
                },
                RightExpression = new AstBinaryExpression()
                {
                    LeftExpression = new AstNumberExpression()
                    {
                        Value = "2"
                    },
                    RightExpression = new AstNumberExpression()
                    {
                        Value = "3"
                    },
                    Operator = TokenType.Star
                }
            };
            var assertVisitor = new TestParserVisitor();
            assertVisitor.AssertVisit(expectedAst, (IAstNode)ast);
        }

        [Fact]
        internal void ParseExpression_UnaryExpression()
        {
            var parser = ParserFactory("-6*2");
            var ast = parser.ParseExpression();
            var expectedAst = new AstBinaryExpression()
            {
                Operator = TokenType.Star,
                LeftExpression = new AstUnaryExpression()
                {
                    Operator = TokenType.Minus,
                    Expression = new AstNumberExpression()
                    {
                        Value = "6"
                    }
                },
                RightExpression = new AstNumberExpression()
                {
                    Value = "2"
                },
            };
            var assertVisitor = new TestParserVisitor();
            assertVisitor.AssertVisit(expectedAst, (IAstNode)ast);
        }

        Parser ParserFactory(string input) =>
            new Parser(
                new TokenSource(
                    new Lexer(
                        new CommandInput(input))));
    }
}
