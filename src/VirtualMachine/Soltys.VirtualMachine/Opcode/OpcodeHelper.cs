using System;

namespace Soltys.VirtualMachine
{
    public static class OpcodeHelper
    {
        public static byte[] GetBytes(this Opcode opcode) => new[] { (byte)opcode };
        public static Opcode ToOpcode(ReadOnlySpan<byte> span) => (Opcode)span[0];

        public static LoadKind ToLoadType(byte value) => (LoadKind)value;
    }
}
