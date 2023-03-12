using Xunit;

namespace Soltys.VirtualMachine.Test;

public class LoadTypeConstantTests
{

    [Fact]
    public void Constructor_NullValuePasses_RaisesNullArgumentException() => 
        Assert.Throws<ArgumentNullException>("value", () => new LoadConstantInstruction(LoadKind.Integer, null));

    [Fact]
    public void Constructor_InvalidLoadTypePassed_RaisesArgumentOutRangeException() =>
        Assert.Throws<ArgumentOutOfRangeException>("loadKind",
            () => new LoadConstantInstruction(LoadKind.Argument, 69));
}
