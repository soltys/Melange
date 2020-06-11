using System;
using SoltysDb.Core.Pages;

namespace SoltysDb.Core
{
    internal class Page
    {
        public PageType PageType
        {
            get => (PageType)RawData[0];
            set => RawData[0] = (byte)value;
        }
        public const int ReservedBytes = 1;

        private readonly Memory<byte> usableData;
        public Span<byte> Data
        {
            get => usableData.Span;
            set => value.CopyTo(usableData.Span);
        }

        public const int PageSize = 4096;
        public byte[] RawData { get; } = new byte[PageSize];


        public Page()
        {
            usableData = new Memory<byte>(RawData, ReservedBytes, Page.PageSize - ReservedBytes);
        }

    }
}