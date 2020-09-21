using System.Collections.Generic;

namespace Soltys.VirtualMachine
{
    internal class VMContext : IVMContext
    {
        private readonly List<IInstruction> instructions;

        private int instructionPointer;

        public Stack<object> ValueStack
        {
            get;
        }

        public VMContext()
        {
            this.instructions = new List<IInstruction>();
            ValueStack = new Stack<object>();
        }

        public void Reset()
        {
            this.instructionPointer = 0;
            ValueStack.Clear();
        }

        public void Load(IEnumerable<IInstruction> decodedInstructions)
        {
            this.instructions.Clear();
            this.instructions.AddRange(decodedInstructions);
        }

        public IInstruction GetCurrentInstruction() => this.instructions[this.instructionPointer];
        public bool IsHalted() => this.instructionPointer < this.instructions.Count;
        public void AdvanceInstructionPointer() => this.instructionPointer++;
    }
}
