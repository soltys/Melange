using System;

namespace Soltys.VirtualMachine
{
    public class LoadPlaceInstruction : LoadInstruction, IInstruction
    {
        public byte Index
        {
            get;
        }

        public LoadPlaceInstruction(LoadType loadType, byte index) : base(loadType)
        {
            Index = index;
        }

        public override string ToString() => $"ld{ToString(LoadType)}.{Index}";

        private string ToString(LoadType loadType) =>
            loadType switch
            {
                LoadType.Argument => "arg",
                LoadType.Local => "loc",
                LoadType.StaticField => "sfld",
                _ => throw new NotImplementedException()
            };

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitLoadPlace(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Load, LoadType, Index);
    }
}
