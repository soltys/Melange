namespace Soltys.VirtualMachine;

public class MultiplicationInstruction : IInstruction
{
    public static (MultiplicationInstruction, int) Create(in ReadOnlySpan<byte> span) =>
        (new MultiplicationInstruction(), 0);

    public void Accept(IRuntimeVisitor visitor) => visitor.VisitMultiplication(this);

    public ReadOnlySpan<byte> GetBytes() => Opcode.Multiplication.GetBytes();

    public override string ToString() => "mul";
}
