using System;
using Xunit;

namespace Soltys.Library.Test
{
    public class BinaryFieldTests
    {
        [Fact]
        public void Constructor_SetOffsetProperty()
        {
            var memory = new byte[8];
            var byteField = new BinaryByteField(memory, 1);

            Assert.Equal(1, byteField.Offset);
        }

        [Fact]
        public void Constructor_OffsetOutOfRangeMemory_ArgumentOutOfRangeExceptionIsThrown()
        {
            var memory = new byte[8];
            Assert.Throws<ArgumentOutOfRangeException>("offset", () => new BinaryByteField(memory, 8));
        }

        [Fact]
        public void Move_MovesBytesToNewLocationInByteArray()
        {
            var memory = new byte[8];
            var byteField = new BinaryByteField(memory, 1);
            byteField.SetValue(100);

            Assert.Equal(100, memory[1]);
            Assert.Equal(0, memory[7]);

            byteField.Move(7);

            //Old place have old value
            Assert.Equal(100, memory[1]);

            //New place have old value
            Assert.Equal(100, memory[7]);
            Assert.Equal(100, byteField.GetValue());

            //new value is set in new place
            byteField.SetValue(111);
            Assert.Equal(111, byteField.GetValue());
            Assert.Equal(111, memory[7]);

            //old place have old value
            Assert.Equal(100, memory[1]);
        }

        [Fact]
        public void Move_UpdatesOffsetProperty()
        {
            var memory = new byte[8];
            var byteField = new BinaryByteField(memory, 1);

            byteField.Move(7);

            Assert.Equal(7, byteField.Offset);
        }

        [Fact]
        public void Move_MovesAlsoFieldSetAsNext()
        {
            var memory = new byte[8];
            var byteField1 = new BinaryByteField(memory, 1);
            var byteField2 = new BinaryByteField(memory, 2);

            byteField1.Next = byteField2;


            byteField1.SetValue(100);
            byteField2.SetValue(200);

            byteField1.Move(5);

            Assert.Equal(100, memory[5]);
            Assert.Equal(200, memory[6]);
        }

        [Fact]
        public void Move_OutOfMemory_OutOfRangeExceptionIsThrown()
        {
            var memory = new byte[8];
            var byteField1 = new BinaryByteField(memory, 1);

            Assert.Throws<ArgumentOutOfRangeException>("newOffset", () => byteField1.Move(8));
        }
    }
}
