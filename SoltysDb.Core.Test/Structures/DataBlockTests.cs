using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SoltysDb.Core.Test.Structures
{
    public class DataBlockTests
    {
        [Fact]
        public void NextBlockLocation_IsSavedInAttachedMemoryBlock()
        {
            var memoryBlock = new byte[128];
            var sut = new DataBlock(memoryBlock, 0, memoryBlock.Length) {NextBlockLocation = 123};


            var newInstance = new DataBlock(memoryBlock,0, memoryBlock.Length);
            Assert.Equal(123, newInstance.NextBlockLocation);
        }

        [Fact]
        public void Constructor_OnPassedNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DataBlock(null, 0, 0));
        }
    }
}
