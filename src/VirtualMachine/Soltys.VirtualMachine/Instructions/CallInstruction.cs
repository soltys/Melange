using System;

namespace Soltys.VirtualMachine
{
    public class CallInstruction : IInstruction
    {
        public string MethodCall
        {
            get;
        }

        public CallInstruction(string methodCall)
        {
            MethodCall = methodCall;
        }

        public void Accept(IRuntimeVisitor visitor) => visitor.VisitCall(this);

        public ReadOnlySpan<byte> GetBytes() => OpcodeHelper.SerializeOpcode(Opcode.Call, MethodCall);


        public static (IInstruction, int) Create(in ReadOnlySpan<byte> span)
        {
            var (stringItself, totalBytesRead) = OpcodeHelper.DecodeString(span);
            return (new CallInstruction(stringItself), totalBytesRead);
        }

        public override string ToString() => $"call {MethodCall}";
    }
}
