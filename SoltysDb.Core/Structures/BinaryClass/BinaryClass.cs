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
        private int totalReservedBytes;

        public BinaryClass(byte[] rawData)
        {
            this.totalReservedBytes = 0;
            this.maxSize = rawData.Length;
            this.RawData = new byte[this.maxSize];
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
            totalReservedBytes = field.FieldEnd;
            return field;
        }

        protected BinaryInt32Field AddInt32Field()
            => (BinaryInt32Field)InitializeField(new BinaryInt32Field(this.RawData, totalReservedBytes));

        protected BinaryInt64Field AddInt64Field()
            => (BinaryInt64Field)InitializeField(new BinaryInt64Field(this.RawData, totalReservedBytes));

        protected BinaryByteField AddByteField()
            => (BinaryByteField)InitializeField(new BinaryByteField(this.RawData, totalReservedBytes));

        protected BinaryBooleanField AddBooleanField()
            => (BinaryBooleanField)InitializeField(new BinaryBooleanField(this.RawData, totalReservedBytes));

        protected BinaryDoubleField AddDoubleField()
            => (BinaryDoubleField)InitializeField(new BinaryDoubleField(this.RawData, totalReservedBytes));

        protected BinaryStringNVarField AddStringNVarField(int maxStringLength)
            => (BinaryStringNVarField)InitializeField(new BinaryStringNVarField(this.RawData, totalReservedBytes, maxStringLength));
    }
}
