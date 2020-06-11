using SoltysDb.Core.Pages;
using Xunit;

namespace SoltysDb.Core.Test.Pages
{
    public class PageTests
    {
        [Theory]
        [InlineData(1, PageType.Header)]
        [InlineData(2, PageType.DataPage)]
        [InlineData(3, PageType.KeyValue)]
        public void ConvertByte_IntoPageType(byte expectedPageTypeByte, PageType PageType)
        {
            var page = new Page { PageType = PageType };
            Assert.Equal(expectedPageTypeByte, page.RawData[0]);
        }
    }
}
