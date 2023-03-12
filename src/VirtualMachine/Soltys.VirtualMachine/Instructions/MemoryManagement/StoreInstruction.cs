namespace Soltys.VirtualMachine;

public class StoreInstruction : IInstruction
{
    public StoreInstruction(StoreKind storeKind, byte index)
    {
        StoreKind = storeKind;
        Index = index;
    }

    public StoreKind StoreKind
    {
        get;
    }

    public byte Index
    {
        get;
    }

    public static (StoreInstruction, int) Create(in ReadOnlySpan<byte> span) =>
        (new StoreInstruction((StoreKind)span[0], span[1]), 2);

    public void Accept(IRuntimeVisitor visitor) => visitor.VisitStore(this);

    public ReadOnlySpan<byte> GetBytes() => InstructionEncoder.Encode(Opcode.Store, StoreKind, Index);

    public override string ToString() => $"st{ToString(StoreKind)}.{Index}";

    private string ToString(StoreKind storeKind) =>
        storeKind switch {
            StoreKind.Local => "loc",
            StoreKind.Argument => "arg",
            StoreKind.Field => "fld",
            StoreKind.StaticField => "sfld",
            _ => throw new ArgumentOutOfRangeException(nameof(storeKind), storeKind, null)
        };
}
