using System.IO;
using Xunit;

namespace SoltysDb.Core.Test
{
    public class DatabaseDataTests
    {
        private readonly DatabaseData sut;
        private DatabaseData SutFactory => new DatabaseData(new MemoryStream());

        public DatabaseDataTests()
        {
            this.sut = SutFactory;
        }

        [Fact]
        public void IsNew_WithEmptyMemoryStream_ReturnsTrue()
        {
            Assert.True(this.sut.IsNew());
        }

        [Fact]
        public void Write_WritingHeaderFile_IsNewReturnsFalse()
        {
            this.sut.Write(new Page(PageType.Header));
            Assert.False(this.sut.IsNew());
        }

        [Fact]
        public void Read_AfterWrite_ReturnsEqualData()
        {

            var dataPage = new Page(PageType.DataPage);
            dataPage.DataBlock.Data = new byte[] {1, 2, 3};
            this.sut.Write(dataPage);

            var page = this.sut.Read(0);

            Assert.Equal(1, page.DataBlock.Data[0]);
            Assert.Equal(2, page.DataBlock.Data[1]);
            Assert.Equal(3, page.DataBlock.Data[2]);
        }

        [Fact]
        public void Write_ReturnsPagePosition()
        {
            var dataPage1 = new Page(PageType.DataPage);
            var dataPage2 = new Page(PageType.DataPage);

            var position1 = this.sut.Write(dataPage1);
            Assert.Equal(0,dataPage1.Position);
            Assert.Equal(position1, dataPage1.Position);

            var position2 = this.sut.Write(dataPage2);
            Assert.Equal(Page.PageSize,dataPage2.Position);
            Assert.Equal(position2,dataPage2.Position);
        }

     
    }
}
