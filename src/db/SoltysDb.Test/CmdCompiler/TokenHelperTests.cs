using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public class TokenHelperTests
    {
        [Theory]
        [InlineData(TokenType.Select, 0)] //default value test
        [InlineData(TokenType.And, 1)] //keyword and operator for boolean expressions
        [InlineData(TokenType.Plus, 2)]
        [InlineData(TokenType.Star, 3)]
        public void GetPrecedence_CorrectValueIsReturned(TokenType tokenType, int expectedValue)
        {
            Assert.Equal(expectedValue, tokenType.GetPrecedence());
        }

        [Theory]
        [InlineData(TokenType.Select, Associativity.Left)] //default value test
        [InlineData(TokenType.Plus, Associativity.Left)]
        [InlineData(TokenType.Star, Associativity.Left)]
        [InlineData(TokenType.Power, Associativity.Right)]
        public void GeGetAssociativity_CorrectValueIsReturned(TokenType tokenType, Associativity associativity)
        {
            Assert.Equal(associativity, tokenType.GetAssociativity());
        }
    }
}
