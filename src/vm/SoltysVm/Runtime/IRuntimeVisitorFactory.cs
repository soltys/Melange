namespace SoltysVm
{
    public interface IRuntimeVisitorFactory
    {
        IRuntimeVisitor Create(IVMContext context);
    }
}