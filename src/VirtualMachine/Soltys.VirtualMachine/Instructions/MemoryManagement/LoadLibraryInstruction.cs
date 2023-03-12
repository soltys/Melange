namespace Soltys.VirtualMachine;

public class LoadLibraryInstruction : LoadInstruction, IInstruction
{
    public LoadLibraryInstruction(string libraryName) : base(LoadKind.Library)
    {
        LibraryName = libraryName;
    }

    public string LibraryName
    {
        get;
    }

    public void Accept(IRuntimeVisitor visitor) =>
        visitor.VisitLoadLibrary(this);

    public ReadOnlySpan<byte> GetBytes() =>
        InstructionEncoder.Encode(Opcode.Load, LoadKind, LibraryName);

    public override string ToString() => $"ldl {LibraryName}";
}
