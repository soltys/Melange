using Soltys.Library;

namespace Soltys.Database;

internal class PageMetadata : BinaryClass
{
    private readonly BinaryByteField pageTypeField;
    public PageKind PageKind
    {
        get => (PageKind)this.pageTypeField.GetValue();
        set => this.pageTypeField.SetValue((byte)value);
    }

    private readonly BinaryInt32Field pageIdField;
    public int PageId
    {
        get => this.pageIdField.GetValue();
        set => this.pageIdField.SetValue(value);
    }

    public int End { get; }

    public PageMetadata(byte[] metaDataBlock, int freeMemoryOffset) : base(metaDataBlock, freeMemoryOffset)
    {
        this.pageTypeField = AddByteField();
        this.pageIdField = AddInt32Field();

        End = this.pageIdField.FieldEnd;
    }
}

internal class Page
{
    protected readonly PageMetadata PageMetadata;

    public PageKind PageKind
    {
        get => this.PageMetadata.PageKind;
        set => this.PageMetadata.PageKind = value;
    }

    public int PageId
    {
        get => this.PageMetadata.PageId;
        set => this.PageMetadata.PageId = value;
    }

    public DataBlock DataBlock { get; protected set; }

    public const int PageSize = 4 * 1024; // 4 kb
    public byte[] RawData { get; } 

    public Page(PageKind pageKind = PageKind.Undefined) : this(new byte[Page.PageSize])
    {
        PageId = -1;
        this.PageMetadata.PageKind = pageKind;
    }

    public Page(byte[] rawData)
    {
        RawData = rawData;
        this.PageMetadata = new PageMetadata(RawData, 0);
        DataBlock = new DataBlock(RawData, this.PageMetadata.End, Page.PageSize - this.PageMetadata.End);
    }
}