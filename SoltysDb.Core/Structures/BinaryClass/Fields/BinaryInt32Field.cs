using System;

namespace SoltysDb.Core
{
    internal class BinaryInt32Field : BinaryField<Int32>
    {
        public BinaryInt32Field(byte[] memory, int offset) : base(memory, offset, sizeof(int))
        {
        }

        public override Int32 GetValue()
        {
            return BitConverter.ToInt32(this.fieldSpan.Span);
        }

        public override void SetValue(Int32 value)
        {
            BitConverter.GetBytes(value).CopyTo(this.fieldSpan);
        }

    }
}