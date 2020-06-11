using System;
using System.Collections.Generic;
using System.Text;
using SoltysDb.Core.Pages;
using Xunit;

namespace SoltysDb.Core.Test.Pages
{
    public class PageTests
    {
        [Theory]
        [InlineData(new byte[] { 1 }, PageType.Header)]
        [InlineData(new byte[] { 2 }, PageType.DataPage)]
        [InlineData(new byte[] { 3 }, PageType.KeyValue)]
        public void ConvertByte_IntoPageType(byte[] rawData, PageType expectedPageType)
        {
            var page = new Page(rawData);
            Assert.Equal(expectedPageType, page.PageType);
        }
    }
}
