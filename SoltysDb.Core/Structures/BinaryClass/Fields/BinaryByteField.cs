using System;

namespace SoltysDb.Core
{
    class BinaryByteField : BinaryField
    {
        public BinaryByteField(byte[] memory, int offset) : base(memory, offset, sizeof(byte))
        {
        }

        public byte GetValue()
        {
            return this.FieldSpan.ToArray()[0];
        }

        public void SetValue(byte value)
        {
            (new[] { value }).CopyTo(this.FieldSpan);
        }
    }
}
