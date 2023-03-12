using Soltys.VirtualMachine.Contracts;

namespace Soltys.Lisp.CoreLib;

public class CoreLibrary : IVMLibrary
{
    private readonly Dictionary<string, IVMExternalFunction> functions = new Dictionary<string, IVMExternalFunction>();
    public CoreLibrary()
    {
        this.functions.Add("println",
            new VMExternalFunction(new Action<string>(Console.WriteLine)));
        this.functions.Add("strcat", new VMExternalFunction(
            new Func<string, string, string>(string.Concat)));
        this.functions.Add("str", new VMExternalFunction
            (new Func<object, string>((o) => o.ToString())));
        this.functions.Add("quote", new VMExternalFunction(new Func<object, object>((o) => o)));
    }

    public IReadOnlyDictionary<string, IVMExternalFunction> Functions => this.functions;
}
