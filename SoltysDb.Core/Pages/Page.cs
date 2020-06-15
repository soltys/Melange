using System;

namespace SoltysDb.Core
{

    internal class PageMetadata : BinaryClass
    {
        private readonly BinaryByteField pageTypeField;
        public PageType PageType
        {
            get => (PageType) this.pageTypeField.GetValue();
            set => this.pageTypeField.SetValue((byte)value);
        }

        private readonly BinaryInt64Field positionField;
        public long Position
        {
            get => this.positionField.GetValue();
            set => this.positionField.SetValue(value);
        }

        public int MetaDataEnd { get; }

        public PageMetadata(byte[] metaDataBlock, int offset):base(metaDataBlock)
        {
            this.pageTypeField = new BinaryByteField(metaDataBlock, offset);
            this.positionField = new BinaryInt64Field(metaDataBlock, this.pageTypeField.FieldEnd);

            MetaDataEnd = this.positionField.FieldEnd;
        }
    }

    internal class Page : IPage
    {
        private readonly PageMetadata metaData;
        
        public PageType PageType
        {
            get => this.metaData.PageType;
            set => this.metaData.PageType = value;
        }

        public long Position
        {
            get => this.metaData.Position;
            set => this.metaData.Position = value;
        }

        public DataBlock DataBlock { get; }

        public const int PageSize = 4 * 1024; // 4 kb
        public byte[] RawData { get; } = new byte[Page.PageSize];

        public Page()
        {
            this.metaData = new PageMetadata(RawData, 0);
            DataBlock = new DataBlock(RawData, this.metaData.MetaDataEnd, Page.PageSize - this.metaData.MetaDataEnd);
            Position = -1;
        }
    }
}