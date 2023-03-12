namespace Soltys.Library;

public abstract class BinaryField
{
    protected Memory<byte> FieldSpan;
    private readonly byte[] memoryHandler;

    protected BinaryField(byte[] memory, int offset, int fieldLength)
    {
        if (offset >= memory.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        Offset = offset;
        FieldLength = fieldLength;
        this.memoryHandler = memory;
        this.FieldSpan = new Memory<byte>(this.memoryHandler, Offset, fieldLength);
    }
    public int FieldLength { get; }

    public int Offset { get; private set; }

    public int FieldEnd => Offset + FieldLength;

    public BinaryField? Next { get; set; }

    public void Move(int newOffset)
    {
        if (newOffset >= this.memoryHandler.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(newOffset));
        }

        if (Next != null)
        {
            var newFieldEnd = newOffset + FieldLength;
            Next.Move(newFieldEnd);
        }

        var newSpan = new Memory<byte>(this.memoryHandler, newOffset, FieldLength);

        this.FieldSpan.CopyTo(newSpan);

        Offset = newOffset;
        this.FieldSpan = newSpan;
    }
}
