using System;

namespace SoltysVm
{
    public class LoadConstantInstruction : LoadInstruction, IInstruction
    {
        public object Value
        {
            get;
        }

        public LoadConstantInstruction(LoadType loadType, object value) : base(loadType)
        {
            if (loadType != LoadType.Integer && loadType != LoadType.Double)
            {
                throw  new ArgumentOutOfRangeException(nameof(loadType));
            }

            Value = value ?? throw new ArgumentNullException(nameof(value), "Load Constant value should not be null");
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitLoadConstant(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Load, LoadType, Value);

        public override string ToString() => $"ldc.{ToConstantLetter(LoadType)} {Value}";

        private string ToConstantLetter(LoadType loadType) =>
            loadType switch {
                LoadType.Integer => "i",
                LoadType.Double => "d",
                _ => throw new InvalidOperationException("LoadType should not be out of given range of values")
            };
    }
}
