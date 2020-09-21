using System;

namespace Soltys.Library
{
    public class BinaryDoubleField : BinaryField
    {
        public BinaryDoubleField(byte[] memory, int offset) : base(memory, offset, sizeof(double))
        {
        }

        public double GetValue()
        {
            return BitConverter.ToDouble(this.FieldSpan.Span);
        }

        public void SetValue(double value)
        {
            BitConverter.GetBytes(value).CopyTo(this.FieldSpan);
        }
    }
}
