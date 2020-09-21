using System;

namespace Soltys.VirtualMachine
{
    public class CompareInstruction : IInstruction
    {
        public CompareType CompareType
        {
            get;
        }

        public CompareInstruction(CompareType compareType)
        {
            CompareType = compareType;
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitCompare(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Compare, CompareType);

        public static (IInstruction, int) Create(in ReadOnlySpan<byte> span) =>
            (new CompareInstruction((CompareType)span[0]), 1);


        public override string ToString() => $"{ToString(CompareType)}";

        public string ToString(CompareType compareType) =>
            compareType switch {
                CompareType.Equals => "ceq",
                CompareType.GreaterThan => "cgt",
                CompareType.LessThan => "clt",
                _ => throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null)
            };
    }
}
