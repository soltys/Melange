using System;

namespace SoltysVm
{
    public class NopInstruction : IInstruction
    {
        public static (NopInstruction, int) Create(ReadOnlySpan<byte> span) => (new NopInstruction(), 0);
        public void Accept(IRuntimeVisitor visitor) => visitor.VisitNop(this);

        public ReadOnlySpan<byte> GetBytes() => Opcode.Nop.GetBytes();

        public override string ToString() => "nop";
    }
}
