namespace Soltys.VirtualMachine.Contracts;

public class VMExternalFunction : IVMExternalFunction
{
    private readonly Delegate del;

    public VMExternalFunction(Delegate de)
    {
        this.del = de;
    }

    public int ArgumentCount => this.del.Method.GetParameters().Length;

    public object Execute(params object[] args)
    {
        return this.del.DynamicInvoke(args);
    }
}
