using System;

namespace SoltysDb.Core
{
    internal class DataBlock
    {
        private const int NextBlockLocationOffset = 0 ;
        private const int NextBlockLocationFieldSize = sizeof(long);
        public long NextBlockLocation
        {
            get => BitConverter.ToInt64(metaData[NextBlockLocationOffset..(NextBlockLocationOffset + NextBlockLocationFieldSize)].Span);
            set => BitConverter.GetBytes(value).AsMemory().CopyTo(metaData.Slice(NextBlockLocationOffset, NextBlockLocationFieldSize));
        }

        private const int LengthOffset = NextBlockLocationFieldSize;
        private const int LengthFieldSize = sizeof(int);
        public int Length => BitConverter.ToInt32(metaData[LengthOffset..(LengthOffset + LengthFieldSize)].Span);

        private const int ReservedBytes = NextBlockLocationFieldSize + LengthFieldSize;
        
        private readonly Memory<byte> usableData;
        private Memory<byte> metaData;

        public Span<byte> Data
        {
            get => usableData.Span;
            set => value.CopyTo(usableData.Span);
        }

        public DataBlock(byte[] dataBlock, int offset, int length)
        {
            if (dataBlock == null)
            {
                throw new ArgumentNullException(nameof(dataBlock));
            }

            metaData = dataBlock.AsMemory(offset, ReservedBytes);
            usableData = new Memory<byte>(dataBlock, offset + ReservedBytes, length - ReservedBytes);


            //Save Length into meta data block
            BitConverter.GetBytes(length - ReservedBytes)
                .AsMemory()
                .CopyTo(metaData.Slice(LengthOffset, LengthFieldSize));
            
            NextBlockLocation = -1;
        }
    }
}
