namespace Soltys.VirtualMachine.Contracts
{
    public interface IVMExternalFunction
    {
        int ArgumentCount
        {
            get;
        }

        object Execute(params object[] args);
    }
}
