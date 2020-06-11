using System;

namespace SoltysDb.Core
{
    internal class Page : IPage
    {

        private const int PageTypeFieldSize = 1;
        public PageType PageType
        {
            get => (PageType)RawData[0];
            set => RawData[0] = (byte)value;
        }


        private const int PositionFieldSize = 4;
        public int Position
        {
            get => BitConverter.ToInt32(RawData[1..5]);
            set
            {
                var positionBytes = BitConverter.GetBytes(value);
                Buffer.BlockCopy( positionBytes, 0, RawData, 1, positionBytes.Length);
            }
        }
      
        //PageType (byte) + Position (int - 4 byte)
        private const int ReservedBytes = PageTypeFieldSize + PositionFieldSize;

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
            Position = -1;
        }
    }
}