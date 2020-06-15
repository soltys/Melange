using System;
using System.Text;

namespace SoltysDb.Core
{
    internal class HeaderPage : DataPage
    {
        public HeaderPage(Page page) : base(page)
        {
            var byteHeader = Encoding.ASCII.GetBytes("SOLTYSDB");
            byteHeader.AsSpan().CopyTo(DataBlock.Data);
        }
    }
}