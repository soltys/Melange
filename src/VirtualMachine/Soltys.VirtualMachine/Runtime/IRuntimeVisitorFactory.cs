namespace Soltys.VirtualMachine
{
    public interface IRuntimeVisitorFactory
    {
        IRuntimeVisitor Create(IVMContext context);
    }
}
