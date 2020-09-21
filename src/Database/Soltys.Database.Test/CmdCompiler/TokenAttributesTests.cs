using Xunit;

namespace Soltys.Database.Test.CmdCompiler
{
    public class TokenAttributesTests
    {
        [Fact]
        public void OperatorAttribute_DefaultValuesAreSet()
        {
            var attribute = new OperatorAttribute();

            Assert.Equal(0, attribute.Precedence);
            Assert.Equal(Associativity.Left, attribute.Associativity);
        }
    }
}
