using System;
using System.Globalization;

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
                throw new ArgumentOutOfRangeException(nameof(loadType));
            }

            Value = value ?? throw new ArgumentNullException(nameof(value), "Load Constant value should not be null");
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitLoadConstant(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Load, LoadType, Value);

        public override string ToString() => $"ldc.{ToConstantLetter(LoadType)} {ToString(Value)}";

        private string ToString(object o) =>
            o switch {
                double d => d.ToString(CultureInfo.InvariantCulture),
                int i => i.ToString(CultureInfo.InvariantCulture),
                _ => o.ToString()
            };

        private string ToConstantLetter(LoadType loadType) =>
            loadType switch
            {
                LoadType.Integer => "i",
                LoadType.Double => "d",
                _ => throw new InvalidOperationException("LoadType should not be out of given range of values")
            };
    }
}
