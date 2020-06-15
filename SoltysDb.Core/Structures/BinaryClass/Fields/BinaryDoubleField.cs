using System;

namespace SoltysDb.Core
{
    internal class BinaryDoubleField : BinaryField<double>
    {
        public BinaryDoubleField(byte[] memory, int offset) : base(memory, offset, sizeof(double))
        {
        }

        public override double GetValue()
        {
            return BitConverter.ToDouble(this.fieldSpan.Span);
        }

        public override void SetValue(double value)
        {
            BitConverter.GetBytes(value).CopyTo(this.fieldSpan);
        }
    }
}