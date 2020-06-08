using System.IO;
using Xunit;

namespace SoltysDb.Core.Test
{
    public class DatabaseDataTests
    {
        private readonly DatabaseData sut;
        private DatabaseData SutFactory =>  new DatabaseData(new MemoryStream());

        public DatabaseDataTests()
        {
            this.sut = SutFactory;
        }

        [Fact]
        public void IsNew_WithEmptyMemoryStream_ReturnsTrue()
        {
            Assert.True(sut.IsNew());
        }

        [Fact]
        public void Write_WritingHeaderFile_IsNewReturnsFalse()
        {
            sut.Write(new HeaderPage());
            Assert.False(sut.IsNew());
        }

        [Fact]
        public void Read_AfterWrite_ReturnsEqualData()
        {
            sut.Write(new DataPage(new byte[]{1,2,3}));

            var page = sut.Read(0);

            Assert.Equal(1,page.RawData[0]);
            Assert.Equal(2,page.RawData[1]);
            Assert.Equal(3,page.RawData[2]);
        }
    }
}
