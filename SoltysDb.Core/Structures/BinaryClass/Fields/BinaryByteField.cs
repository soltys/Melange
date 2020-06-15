using System;

namespace SoltysDb.Core
{
    class BinaryByteField : BinaryField<byte>
    {
        public BinaryByteField(byte[] memory, int offset) : base(memory, offset, sizeof(byte))
        {
        }

        public override byte GetValue()
        {
            return this.fieldSpan.ToArray()[0];
        }

        public override void SetValue(byte value)
        {
            (new[] { value }).CopyTo(this.fieldSpan);
        }
    }
}
