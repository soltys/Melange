using System;

namespace Soltys.VirtualMachine
{
    public class BranchInstruction : IInstruction
    {
        public BranchKind BranchKind
        {
            get;
        }

        public int Target
        {
            get;
        }

        public BranchInstruction(BranchKind branchKind, int target)
        {
            BranchKind = branchKind;
            Target = target;
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitBranch(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Branch, BranchKind, Target);
        public override string ToString() => $"br{ToString(BranchKind)} {Target}";

        private string ToString(BranchKind branchKind) =>
            branchKind switch {
                BranchKind.Jump => "",
                BranchKind.IfTrue => ".true",
                BranchKind.IfFalse => ".false",
                _ => throw new ArgumentOutOfRangeException(nameof(branchKind), branchKind, null)
            };

        public static (BranchInstruction,int) Create(ReadOnlySpan<byte> span)
        {
            var branchType = (BranchKind)span[0];
            var target = BitConverter.ToInt32(span.Slice(sizeof(BranchKind)));

            var bytesRead = sizeof(BranchKind) + sizeof(int);

            return (new BranchInstruction(branchType, target), bytesRead);
        }
    }
}
