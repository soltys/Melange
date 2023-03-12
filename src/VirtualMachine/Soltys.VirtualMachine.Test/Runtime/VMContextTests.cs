using Soltys.VirtualMachine.Contracts;
using Xunit;

namespace Soltys.VirtualMachine.Test.Runtime;

public class VMContextTests
{
    [Fact]
    public void FindFunction_SearchesInVmLibraries_WhenFoundReturnsReference()
    {
        var sut = new VMContext();
        var vmLibrary = new TestVmLibrary("foo", "bar", "john");
        sut.AddVMLibrary(vmLibrary);
        var actual = sut.FindExternalFunction("bar");
        Assert.Same(vmLibrary.Functions["bar"], actual);
    }

    [Fact]
    public void FindFunction_WhenSearchingMethodIsNotFound_ThrowsException()
    {
        var sut = new VMContext();
        Assert.Throws<InvalidOperationException>(() => sut.FindExternalFunction("notExistingMethod"));
    }

    private class TestVmLibrary : IVMLibrary
    {
        private readonly Dictionary<string, IVMExternalFunction> functions;

        private class DummyFunc : IVMExternalFunction
        {
            public int ArgumentCount => 0;
            public object Execute(params object[] args) => null;
        }
        public TestVmLibrary(params string[] functionNames)
        {
            this.functions = new Dictionary<string, IVMExternalFunction>();
            foreach (var name in functionNames)
            {
                this.functions.Add(name, new DummyFunc());
            }
        }
        public IReadOnlyDictionary<string, IVMExternalFunction> Functions => this.functions;
    }
}
