using System.Collections.Generic;

namespace Soltys.VirtualMachine
{
    public class VM
    {
        public static RuntimeFactory DefaultRuntime
        {
            get;
        } = new RuntimeFactory();

        private readonly VMContext context;
        private readonly IRuntimeVisitor runtimeVisitor;

        public object? PeekValueStack => this.context.ValueStack.Count == 0 ? null : this.context.ValueStack.Peek();

        public VM() : this(DefaultRuntime)
        {
        }

        public VM(IRuntimeVisitorFactory runtimeVisitor)
        {
            this.context = new VMContext();
            this.runtimeVisitor = runtimeVisitor.Create(this.context);
        }

        public void Load(IEnumerable<VMFunction> instructions) =>
            this.context.Load(instructions);

        public void Run()
        {
            this.context.ChangeMethod(new CallEntry("Main", 0));

            while (this.context.IsHalted())
            {
                this.context.GetCurrentInstruction()?.Accept(this.runtimeVisitor);
                this.context.AdvanceInstructionPointer();
            }
        }

        public void Clear()
        {
            this.context.Clear();
        }
    }
}
