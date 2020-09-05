using System;

namespace SoltysVm
{
    public class StoreInstruction : IInstruction
    {
        public StoreInstruction(StoreType storeType, byte index)
        {
            StoreType = storeType;
            Index = index;
        }

        public StoreType StoreType
        {
            get;
        }

        public byte Index
        {
            get;
        }

        public static (StoreInstruction, int) Create(in ReadOnlySpan<byte> span) =>
            (new StoreInstruction((StoreType)span[0], span[1]), 2);

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitStore(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Store, StoreType, Index);

        public override string ToString() => $"st{ToString(StoreType)}.{Index}";

        private string ToString(StoreType storeType) =>
            storeType switch {
                StoreType.Local => "loc",
                StoreType.Argument => "arg",
                StoreType.Field => "fld",
                StoreType.StaticField => "sfld",
                _ => throw new ArgumentOutOfRangeException(nameof(storeType), storeType, null)
            };
    }
}
