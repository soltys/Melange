using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class ParserTests
    {
        //https://en.wikipedia.org/wiki/Recursive_descent_parser
        [Fact]
        internal void ParseExpression_OperatorPrecedence_RecursiveDescentParser()
        {
            var parser = ParserFactory("2+2*2");
            var ast = parser.ParseExpression();
            Assert.IsType<AstBinaryExpression>(ast);
            var astBinaryExpression = (AstBinaryExpression)ast;
            Assert.Equal(TokenType.Plus, astBinaryExpression.Operator);
        }

        [Fact]
        internal void ParseExpression_Parenthesis_PrioritySupport()
        {
            var parser = ParserFactory("(2+2)*2");
            var ast = parser.ParseExpression();
            Assert.IsType<AstBinaryExpression>(ast);
            var astBinaryExpression = (AstBinaryExpression)ast;
            Assert.Equal(TokenType.Star, astBinaryExpression.Operator);
        }

        [Fact]
        internal void ParseExpression_OddNumberOfAddition()
        {
            var parser = ParserFactory("1+2+3");
            var ast = parser.ParseExpression();
            Assert.IsType<AstBinaryExpression>(ast);
            var astBinaryExpression = (AstBinaryExpression)ast;
            Assert.Equal(TokenType.Plus, astBinaryExpression.Operator);
            Assert.IsType<AstNumberExpression>(astBinaryExpression.LeftExpression);
            Assert.IsType<AstBinaryExpression>(astBinaryExpression.RightExpression);
        }

        [Fact]
        internal void ParseExpression_OddNumberOfMultiplication()
        {
            var parser = ParserFactory("1*2*3");
            var ast = parser.ParseExpression();
            Assert.IsType<AstBinaryExpression>(ast);
            var astBinaryExpression = (AstBinaryExpression)ast;
            Assert.Equal(TokenType.Star, astBinaryExpression.Operator);
            Assert.IsType<AstNumberExpression>(astBinaryExpression.LeftExpression);
            Assert.IsType<AstBinaryExpression>(astBinaryExpression.RightExpression);
        }

        [Fact]
        internal void ParseExpression_MoreComplexArithmicExpression()
        {
            var parser = ParserFactory("1+2*3+4");
            var ast = parser.ParseExpression();
            var astBinaryExpression = (AstBinaryExpression)ast;
            Assert.Equal(TokenType.Plus, astBinaryExpression.Operator);
            Assert.IsType<AstNumberExpression>(astBinaryExpression.LeftExpression);
            Assert.IsType<AstBinaryExpression>(astBinaryExpression.RightExpression);
            var rightExpression = (AstBinaryExpression)astBinaryExpression.RightExpression;
            Assert.Equal(TokenType.Plus, rightExpression.Operator);
            Assert.IsType<AstBinaryExpression>(rightExpression.LeftExpression);
            Assert.Equal("4", rightExpression.RightExpression.Value);

        }

        Parser ParserFactory(string input) =>
            new Parser(
                new TokenSource(
                    new Lexer(
                        new CommandInput(input))));


    }
}
