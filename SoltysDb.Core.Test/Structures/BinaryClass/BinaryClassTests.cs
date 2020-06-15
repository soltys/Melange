using Xunit;

namespace SoltysDb.Core.Test.Structures
{
    public class BinaryClassTests
    {
        [Fact]
        public void ToBytesAndSetBytes_SameValues()
        {
            var sut = new MyClass
            {
                Id = 42,
                LongValue = 666L,
                DoubleValue = 3.1415,
                BoolValue = true,
                StringValue = "foo",
                ByteValue = 100
            };


            byte[] bytes = sut.ToBytes();

            var newInstance = new MyClass();
            newInstance.SetBytes(bytes);

            Assert.Equal(42, newInstance.Id);
            Assert.Equal(666L, newInstance.LongValue);
            Assert.Equal(3.1415, newInstance.DoubleValue, 4);
            Assert.True(newInstance.BoolValue);
            Assert.Equal("foo", sut.StringValue);
            Assert.Equal(100, sut.ByteValue);
        }
    }


    internal class MyClass : BinaryClass
    {
        public MyClass() : base(1024)
        {
            idField = new BinaryInt32Field(RawData, 0);
            longValueField = new BinaryInt64Field(RawData, idField.FieldEnd);
            doubleValueField = new BinaryDoubleField(RawData, longValueField.FieldEnd);
            binaryBooleanField = new BinaryBooleanField(RawData, doubleValueField.FieldEnd);
            stringField = new BinaryStringNVarField(RawData, binaryBooleanField.FieldEnd, 16);
            byteField = new BinaryByteField(RawData, stringField.FieldEnd);
        }

        private readonly BinaryInt32Field idField;

        public int Id
        {
            get => this.idField.ToValue();
            set => this.idField.SetValue(value);
        }

        private readonly BinaryInt64Field longValueField;

        public long LongValue
        {
            get => this.longValueField.ToValue();
            set => this.longValueField.SetValue(value);
        }

        private readonly BinaryDoubleField doubleValueField;

        public double DoubleValue
        {
            get => this.doubleValueField.ToValue();
            set => this.doubleValueField.SetValue(value);
        }

        private readonly BinaryBooleanField binaryBooleanField;

        public bool BoolValue
        {
            get => this.binaryBooleanField.ToValue();
            set => this.binaryBooleanField.SetValue(value);
        }

        private readonly BinaryStringNVarField stringField;

        public string StringValue
        {
            get => this.stringField.ToValue();
            set => this.stringField.SetValue(value);
        }

        private readonly BinaryByteField byteField;

        public byte ByteValue
        {
            get => this.byteField.ToValue();
            set => this.byteField.SetValue(value);
        }


    }
}