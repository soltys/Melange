using System;
using System.Linq;

namespace SoltysDb.Core
{
    internal class BinaryClass
    {
        protected readonly byte[] RawData;
        private int size;

        public BinaryClass(int size)
        {
            this.size = size;
            this.RawData = new byte[this.size];
        }

        public BinaryClass(byte[] rawData) : this(rawData.Length)
        {
            rawData.CopyTo(this.RawData.AsSpan());
        }

        public byte[] ToBytes()
        {
            return this.RawData.ToArray();
        }

        public void SetBytes(byte[] rawData)
        {
            rawData.CopyTo(this.RawData.AsSpan());
        }
    }
}
