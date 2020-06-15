using System;

namespace SoltysDb.Core
{
    internal class BinaryBooleanField : BinaryField
    {
        public BinaryBooleanField(byte[] memory, int offset) : base(memory, offset, sizeof(bool))
        {
        }

        public Boolean ToValue()
        {
            return BitConverter.ToBoolean(fieldSpan.Span);
        }

        public void SetValue(bool value)
        {
            BitConverter.GetBytes(value).CopyTo(this.fieldSpan);
        }
    }
}