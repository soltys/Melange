using System.IO;

namespace SoltysVm
{
    internal class VM
    {
        private readonly VMContext context;
        private readonly IRuntimeVisitor runtimeVisitor;

        public object PeekValueStack => this.context.ValueStack.Peek();

        public VM(IRuntimeVisitorFactory runtimeVisitor)
        {
            this.context = new VMContext();
            this.runtimeVisitor = runtimeVisitor.Create(this.context);
        }

        public void Load(Stream source)
        {
            this.context.Load(InstructionDecoder.DecodeStream(source));
        }

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
