namespace Soltys.VirtualMachine
{
    public class RuntimeFactory : IRuntimeVisitorFactory
    {
        public IRuntimeVisitor Create(IVMContext context) => new RuntimeVisitor(context);
    }
}
