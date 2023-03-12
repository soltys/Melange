namespace Soltys.VirtualMachine;

public class CallInstruction : IInstruction
{
    public string MethodName
    {
        get;
    }

    public CallInstruction(string methodName)
    {
        MethodName = methodName;
    }

    public void Accept(IRuntimeVisitor visitor) => visitor.VisitCall(this);

    public ReadOnlySpan<byte> GetBytes() => InstructionEncoder.Encode(Opcode.Call, MethodName);


    public static (IInstruction, int) Create(in ReadOnlySpan<byte> span)
    {
        var (stringItself, totalBytesRead) = InstructionDecoder.DecodeString(span);
        return (new CallInstruction(stringItself), totalBytesRead);
    }

    public override string ToString() => $"call {MethodName}";
}
