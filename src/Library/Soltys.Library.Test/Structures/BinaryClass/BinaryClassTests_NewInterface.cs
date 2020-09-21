using Xunit;

namespace Soltys.Library.Test
{
    public class BinaryClassTests_NewInterface
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

        internal class MyClass : BinaryClass
        {
            public MyClass() : base(new byte[1024])
            {
                this.idField = AddInt32Field();
                this.longValueField = AddInt64Field();
                this.doubleValueField = AddDoubleField();
                this.binaryBooleanField = AddBooleanField();
                this.stringField = AddStringNVarField(16);
                this.byteField = AddByteField();
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
