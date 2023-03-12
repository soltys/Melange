namespace Soltys.VirtualMachine;

public abstract class ListInstruction : IInstruction
{
    public abstract ListOperationKind Operation
    {
        get;
    }

    public static (IInstruction, int) Create(in ReadOnlySpan<byte> span)
    {
        var loadType = OpcodeHelper.ToListOperationKind(span[0]);
        var bytesRead = 1;
        return loadType switch
        {
            ListOperationKind.New => (new ListNewInstruction(), bytesRead),
            ListOperationKind.Add => (new ListAddInstruction(), bytesRead),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void Accept(IRuntimeVisitor visitor) => visitor.VisitList(this);

    public ReadOnlySpan<byte> GetBytes() => InstructionEncoder.Encode(Opcode.List, Operation);
}
