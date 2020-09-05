using System;

namespace SoltysVm
{
    public class AddInstruction : IInstruction
    {
        public static (AddInstruction, int) Create(in ReadOnlySpan<byte> span) =>
            (new AddInstruction(), 0);

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitAdd(this);

        public ReadOnlySpan<byte> GetBytes() => Opcode.Add.GetBytes();

        public override string ToString() => "add";
    }
}
