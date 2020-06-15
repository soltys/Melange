using System;

namespace SoltysDb.Core
{
    internal class BinaryInt32Field : BinaryField
    {
        public BinaryInt32Field(byte[] memory, int offset) : base(memory, offset, sizeof(int))
        {
        }

        public Int32 ToValue()
        {
            return BitConverter.ToInt32(fieldSpan.Span);
        }

        public void SetValue(Int32 value)
        {
            BitConverter.GetBytes(value).CopyTo(this.fieldSpan);
        }

    }
}