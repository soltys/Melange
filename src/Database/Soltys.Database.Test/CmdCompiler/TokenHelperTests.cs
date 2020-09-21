using Xunit;

namespace Soltys.Database.Test.CmdCompiler
{
    public class TokenHelperTests
    {
        [Theory]
        [InlineData(TokenKind.Select, 0)] //default value test
        [InlineData(TokenKind.And, 1)] //keyword and operator for boolean expressions
        [InlineData(TokenKind.Plus, 2)]
        [InlineData(TokenKind.Star, 3)]
        public void GetPrecedence_CorrectValueIsReturned(TokenKind tokenKind, int expectedValue)
        {
            Assert.Equal(expectedValue, tokenKind.GetPrecedence());
        }

        [Theory]
        [InlineData(TokenKind.Select, Associativity.Left)] //default value test
        [InlineData(TokenKind.Plus, Associativity.Left)]
        [InlineData(TokenKind.Star, Associativity.Left)]
        [InlineData(TokenKind.Power, Associativity.Right)]
        public void GeGetAssociativity_CorrectValueIsReturned(TokenKind tokenKind, Associativity associativity)
        {
            Assert.Equal(associativity, tokenKind.GetAssociativity());
        }
    }
}
