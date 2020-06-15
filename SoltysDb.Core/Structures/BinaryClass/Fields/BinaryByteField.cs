using System;

namespace SoltysDb.Core
{
    class BinaryByteField : BinaryField
    {
        public BinaryByteField(byte[] memory, int offset) : base(memory, offset, sizeof(byte))
        {
        }

        public byte ToValue()
        {
            return this.fieldSpan.ToArray()[0];
        }

        public void SetValue(byte value)
        {
            (new[] { value }).CopyTo(this.fieldSpan);
        }
    }
}
