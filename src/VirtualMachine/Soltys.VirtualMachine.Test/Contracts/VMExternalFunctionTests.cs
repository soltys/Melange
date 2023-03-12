using Soltys.VirtualMachine.Contracts;
using Xunit;

namespace Soltys.VirtualMachine.Test.Contracts;

public class VMExternalFunctionTests
{
    [Fact]
    internal void Execute_IsExecutingWrapped_MethodIsCalled()
    {
        var actionWasCalled = false;
        var externalFunction = new VMExternalFunction(new Action(() =>
        {
            actionWasCalled = true;
        }));

        externalFunction.Execute();
        Assert.Equal(0, externalFunction.ArgumentCount);
        Assert.True(actionWasCalled);
    }

    [Fact]
    internal void Execute_ExecuteIsPassingArguments_ToWrappedMethod()
    {
        var actionWasCalled = false;
        var externalFunction = new VMExternalFunction(new Action<int, int>((a, b) =>
        {
            actionWasCalled = true;

            Assert.Equal(1, a);
            Assert.Equal(2, b);
        }));

        externalFunction.Execute(1, 2);
        Assert.Equal(2, externalFunction.ArgumentCount);
        Assert.True(actionWasCalled);
    }
}
