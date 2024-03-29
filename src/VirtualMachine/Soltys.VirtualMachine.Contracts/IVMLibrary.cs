namespace Soltys.VirtualMachine.Contracts;

public interface IVMLibrary
{
    IReadOnlyDictionary<string, IVMExternalFunction> Functions
    {
        get;
    }
}
