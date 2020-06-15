using System;

namespace SoltysDb.Core
{
    internal class DataBlock
    {
        private const int NextBlockLocationOffset = 0 ;
        private const int NextBlockLocationFieldSize = sizeof(long);
        public long NextBlockLocation
        {
            get => BitConverter.ToInt64(this.metaData[DataBlock.NextBlockLocationOffset..(DataBlock.NextBlockLocationOffset + DataBlock.NextBlockLocationFieldSize)].Span);
            set => BitConverter.GetBytes(value).AsMemory().CopyTo(this.metaData.Slice(DataBlock.NextBlockLocationOffset, DataBlock.NextBlockLocationFieldSize));
        }

        private const int LengthOffset = DataBlock.NextBlockLocationFieldSize;
        private const int LengthFieldSize = sizeof(int);
        public int Length => BitConverter.ToInt32(this.metaData[DataBlock.LengthOffset..(DataBlock.LengthOffset + DataBlock.LengthFieldSize)].Span);

        private const int ReservedBytes = DataBlock.NextBlockLocationFieldSize + DataBlock.LengthFieldSize;
        
        private readonly Memory<byte> usableData;
        private Memory<byte> metaData;

        public Span<byte> Data
        {
            get => this.usableData.Span;
            set => value.CopyTo(this.usableData.Span);
        }

        public DataBlock(byte[] dataBlock, int offset, int length)
        {
            if (dataBlock == null)
            {
                throw new ArgumentNullException(nameof(dataBlock));
            }

            this.metaData = dataBlock.AsMemory(offset, DataBlock.ReservedBytes);
            this.usableData = new Memory<byte>(dataBlock, offset + DataBlock.ReservedBytes, length - DataBlock.ReservedBytes);


            //Save Length into meta data block
            BitConverter.GetBytes(length - DataBlock.ReservedBytes)
                .AsMemory()
                .CopyTo(this.metaData.Slice(DataBlock.LengthOffset, DataBlock.LengthFieldSize));
            
            NextBlockLocation = -1;
        }
    }
}
