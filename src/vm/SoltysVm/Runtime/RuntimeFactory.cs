namespace SoltysVm
{
    internal class RuntimeFactory : IRuntimeVisitorFactory
    {
        public IRuntimeVisitor Create(IVMContext context) => new RuntimeVisitor(context);
    }
}
