using Xunit;

namespace Soltys.Database.Test.CmdCompiler
{
    public class TokenTests
    {
        [Fact]
        public void Token_Holds_Value()
        {
            var token = new Token(TokenKind.Insert, "insert");

            Assert.Equal(TokenKind.Insert, token.TokenKind);
            Assert.Equal("insert", token.Value);
        }

        [Fact]
        public void Token_Default_Values()
        {
            var token = new Token();
            Assert.Equal(TokenKind.Undefined, token.TokenKind);
            Assert.Null(token.Value);
        }

        [Fact]
        public void ToString_HasProperFormat()
        {
            var token = new Token(TokenKind.Id, "Value");
            Assert.Equal("<Id,Value>", token.ToString());
        }
    }
}
