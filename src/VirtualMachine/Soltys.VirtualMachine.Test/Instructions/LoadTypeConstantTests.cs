using System;
using Xunit;

namespace Soltys.VirtualMachine.Test
{
    public class LoadTypeConstantTests
    {

        [Fact]
        public void Constructor_NullValuePasses_RaisesNullArgumentException() => 
            Assert.Throws<ArgumentNullException>("value", () => new LoadConstantInstruction(LoadType.Integer, null));

        [Fact]
        public void Constructor_InvalidLoadTypePassed_RaisesArgumentOutRangeException() =>
            Assert.Throws<ArgumentOutOfRangeException>("loadType",
                () => new LoadConstantInstruction(LoadType.Argument, 69));
    }
}
