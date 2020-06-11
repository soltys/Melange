using System.IO;
using SoltysDb.Core.Pages;
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
            sut.Write(new HeaderPage(new Page()));
            Assert.False(sut.IsNew());
        }

        [Fact]
        public void Read_AfterWrite_ReturnsEqualData()
        {
            sut.Write(new DataPage(new Page(new byte[]{2,2,3})));

            var page = sut.Read(0);

            Assert.Equal(PageType.DataPage,page.PageType);
            Assert.Equal(2,page.Data[0]);
            Assert.Equal(3,page.Data[1]);
        }
    }
}
