namespace Soltys.VirtualMachine;

public class DivisionInstruction : IInstruction
{
    public static (DivisionInstruction, int) Create(in ReadOnlySpan<byte> span) =>
        (new DivisionInstruction(), 0);

    public void Accept(IRuntimeVisitor visitor) => visitor.VisitDivision(this);

    public ReadOnlySpan<byte> GetBytes() => Opcode.Division.GetBytes();

    public override string ToString() => "div";
}
