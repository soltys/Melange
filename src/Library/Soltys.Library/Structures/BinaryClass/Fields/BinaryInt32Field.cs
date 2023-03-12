namespace Soltys.Library;

public class BinaryInt32Field : BinaryField
{ 
    public BinaryInt32Field(byte[] memory, int offset) : base(memory, offset, sizeof(int))
    {
    }

    public Int32 GetValue()
    {
        return BitConverter.ToInt32(this.FieldSpan.Span);
    }

    public void SetValue(Int32 value)
    {
        BitConverter.GetBytes(value).CopyTo(this.FieldSpan);
    }
}
