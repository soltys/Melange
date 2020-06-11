using System;

namespace SoltysDb.Core
{
    internal class DataPage : IPage
    {
        private readonly Page page;

        public Span<byte> Data
        {
            get => this.page.Data;
            set => this.page.Data = value;
        }

        public byte[] RawData => this.page.RawData;

        public DataPage(Page page)
        {
            this.page = page ?? throw new ArgumentNullException(nameof(page));
            this.page.PageType = PageType.DataPage;
        }
    }
}