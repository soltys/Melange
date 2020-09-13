using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public partial class ParserTests
    {
        [Fact]
        public void ParseStatement_Insert()
        {
            var expectedAst = new AstInsertStatement()
            {
                Location = new AstLocation() { Value = "soltysdb_kv" },
                Values = new AstValue(new[]
                {
                    new AstExpression() {Value = "aaa"},
                    new AstExpression() {Value = "bbb"}
                })
            };

            AstAssert.Statement(expectedAst, "INSERT INTO soltysdb_kv VALUES (aaa,bbb)");
        }


    }
}
