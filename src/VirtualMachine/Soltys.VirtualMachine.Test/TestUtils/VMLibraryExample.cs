using Soltys.VirtualMachine.Contracts;

namespace Soltys.VirtualMachine.Test.TestUtils;

public class VMLibraryExample : IVMLibrary
{
    private readonly Dictionary<string, IVMExternalFunction> functions;

    public VMLibraryExample()
    {
        this.functions = new Dictionary<string, IVMExternalFunction>();
        Func<int, int, int> a = (a, b) => a + b;
        this.functions.Add("add", new VMExternalFunction(a));
    }

    public IReadOnlyDictionary<string, IVMExternalFunction> Functions => this.functions;

}
