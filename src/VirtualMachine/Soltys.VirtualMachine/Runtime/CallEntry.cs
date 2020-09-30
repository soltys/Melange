namespace Soltys.VirtualMachine
{
    internal class CallEntry
    {
        public CallEntry(string methodName, int instructionPointer)
        {
            MethodName = methodName;
            InstructionPointer = instructionPointer;
        }

        public int InstructionPointer
        {
            get;
            set;
        }

        public string MethodName
        {
            get;
        }
    }
}
