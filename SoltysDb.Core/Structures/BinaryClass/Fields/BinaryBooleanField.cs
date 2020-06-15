using System;

namespace SoltysDb.Core
{
    internal class BinaryBooleanField : BinaryField<Boolean>
    {
        public BinaryBooleanField(byte[] memory, int offset) : base(memory, offset, sizeof(bool))
        {
        }

        public override Boolean GetValue()
        {
            return BitConverter.ToBoolean(this.fieldSpan.Span);
        }

        public override void SetValue(bool value)
        {
            BitConverter.GetBytes(value).CopyTo(this.fieldSpan);
        }
    }
}