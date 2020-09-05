using Xunit;

namespace SoltysDb.Test
{
    public class HeaderPageTests
    {
        [Fact]
        public void HeaderMetadata_HasConstantString()
        {
            var sut = new HeaderMetadata(new byte[512], 0);
            Assert.Equal("SOLTYSDB",sut.DatabaseName);
        }
    }
}
