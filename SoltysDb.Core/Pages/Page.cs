using System;

namespace SoltysDb.Core
{
    internal class PageMetadata : BinaryClass
    {
        private readonly BinaryByteField pageTypeField;
        public PageType PageType
        {
            get => (PageType)this.pageTypeField.GetValue();
            set => this.pageTypeField.SetValue((byte)value);
        }

        private readonly BinaryInt64Field positionField;
        public long Position
        {
            get => this.positionField.GetValue();
            set => this.positionField.SetValue(value);
        }

        public int End { get; }

        public PageMetadata(byte[] metaDataBlock, int freeMemoryOffset) : base(metaDataBlock, freeMemoryOffset)
        {
            this.pageTypeField = AddByteField();
            this.positionField = AddInt64Field();

            End = this.positionField.FieldEnd;
        }
    }

    internal class Page : IPage
    {
        protected readonly PageMetadata PageMetadata;

        public PageType PageType
        {
            get => this.PageMetadata.PageType;
            set => this.PageMetadata.PageType = value;
        }

        public long Position
        {
            get => this.PageMetadata.Position;
            set => this.PageMetadata.Position = value;
        }

        public DataBlock DataBlock { get; protected set; }

        public const int PageSize = 4 * 1024; // 4 kb
        public byte[] RawData { get; } 

        public Page(PageType pageType = PageType.Undefined) : this(new byte[Page.PageSize])
        {
            Position = -1;
            this.PageMetadata.PageType = pageType;
        }

        public Page(byte[] rawData)
        {
            RawData = rawData;
            this.PageMetadata = new PageMetadata(RawData, 0);
            DataBlock = new DataBlock(RawData, this.PageMetadata.End, Page.PageSize - this.PageMetadata.End);
        }
    }
}