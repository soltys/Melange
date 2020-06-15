using System;

namespace SoltysDb.Core
{
    internal class Page : IPage
    {

        private const int PageTypeOffset = 0;
        private const int PageTypeFieldSize = sizeof(PageType);
        public PageType PageType
        {
            get => (PageType)RawData[Page.PageTypeOffset];
            set => RawData[Page.PageTypeOffset] = (byte)value;
        }

        private const int PositionOffset = Page.PageTypeFieldSize;
        private const int PositionFieldSize = sizeof(long);
        public long Position
        {
            get => BitConverter.ToInt32(RawData[Page.PositionOffset..(Page.PositionOffset + Page.PositionFieldSize)]);
            set
            {
                var positionBytes = BitConverter.GetBytes(value);
                Buffer.BlockCopy(positionBytes, 0, RawData, Page.PositionOffset, positionBytes.Length);
            }
        }

        //PageType (byte) + Position (int - 4 byte)
        private const int ReservedBytes = Page.PageTypeFieldSize + Page.PositionFieldSize;

        public DataBlock DataBlock { get; }

        public const int PageSize = 4096;
        public byte[] RawData { get; } = new byte[Page.PageSize];
        
        public Page()
        {
            DataBlock = new DataBlock(RawData, Page.ReservedBytes, Page.PageSize - Page.ReservedBytes);
            Position = -1;
        }
    }
}