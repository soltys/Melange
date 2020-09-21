using System;

namespace Soltys.VirtualMachine
{
    public class LoadStringInstruction : LoadInstruction, IInstruction
    {
        public string Value
        {
            get;
        }

        public LoadStringInstruction(string value) : base(LoadType.String)
        {
            Value = value;
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitLoadString(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Load, LoadType.String, Value);

        public override string ToString() => $"ldstr {Value}";
    }
}
