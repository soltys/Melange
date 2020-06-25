using Xunit;

namespace SoltysDb.Core.Test.Pages
{
    public class PageTests
    {
        [Theory]
        [InlineData(1, PageType.Header)]
        [InlineData(2, PageType.DataPage)]
        [InlineData(3, PageType.KeyValue)]
        public void ConvertByte_IntoPageType(byte expectedPageTypeByte, PageType pageType)
        {
            var page = new Page();
            page.PageType = pageType;
            Assert.Equal(expectedPageTypeByte, page.RawData[0]);
        }

        [Fact]
        public void Position_Initial_IsLessThanZero()
        {
            var page = new Page();
            Assert.True(page.Position < 0 , "Position should be less than zero");
        }
    }
}
