using System;

namespace SoltysDb.Core
{
    internal class DataPage : IPage
    {
        private readonly Page page;

        public DataBlock DataBlock => this.page.DataBlock;

        public long Position
        {
            get => this.page.Position;
            set => this.page.Position = value;
        }

        public byte[] RawData => this.page.RawData;

        public DataPage(Page page)
        {
            this.page = page ?? throw new ArgumentNullException(nameof(page));
            this.page.PageType = PageType.DataPage;
        }
    }
}