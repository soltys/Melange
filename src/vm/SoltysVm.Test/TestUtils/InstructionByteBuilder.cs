using System;
using System.Collections.Generic;
using System.IO;

namespace SoltysVm.Test.TestUtils
{
    internal class InstructionByteBuilder
    {
        private List<byte> output;

        public InstructionByteBuilder()
        {
            this.output = new List<byte>();
        }

        public InstructionByteBuilder Opcode(Opcode opcode, params object[] objects)
        {
            this.output.AddRange(OpcodeHelper.SerializeOpcode(opcode, objects));
            return this;
        }

        public byte[] ToArray() => this.output.ToArray();
        public ReadOnlySpan<byte> AsSpan() => this.output.ToArray().AsSpan();
        public Stream AsStream() => new MemoryStream(ToArray());

        public static InstructionByteBuilder Create() => new InstructionByteBuilder();
    }
}
