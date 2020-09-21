using System;

namespace Soltys.VirtualMachine
{
    public class BranchInstruction : IInstruction
    {
        public BranchType BranchType
        {
            get;
        }

        public int Target
        {
            get;
        }

        public BranchInstruction(BranchType branchType, int target)
        {
            BranchType = branchType;
            Target = target;
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitBranch(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Branch, BranchType, Target);
        public override string ToString() => $"br{ToString(BranchType)} {Target}";

        private string ToString(BranchType branchType) =>
            branchType switch {
                BranchType.Jump => "",
                BranchType.IfTrue => ".true",
                BranchType.IfFalse => ".false",
                _ => throw new ArgumentOutOfRangeException(nameof(branchType), branchType, null)
            };

        public static (BranchInstruction,int) Create(ReadOnlySpan<byte> span)
        {
            var branchType = (BranchType)span[0];
            var target = BitConverter.ToInt32(span.Slice(sizeof(BranchType)));

            var bytesRead = sizeof(BranchType) + sizeof(int);

            return (new BranchInstruction(branchType, target), bytesRead);
        }
    }
}
