using Xunit;

namespace SoltysLib.Test
{
    public class BinaryClassTests_OldInterface
    {
        [Fact]
        public void ToBytesAndSetBytes_SameValues()
        {
            var sut = new MyClass
            {
                IntValue = 42,
                LongValue = 666L,
                DoubleValue = 3.1415,
                BoolValue = true,
                StringValue = "foo",
                ByteValue = 100
            };


            byte[] bytes = sut.ToBytes();

            var newInstance = new MyClass();
            newInstance.SetBytes(bytes);

            Assert.Equal(42, newInstance.IntValue);
            Assert.Equal(666L, newInstance.LongValue);
            Assert.Equal(3.1415, newInstance.DoubleValue, 4);
            Assert.True(newInstance.BoolValue);
            Assert.Equal("foo", sut.StringValue);
            Assert.Equal(100, sut.ByteValue);
        }


        internal class MyClass : SoltysLib.BinaryClass
        {
            public MyClass() : base(new byte[1024])
            {
                this.idField = new BinaryInt32Field(this.memoryHandler, 0);
                this.longValueField = new BinaryInt64Field(this.memoryHandler, this.idField.FieldEnd);
                this.doubleValueField = new BinaryDoubleField(this.memoryHandler, this.longValueField.FieldEnd);
                this.binaryBooleanField = new BinaryBooleanField(this.memoryHandler, this.doubleValueField.FieldEnd);
                this.stringField = new BinaryStringNVarField(this.memoryHandler, this.binaryBooleanField.FieldEnd, 16);
                this.byteField = new BinaryByteField(this.memoryHandler, this.stringField.FieldEnd);
            }

            private readonly BinaryInt32Field idField;
            public int IntValue
            {
                get => this.idField.GetValue();
                set => this.idField.SetValue(value);
            }

            private readonly BinaryInt64Field longValueField;
            public long LongValue
            {
                get => this.longValueField.GetValue();
                set => this.longValueField.SetValue(value);
            }

            private readonly BinaryDoubleField doubleValueField;
            public double DoubleValue
            {
                get => this.doubleValueField.GetValue();
                set => this.doubleValueField.SetValue(value);
            }

            private readonly BinaryBooleanField binaryBooleanField;
            public bool BoolValue
            {
                get => this.binaryBooleanField.GetValue();
                set => this.binaryBooleanField.SetValue(value);
            }

            private readonly BinaryStringNVarField stringField;
            public string StringValue
            {
                get => this.stringField.GetValue();
                set => this.stringField.SetValue(value);
            }

            private readonly BinaryByteField byteField;
            public byte ByteValue
            {
                get => this.byteField.GetValue();
                set => this.byteField.SetValue(value);
            }
        }
    }


}
