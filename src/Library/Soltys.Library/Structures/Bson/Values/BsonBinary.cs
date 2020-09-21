using System;

namespace Soltys.Library.Bson
{
    internal class BsonBinary : BsonValue
    {
        public BinarySubType BinarySubType
        {
            get;
        }

        internal override ElementType Type => ElementType.Binary;

        public byte[] Bytes
        {
            get;
        }

        public BsonBinary(BinarySubType binarySubType, byte[] bytes)
        {
            BinarySubType = binarySubType;
            Bytes = bytes;
        }

        public override ReadOnlySpan<byte> GetBytes()
        {
            var serializedSize = sizeof(int) + sizeof(byte) + Bytes.Length;
            var output = new byte[serializedSize];
            Array.Copy(BitConverter.GetBytes(Bytes.Length), output, sizeof(int));
            output[sizeof(int)] = (byte)BinarySubType;
            Array.Copy(Bytes, 0, output, sizeof(int) + sizeof(byte), Bytes.Length);
            return output;
        }

        public override string ToString() => $"BinData({BinarySubType}, {ToString(Bytes)})";
        private static string ToString(byte[] bytes) => Convert.ToBase64String(bytes);
    }
}
