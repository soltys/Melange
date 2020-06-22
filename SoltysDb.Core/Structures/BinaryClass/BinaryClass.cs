using System;
using System.Collections.Generic;
using System.Linq;

namespace SoltysDb.Core
{
    internal class BinaryClass
    {
        List<BinaryField> binaryFields = new List<BinaryField>();

        protected readonly byte[] RawData;
        private readonly int maxSize;
        private int reservedBytes;

        public BinaryClass(int maxSize)
        {
            this.reservedBytes = 0;
            this.maxSize = maxSize;
            this.RawData = new byte[this.maxSize];
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

        private BinaryField InitializeField(BinaryField field)
        {
            binaryFields.Add(field);
            reservedBytes = field.FieldEnd;
            return field;
        }

        protected BinaryInt32Field AddInt32Field()
            => (BinaryInt32Field)InitializeField(new BinaryInt32Field(this.RawData, reservedBytes));

        protected BinaryInt64Field AddInt64Field()
            => (BinaryInt64Field)InitializeField(new BinaryInt64Field(this.RawData, reservedBytes));

        protected BinaryByteField AddByteField()
            => (BinaryByteField)InitializeField(new BinaryByteField(this.RawData, reservedBytes));

        protected BinaryBooleanField AddBooleanField()
            => (BinaryBooleanField)InitializeField(new BinaryBooleanField(this.RawData, reservedBytes));

        protected BinaryDoubleField AddDoubleField()
            => (BinaryDoubleField)InitializeField(new BinaryDoubleField(this.RawData, reservedBytes));

        protected BinaryStringNVarField AddStringNVarField(int maxStringLength)
            => (BinaryStringNVarField)InitializeField(new BinaryStringNVarField(this.RawData, reservedBytes, maxStringLength));
    }
}
