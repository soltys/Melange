using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

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

        public object PeekValueStack => this.context.ValueStack.Peek();

        public VM() : this(DefaultRuntime)
        {
        }

        public VM(IRuntimeVisitorFactory runtimeVisitor)
        {
            this.context = new VMContext();
            this.runtimeVisitor = runtimeVisitor.Create(this.context);
        }

        public void Load(Stream source)
        {
            this.context.Load(InstructionDecoder.DecodeStream(source));
        }

        public void Load(IEnumerable<IInstruction> instructions) =>
            this.context.Load(instructions);

        public void Run()
        {
            this.context.Reset();

            while (this.context.IsHalted())
            {
                this.context.GetCurrentInstruction().Accept(this.runtimeVisitor);
                this.context.AdvanceInstructionPointer();
            }
        }
    }
}
