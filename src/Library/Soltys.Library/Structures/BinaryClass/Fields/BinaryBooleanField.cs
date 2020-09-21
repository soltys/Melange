using System;

namespace Soltys.Library
{
    public class BinaryBooleanField : BinaryField
    { 
        public BinaryBooleanField(byte[] memory, int offset) : base(memory, offset, sizeof(bool))
        {
        }

        public Boolean GetValue()
        {
            return BitConverter.ToBoolean(this.FieldSpan.Span);
        }

        public void SetValue(bool value)
        {
            BitConverter.GetBytes(value).CopyTo(this.FieldSpan);
        }
    }
}
