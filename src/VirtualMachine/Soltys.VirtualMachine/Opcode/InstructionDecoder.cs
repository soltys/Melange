using System.Text;

namespace Soltys.VirtualMachine;

internal static class InstructionDecoder
{
    public static List<IInstruction> DecodeStream(Stream source)
    {
        var instructions = new List<IInstruction>();

        var bytes = ReadBytes(source);

        int readIndex = 0;

        while (readIndex < bytes.Length)
        {
            var (instruction, bytesRead) = Decode(bytes.Slice(readIndex));
            readIndex += bytesRead;

            instructions.Add(instruction);
        }

        return instructions;
    }

    private static Span<byte> ReadBytes(Stream source)
    {
        using var memoryStream = new MemoryStream();
        source.CopyTo(memoryStream);
        return memoryStream.ToArray().AsSpan();
    }

    public static (IInstruction, int) Decode(ReadOnlySpan<byte> span)
    {
        if (span.Length < sizeof(Opcode))
        {
            throw new ArgumentException("Memory size is to small for required SerializeOpcode to exist");
        }

        var opcode = OpcodeHelper.ToOpcode(span.Slice(0, sizeof(Opcode)));

        var (instruction, bytesRead) = OpcodeToInstruction(span.Slice(sizeof(Opcode)), opcode);
        return (instruction, bytesRead + sizeof(Opcode));
    }

    private static (IInstruction, int) OpcodeToInstruction(ReadOnlySpan<byte> span, Opcode opcode) =>
        opcode switch
        {
            //Memory management
            Opcode.Store => StoreInstruction.Create(span),
            Opcode.Load => LoadInstruction.Create(span),
            Opcode.List => ListInstruction.Create(span),

            //Math operation
            Opcode.Add => AddInstruction.Create(span),
            Opcode.Subtraction => SubtractionInstruction.Create(span),
            Opcode.Multiplication => MultiplicationInstruction.Create(span),
            Opcode.Division => DivisionInstruction.Create(span),

            //Boolean compare
            Opcode.Compare => CompareInstruction.Create(span),

            //Branching
            Opcode.Call => CallInstruction.Create(span),
            Opcode.Return => ReturnInstruction.Create(span),
            Opcode.Branch => BranchInstruction.Create(span),

            //Special
            Opcode.Nop => NopInstruction.Create(span),
            Opcode.Custom => throw new OpcodeDecodeException(),

            Opcode.Undefined => throw new OpcodeDecodeException(),
            _ => throw new OpcodeDecodeException()
        };

    public static (string, int) DecodeString(ReadOnlySpan<byte> span)
    {
        var lengthBytesRead = sizeof(int);
        var stringLength = BitConverter.ToInt32(span);
        var stringItself = Encoding.UTF8.GetString(span.Slice(lengthBytesRead, stringLength));

        var totalBytesRead = lengthBytesRead + stringItself.Length;
        return (stringItself, totalBytesRead);
    }
}
