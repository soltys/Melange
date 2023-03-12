namespace Soltys.VirtualMachine;

public class ListAddInstruction : ListInstruction
{
    public override ListOperationKind Operation
        => ListOperationKind.Add;

    public override string ToString() => "list.add";
}