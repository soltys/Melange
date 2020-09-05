using System.Collections.Generic;
using System.IO;
using SoltysVm.Test.TestUtils;
using Xunit;

namespace SoltysVm.Test
{
    public class AssemblerTests
    {
        [Fact]
        public void Assemble_ToExpectedFormat()
        {
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadType.Integer, 42)
                .Opcode(Opcode.Load, LoadType.Integer, 69)
                .Opcode(Opcode.Add)
                .Opcode(Opcode.Return)
                .ToArray();

            var instructions = new List<IInstruction> {
                new LoadConstantInstruction(LoadType.Integer, 42),
                new LoadConstantInstruction(LoadType.Integer, 69),
                new AddInstruction(),
                new ReturnInstruction()
            };

            var assembler = new Assembler();
            using var outputStream = new MemoryStream();
            assembler.Assemble(outputStream, instructions);

            Assert.Equal(expectedBytes, outputStream.ToArray());
        }

    }
}
