using Soltys.Library.TextAnalysis;
using Xunit;

namespace Soltys.Database.Test.Cmd
{
    public class TokenHelperTests
    {
        [Theory]
        [InlineData(CmdTokenKind.Select, 0)] //default value test
        [InlineData(CmdTokenKind.And, 1)] //keyword and operator for boolean expressions
        [InlineData(CmdTokenKind.Plus, 2)]
        [InlineData(CmdTokenKind.Star, 3)]
        public void GetPrecedence_CorrectValueIsReturned(CmdTokenKind cmdTokenKind, int expectedValue)
        {
            Assert.Equal(expectedValue, cmdTokenKind.GetPrecedence());
        }

        [Theory]
        [InlineData(CmdTokenKind.Select, Associativity.Left)] //default value test
        [InlineData(CmdTokenKind.Plus, Associativity.Left)]
        [InlineData(CmdTokenKind.Star, Associativity.Left)]
        [InlineData(CmdTokenKind.Power, Associativity.Right)]
        public void GeGetAssociativity_CorrectValueIsReturned(CmdTokenKind cmdTokenKind, Associativity associativity)
        {
            Assert.Equal(associativity, cmdTokenKind.GetAssociativity());
        }
    }
}
