using System;
using System.Globalization;

namespace Soltys.VirtualMachine
{
    public class LoadConstantInstruction : LoadInstruction, IInstruction
    {
        public object Value
        {
            get;
        }

        public LoadConstantInstruction(LoadKind loadKind, object value) : base(loadKind)
        {
            if (loadKind != LoadKind.Integer && loadKind != LoadKind.Double)
            {
                throw new ArgumentOutOfRangeException(nameof(loadKind));
            }

            Value = value ?? throw new ArgumentNullException(nameof(value), "Load Constant value should not be null");
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitLoadConstant(this);

        public ReadOnlySpan<byte> GetBytes() => InstructionEncoder.Encode(Opcode.Load, LoadKind, Value);

        public override string ToString() => $"ldc.{ToConstantLetter(LoadKind)} {ToString(Value)}";

        private string ToString(object o) =>
            o switch 
            {
                double d => d.ToString(CultureInfo.InvariantCulture),
                int i => i.ToString(CultureInfo.InvariantCulture),
                _ => o.ToString()
            };

        private string ToConstantLetter(LoadKind loadKind) =>
            loadKind switch
            {
                LoadKind.Integer => "i",
                LoadKind.Double => "d",
                _ => throw new InvalidOperationException("LoadKind should not be out of given range of values")
            };
    }
}
