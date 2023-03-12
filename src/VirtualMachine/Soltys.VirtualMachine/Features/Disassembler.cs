namespace Soltys.VirtualMachine;

internal class Disassembler
{
    public IEnumerable<string> Disassemble(Stream source) => 
        InstructionDecoder.DecodeStream(source).Select(x => x.ToString());
}
