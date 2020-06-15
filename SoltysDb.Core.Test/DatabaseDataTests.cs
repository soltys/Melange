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

            var dataPage = new DataPage(new Page());
            dataPage.DataBlock.Data = new byte[] {1, 2, 3};
            sut.Write(dataPage);

            var page = (DataPage)sut.Read(0);

            Assert.Equal(1, page.DataBlock.Data[0]);
            Assert.Equal(2, page.DataBlock.Data[1]);
            Assert.Equal(3, page.DataBlock.Data[2]);
        }

        [Fact]
        public void Read_InvalidPageType_ThrowsException()
        {
            var page = new Page();
            page.RawData[0] = 200;

            sut.Write(page);
            Assert.Throws<DbInvalidOperationException>(()=>sut.Read(0));
        }

        [Fact]
        public void Write_ReturnsPagePostion()
        {
            var dataPage1 = new DataPage(new Page());
            var dataPage2 = new DataPage(new Page());

            var position1 = sut.Write(dataPage1);
            Assert.Equal(0,dataPage1.Position);
            Assert.Equal(position1, dataPage1.Position);

            var position2 = sut.Write(dataPage2);
            Assert.Equal(Page.PageSize,dataPage2.Position);
            Assert.Equal(position2,dataPage2.Position);
        }

     
    }
}
