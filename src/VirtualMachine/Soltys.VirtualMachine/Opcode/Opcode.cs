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
        Compare, //Compare [CompareType: Enum]

        //Branching
        Return,
        Call, // Call [MethodName: String]
        Branch, //Branch [BranchType: Enum] [Target: Int]

        //Special
        Nop,
        Custom
    }
}
