using System;
using SoltysDb.Core.Pages;

namespace SoltysDb.Core
{
    internal class Page
    {
        //public PageType PageType { get; set; }
        public PageType PageType
        {
            get
            {
                return (PageType)RawData[0];
            }
            set
            {
                RawData[0] = (byte)PageType;
            }
        }
        public const int ReservedBytes = 1;

        private readonly Memory<byte> usableData;
        public Span<byte> Data => usableData.Span;

        public const int PageSize = 4096;
        public byte[] RawData { get; } = new byte[PageSize];


        public Page()
        {
            usableData = new Memory<byte>(RawData, ReservedBytes, Page.PageSize - ReservedBytes);
        }

        public Page(byte[] rawData)
        {
            Array.Copy(rawData, RawData, rawData.Length);
            usableData = new Memory<byte>(RawData, ReservedBytes, Page.PageSize - ReservedBytes);
        }
    }
}