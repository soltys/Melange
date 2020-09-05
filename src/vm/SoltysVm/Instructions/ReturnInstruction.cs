using System;

namespace SoltysVm
{
    public class ReturnInstruction : IInstruction
    {
        public static (ReturnInstruction, int) Create(ReadOnlySpan<byte> span) => (new ReturnInstruction(), 0);
        public void Accept(IRuntimeVisitor visitor) => visitor.VisitReturn(this);

        public ReadOnlySpan<byte> GetBytes() => Opcode.Return.GetBytes();
        public override string ToString() => "ret";
    }
}
