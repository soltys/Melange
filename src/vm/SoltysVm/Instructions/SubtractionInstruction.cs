using System;

namespace SoltysVm
{
    public class SubtractionInstruction : IInstruction
    {
        public static (SubtractionInstruction, int) Create(in ReadOnlySpan<byte> span) =>
            (new SubtractionInstruction(), 0);

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitSubtraction(this);

        public ReadOnlySpan<byte> GetBytes() => Opcode.Subtraction.GetBytes();
        public override string ToString() => "sub";
    }
}
