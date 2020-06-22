using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SoltysDb.Core.Test.Structures
{
public    class BinaryFieldTests
    {
        [Fact]
        public void Move_MovesBytesToNewLocationInByteArray()
        {
            var memory = new byte[8];

            var byteField = new BinaryByteField(memory, 1 );
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
    }
}
