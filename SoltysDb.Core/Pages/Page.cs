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

        public PageMetadata(byte[] metaDataBlock, int offset) : base(metaDataBlock)
        {
            this.pageTypeField = new BinaryByteField(metaDataBlock, offset);
            this.positionField = new BinaryInt64Field(metaDataBlock, this.pageTypeField.FieldEnd);

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
        public byte[] RawData { get; } = new byte[Page.PageSize];

        public Page(PageType pageType = PageType.Undefined)
        {
            this.PageMetadata = new PageMetadata(RawData, 0) { PageType = pageType };
            DataBlock = new DataBlock(RawData, this.PageMetadata.End, Page.PageSize - this.PageMetadata.End);
            Position = -1;
        }
    }
}