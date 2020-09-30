using System;

namespace Soltys.VirtualMachine
{
    public class LoadPlaceInstruction : LoadInstruction, IInstruction
    {
        public byte Index
        {
            get;
        }

        public LoadPlaceInstruction(LoadKind loadKind, byte index) : base(loadKind)
        {
            Index = index;
        }

        public override string ToString() => $"ld{ToString(LoadKind)}.{Index}";

        private string ToString(LoadKind loadKind) =>
            loadKind switch
            {
                LoadKind.Argument => "arg",
                LoadKind.Local => "loc",
                LoadKind.StaticField => "sfld",
                _ => throw new NotImplementedException()
            };

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitLoadPlace(this);

        public ReadOnlySpan<byte> GetBytes() => InstructionEncoder.Encode(Opcode.Load, LoadKind, Index);
    }
}
