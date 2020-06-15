using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SoltysDb.Core.Test.Structures
{
    public class DataBlockTests
    {
        //[Fact]
        //public void Length_AfterCreationIsSmallerThanOriginalValue_By12Bytes()
        //{
        //    var memoryBlock = new byte[128];
        //    var sut = new DataBlock(memoryBlock, 0, memoryBlock.Length);

        //    //length is smaller than initial block of memory to hold information about it's length where data is located
        //    Assert.Equal(116, sut.Length);
        //}

        //[Fact]
        //public void NextBlockLocation_AfterCreation_LessThanZero()
        //{
        //    var sut = new DataBlock(new byte[16], 0, 12);
        //    Assert.Equal(-1, sut.NextBlockLocation);
        //}

        [Fact]
        public void Constructor_OnPassedNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DataBlock(null, 0, 0));
        }
    }
}
