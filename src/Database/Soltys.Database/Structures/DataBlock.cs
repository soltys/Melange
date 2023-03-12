using Soltys.Library;

namespace Soltys.Database;

internal class DataBlockMetadata : BinaryClass
{
    private readonly BinaryInt32Field nextBlockPageIdField;
    public int NextPageId
    {
        get => this.nextBlockPageIdField.GetValue();
        set => this.nextBlockPageIdField.SetValue(value);
    }

    public DataBlockMetadata(byte[] metaDataBlock, int offset) : base(metaDataBlock)
    {
        this.nextBlockPageIdField = new BinaryInt32Field(metaDataBlock, offset);

        MetaDataEnd = this.nextBlockPageIdField.FieldEnd;
    }

    public int MetaDataEnd
    {
        get;
    }
}

internal class DataBlock
{
    private readonly Memory<byte> usableData;
    private readonly DataBlockMetadata metaData;

    public int NextPageId
    {
        get => this.metaData.NextPageId;
        set => this.metaData.NextPageId = value;
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

        this.metaData = new DataBlockMetadata(dataBlock, offset) { };
        this.usableData = new Memory<byte>(dataBlock, this.metaData.MetaDataEnd, length - this.metaData.MetaDataEnd);
    }
}
