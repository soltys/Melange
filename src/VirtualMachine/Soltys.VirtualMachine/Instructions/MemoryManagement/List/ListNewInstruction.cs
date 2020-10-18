namespace Soltys.VirtualMachine
{
    public class ListNewInstruction : ListInstruction
    {
        public override ListOperationKind Operation
            => ListOperationKind.New;

        public override string ToString() => "list.new";
    }
}
