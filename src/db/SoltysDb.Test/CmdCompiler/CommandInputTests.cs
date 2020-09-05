using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    public class CommandInputTests
    {
        [Fact]
        public void CommandInput_StringPassedInConstructor_ReadToEnd()
        {
            var sut = new CommandInput("abcdef");
            Assert.Equal("abcdef", sut.GetToEnd());
        }
    }
}
