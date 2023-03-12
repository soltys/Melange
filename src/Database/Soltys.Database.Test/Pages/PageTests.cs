using Xunit;

namespace Soltys.Database.Test;

public class PageTests
{
    [Theory]
    [InlineData(0, PageKind.Undefined)]
    [InlineData(1, PageKind.Header)]
    [InlineData(2, PageKind.DataPage)]
    [InlineData(3, PageKind.KeyValue)]
    public void ConvertByte_IntoPageType(byte expectedPageTypeByte, PageKind pageKind)
    {
        var page = new Page {PageKind = pageKind};
        Assert.Equal(expectedPageTypeByte, page.RawData[0]);
    }

    [Fact]
    public void Position_Initial_IsLessThanZero()
    {
        var page = new Page();
        Assert.True(page.PageId < 0 , "PageId should be less than zero");
    }
}