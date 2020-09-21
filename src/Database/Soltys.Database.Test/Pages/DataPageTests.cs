using System;
using Xunit;

namespace Soltys.Database.Test
{
    public class DataPageTests
    {
        [Fact]
        public void Length_DataPage_IsSameAsEveryPage()
        {
            var sut = new Page(PageKind.DataPage);
            Assert.Equal(Page.PageSize, sut.RawData.Length);
        }

        [Fact]
        public void RawData_ContainsDataFromConstructor()
        {
            var sut = new Page(PageKind.DataPage);
            sut.DataBlock.Data = new Span<byte>(new byte[] {1, 2, 3});
            Assert.Equal(Page.PageSize, sut.RawData.Length);

            Assert.Equal(1, sut.DataBlock.Data[0]);
            Assert.Equal(2, sut.DataBlock.Data[1]);
            Assert.Equal(3, sut.DataBlock.Data[2]);
            Assert.Equal(0, sut.DataBlock.Data[3]);
        }
    }
}
