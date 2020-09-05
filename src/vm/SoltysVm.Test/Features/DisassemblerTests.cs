using System.Linq;
using SoltysVm.Test.TestUtils;
using Xunit;

namespace SoltysVm.Test
{
    public class DisassemblerTests
    {
        [Fact]
        public void Disassemble()
        {
            var bytesStream = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadType.Integer, 42)
                .Opcode(Opcode.Load, LoadType.Integer, 69)
                .Opcode(Opcode.Add)
                .Opcode(Opcode.Return)
                .AsStream();

            var disassembler = new Disassembler();
            var disassembled = disassembler.Disassemble(bytesStream).ToArray();

            Assert.Equal(4, disassembled.Length);
            Assert.Equal("add", disassembled[2]);
            Assert.Equal("ret", disassembled[3]);
        }
    }
}
