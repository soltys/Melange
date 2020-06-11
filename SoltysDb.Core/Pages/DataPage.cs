using System;

namespace SoltysDb.Core
{
    internal class DataPage : IPage
    {
        private readonly Page page;
        public Span<byte> Data => this.page.Data;
        public byte[] RawData => this.page.RawData;

        public DataPage(Page page)
        {
            this.page = page;
        }
    }
}