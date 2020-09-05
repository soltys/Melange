using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoltysVm
{
    public static class OpcodeHelper
    {
        public static byte[] GetBytes(this Opcode opcode) => new[] { (byte)opcode };
        public static Opcode ToOpcode(ReadOnlySpan<byte> span) => (Opcode)span[0];

        public static LoadType ToLoadType(byte value) => (LoadType)value;

        public static byte[] SerializeOpcode(Opcode opcode, params object[] objects)
        {
            var output = new List<byte>();
            output.AddRange(
                opcode.GetBytes()
            );

            foreach (var o in objects)
            {
                output.AddRange(ConvertObjectToBytes(o));
            }
            return output.ToArray();
        }

        private static byte[] ConvertObjectToBytes(object o)
        {
            if (o.GetType().IsEnum)
            {
                return new[] { Convert.ToByte(o) };
            }

            return o switch
            {
                byte b => new[] { b },
                int i => BitConverter.GetBytes(i),
                double d => BitConverter.GetBytes(d),
                float f => BitConverter.GetBytes(f),
                string s => BitConverter.GetBytes(s.Length).AsEnumerable()
                    .Concat(Encoding.UTF8.GetBytes(s)).ToArray(),
                _ => throw new ArgumentException("Type is not recognized", "objects")
            };
        }

        public static (string, int) DecodeString(ReadOnlySpan<byte> span)
        {
            var lengthBytesRead = sizeof(int);
            var stringLength = BitConverter.ToInt32(span);
            var stringItself = Encoding.UTF8.GetString(span.Slice(lengthBytesRead, stringLength));

            var totalBytesRead = lengthBytesRead + stringItself.Length;
            return (stringItself, totalBytesRead);
        }
    }
}
