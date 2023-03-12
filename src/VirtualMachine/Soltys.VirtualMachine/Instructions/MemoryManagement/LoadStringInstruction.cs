namespace Soltys.VirtualMachine;

public class LoadStringInstruction : LoadInstruction, IInstruction
{
    public string Value
    {
        get;
    }

    public LoadStringInstruction(string value) : base(LoadKind.String)
    {
        Value = value;
    }

    public void Accept(IRuntimeVisitor visitor) => visitor.VisitLoadString(this);

    public ReadOnlySpan<byte> GetBytes() => InstructionEncoder.Encode(Opcode.Load, LoadKind.String, Value);

    public override string ToString() => $"ldstr {Value}";
}
