using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class TokenTests
    {
        [Fact]
        public void Token_Holds_Value()
        {
            var token = new Token(TokenType.Insert, "insert");

            Assert.Equal(TokenType.Insert, token.TokenType);
            Assert.Equal("insert", token.Value);
        }

        [Fact]
        public void Token_Default_Values()
        {
            var token = new Token();
            Assert.Equal(TokenType.Undefined, token.TokenType);
            Assert.Null(token.Value);
        }
    }
}
