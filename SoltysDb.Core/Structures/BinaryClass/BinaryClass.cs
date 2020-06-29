using System;
using System.Collections.Generic;
using System.Linq;

namespace SoltysDb.Core
{
    internal class BinaryClass
    {
        List<BinaryField> binaryFields = new List<BinaryField>();

        protected readonly byte[] memoryHandler;
        private int freeMemoryOffset;

        public BinaryClass(byte[] dataSource) : this(dataSource, 0)
        {
        }

        public BinaryClass(byte[] dataSource, int freeMemoryOffset)
        {
            this.freeMemoryOffset = freeMemoryOffset;
            this.memoryHandler = dataSource;
        }

        public byte[] ToBytes()
        {
            return this.memoryHandler.ToArray();
        }

        public void SetBytes(byte[] rawData)
        {
            rawData.CopyTo(this.memoryHandler.AsSpan());
        }

        private BinaryField InitializeField(BinaryField field)
        {
            this.binaryFields.Add(field);
            this.freeMemoryOffset = field.FieldEnd;
            return field;
        }

        protected BinaryInt32Field AddInt32Field()
            => (BinaryInt32Field)InitializeField(new BinaryInt32Field(this.memoryHandler, this.freeMemoryOffset));

        protected BinaryInt64Field AddInt64Field()
            => (BinaryInt64Field)InitializeField(new BinaryInt64Field(this.memoryHandler, this.freeMemoryOffset));

        protected BinaryByteField AddByteField()
            => (BinaryByteField)InitializeField(new BinaryByteField(this.memoryHandler, this.freeMemoryOffset));

        protected BinaryBooleanField AddBooleanField()
            => (BinaryBooleanField)InitializeField(new BinaryBooleanField(this.memoryHandler, this.freeMemoryOffset));

        protected BinaryDoubleField AddDoubleField()
            => (BinaryDoubleField)InitializeField(new BinaryDoubleField(this.memoryHandler, this.freeMemoryOffset));

        protected BinaryStringNVarField AddStringNVarField(int maxStringLength)
            => (BinaryStringNVarField)InitializeField(new BinaryStringNVarField(this.memoryHandler, this.freeMemoryOffset, maxStringLength));
        
        protected  BinaryStringNVarField AddStringNVarField()
            => (BinaryStringNVarField)InitializeField(new BinaryStringNVarField(this.memoryHandler, this.freeMemoryOffset, BitConverter.ToInt32(this.memoryHandler, this.freeMemoryOffset)));
    }
}
