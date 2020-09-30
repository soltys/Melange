using System;
using System.Collections.Generic;
using System.IO;

namespace Soltys.VirtualMachine.Test.TestUtils
{
    internal class InstructionByteBuilder
    {
        private readonly List<byte> output;

        public InstructionByteBuilder()
        {
            this.output = new List<byte>();
        }

        public InstructionByteBuilder Opcode(Opcode opcode, params object[] objects)
        {
            this.output.AddRange(InstructionEncoder.Encode(opcode, objects));
            return this;
        }

        public byte[] ToArray() => this.output.ToArray();
        public ReadOnlySpan<byte> AsSpan() => this.output.ToArray().AsSpan();
        public Stream AsStream() => new MemoryStream(ToArray());

        public static InstructionByteBuilder Create() => new InstructionByteBuilder();
    }
}
