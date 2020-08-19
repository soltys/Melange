using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class ParserTests
    {
        //https://en.wikipedia.org/wiki/Recursive_descent_parser
        [Fact]
        internal void ParserTest_OperatorPrecedence_RecursiveDescentParser()
        {
            var parser = ParserFactory("2+2*2");
            var ast = parser.ParseExpression();
            Assert.IsType<AstBinaryExpression>(ast);
            var astBinaryExpression = (AstBinaryExpression)ast;
            Assert.Equal(TokenType.Plus, astBinaryExpression.Operator);
        }

        [Fact]
        internal void ParserTest_Parenthesis_Support()
        {
            var parser = ParserFactory("(2+2)*2");
            var ast = parser.ParseExpression();
            Assert.IsType<AstBinaryExpression>(ast);
            var astBinaryExpression = (AstBinaryExpression)ast;
            Assert.Equal(TokenType.Star, astBinaryExpression.Operator);
        }

        Parser ParserFactory(string input) =>
            new Parser(
                new TokenSource(
                    new Lexer(
                        new CommandInput(input))));


    }
}
