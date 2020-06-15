using System;

namespace SoltysDb.Core
{
    internal class BinaryDoubleField : BinaryField
    {
        public BinaryDoubleField(byte[] memory, int offset) : base(memory, offset, sizeof(double))
        {
        }

        public double ToValue()
        {
            return BitConverter.ToDouble(fieldSpan.Span);
        }

        public void SetValue(double value)
        {
            BitConverter.GetBytes(value).CopyTo(this.fieldSpan);
        }
    }
}