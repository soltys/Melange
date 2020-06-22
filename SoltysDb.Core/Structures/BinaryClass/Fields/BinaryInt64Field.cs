using System;

namespace SoltysDb.Core
{
    internal class BinaryInt64Field : BinaryField
    {
        public BinaryInt64Field(byte[] memory, int offset) : base(memory, offset, sizeof(long))
        {
        }

        public Int64 GetValue()
        {
            return BitConverter.ToInt64(this.FieldSpan.Span);
        }

        public void SetValue(Int64 value)
        {
            BitConverter.GetBytes(value).CopyTo(this.FieldSpan);
        }
    }
}