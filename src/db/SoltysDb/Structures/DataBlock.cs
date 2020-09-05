using System;
using SoltysLib;

namespace SoltysDb
{
    internal class DataBlockMetadata : BinaryClass
    {
        private readonly BinaryInt64Field nextBlockLocationField;
        public long NextPageLocation
        {
            get => this.nextBlockLocationField.GetValue();
            set => this.nextBlockLocationField.SetValue(value);
        }

        public DataBlockMetadata(byte[] metaDataBlock, int offset) : base(metaDataBlock)
        {
            this.nextBlockLocationField = new BinaryInt64Field(metaDataBlock, offset);

            MetaDataEnd = this.nextBlockLocationField.FieldEnd;
        }

        public int MetaDataEnd { get; private set; }
    }

    internal class DataBlock
    {
        private readonly Memory<byte> usableData;
        private readonly DataBlockMetadata metaData;

        public long NextPageLocation
        {
            get => this.metaData.NextPageLocation;
            set => this.metaData.NextPageLocation = value;
        }

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

            this.metaData = new DataBlockMetadata(dataBlock, offset) {  };
            this.usableData = new Memory<byte>(dataBlock, this.metaData.MetaDataEnd, length - this.metaData.MetaDataEnd);
        }
    }
}
