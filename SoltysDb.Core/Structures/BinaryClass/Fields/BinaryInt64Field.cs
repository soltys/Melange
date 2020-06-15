using System;

namespace SoltysDb.Core
{
    internal class BinaryInt64Field : BinaryField<Int64>
    {
        public BinaryInt64Field(byte[] memory, int offset) : base(memory, offset, sizeof(long))
        {
        }

        public override Int64 GetValue()
        {
            return BitConverter.ToInt64(this.fieldSpan.Span);
        }

        public override void SetValue(Int64 value)
        {
            BitConverter.GetBytes(value).CopyTo(this.fieldSpan);
        }
    }
}