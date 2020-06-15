using System;

namespace SoltysDb.Core
{
    internal class Page : IPage
    {

        private const int PageTypeOffset = 0;
        private const int PageTypeFieldSize = sizeof(PageType);
        public PageType PageType
        {
            get => (PageType)RawData[PageTypeOffset];
            set => RawData[PageTypeOffset] = (byte)value;
        }

        private const int PositionOffset = PageTypeFieldSize;
        private const int PositionFieldSize = sizeof(long);
        public long Position
        {
            get => BitConverter.ToInt32(RawData[PositionOffset..(PositionOffset + PositionFieldSize)]);
            set
            {
                var positionBytes = BitConverter.GetBytes(value);
                Buffer.BlockCopy(positionBytes, 0, RawData, PositionOffset, positionBytes.Length);
            }
        }

        //PageType (byte) + Position (int - 4 byte)
        private const int ReservedBytes = PageTypeFieldSize + PositionFieldSize;

        public DataBlock DataBlock { get; }

        public const int PageSize = 4096;
        public byte[] RawData { get; } = new byte[PageSize];
        
        public Page()
        {
            DataBlock = new DataBlock(RawData, ReservedBytes, Page.PageSize - ReservedBytes);
            Position = -1;
        }
    }
}