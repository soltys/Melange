using SoltysLib.TextAnalysis;
using Xunit;

namespace SoltysLib.Test.TextAnalysis
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
