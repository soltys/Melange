using System;

namespace SoltysDb.Core
{
    internal abstract class BinaryField
    {
        protected Memory<byte> FieldSpan;
        private readonly byte[] memoryHandler;

        protected BinaryField(byte[] memory, int offset, int fieldLength)
        {
            if (offset >= memory.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            this.Offset = offset;
            FieldLength = fieldLength;
            memoryHandler = memory;
            this.FieldSpan = new Memory<byte>(this.memoryHandler, Offset, fieldLength);
        }
        public int FieldLength { get; }

        public int Offset { get; private set; }

        public int FieldEnd => Offset + FieldLength;

        public BinaryField Next { get; set; }

        public void Move(int newOffset)
        {
            if (newOffset >= memoryHandler.Length )
            {
                throw new ArgumentOutOfRangeException(nameof(newOffset));
            }

            if (Next != null)
            {
                var newFieldEnd = newOffset + FieldLength;
                Next.Move(newFieldEnd);
            }

            var newSpan = new Memory<byte>(this.memoryHandler, newOffset, this.FieldLength);
            
            this.FieldSpan.CopyTo(newSpan);

            this.Offset = newOffset;
            this.FieldSpan = newSpan;
        }
    }
}