namespace Soltys.VirtualMachine
{
    public enum Opcode : byte
    {
        Undefined,

        //Memory Management
        Load, 
        Store,

        //Math operations
        Add,
        Subtraction,
        Multiplication,
        Division,

        //Boolean compare
        Compare, //Compare [CompareKind: Enum]

        //Branching
        Return,
        Call, //Call [MethodName: String]
        Branch, //Branch [BranchKind: Enum] [Target: Int]

        //Special
        Nop,
        Custom
    }
}
