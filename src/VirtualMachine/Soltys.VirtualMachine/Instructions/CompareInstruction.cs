using System;

namespace Soltys.VirtualMachine
{
    public class CompareInstruction : IInstruction
    {
        public CompareKind CompareKind
        {
            get;
        }

        public CompareInstruction(CompareKind compareKind)
        {
            CompareKind = compareKind;
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitCompare(this);

        public ReadOnlySpan<byte> GetBytes() => InstructionEncoder.Encode(Opcode.Compare, CompareKind);

        public static (IInstruction, int) Create(in ReadOnlySpan<byte> span) =>
            (new CompareInstruction((CompareKind)span[0]), 1);


        public override string ToString() => $"{ToString(CompareKind)}";

        public string ToString(CompareKind compareKind) =>
            compareKind switch {
                CompareKind.Equals => "ceq",
                CompareKind.GreaterThan => "cgt",
                CompareKind.LessThan => "clt",
                _ => throw new ArgumentOutOfRangeException(nameof(compareKind), compareKind, null)
            };
    }
}
