using Xunit;

namespace SoltysDb.Core.Test
{
    public class DataPageTests
    {
        [Fact]
        public void Length_DataPage_IsSameAsEveryPage()
        {
            var sut = new DataPage();
            Assert.Equal(Page.PageSize,sut.RawData.Length);
        }

        [Fact]
        public void RawData_ContainsDataFromConstructor()
        {
            var sut = new DataPage(new byte[]{1,2,3});
            Assert.Equal(Page.PageSize, sut.RawData.Length);

            Assert.Equal(1, sut.RawData[0]);
            Assert.Equal(2, sut.RawData[1]);
            Assert.Equal(3, sut.RawData[2]);
            Assert.Equal(0, sut.RawData[3]);
        }
    }
}
