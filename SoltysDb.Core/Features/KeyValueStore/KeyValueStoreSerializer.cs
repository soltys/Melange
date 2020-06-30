using System;
using System.Collections.Generic;
using System.Text;

namespace SoltysDb.Core
{
    internal static class KeyValueStoreSerializer
    {
        private class KeyValueEntry : BinaryClass
        {
            private readonly BinaryStringNVarField keyField;
            private readonly BinaryStringNVarField valueField;

            public string Key => this.keyField.GetValue();
            public string Value => this.valueField.GetValue();
            public int Size { get; private set; }

            public KeyValueEntry(byte[] dataSource, string key, string value) : base(dataSource)
            {
                this.keyField = AddStringNVarField(key.Length);
                this.valueField = AddStringNVarField(value.Length);

                this.keyField.SetValue(key);
                this.valueField.SetValue(value);

                Size = this.valueField.FieldEnd;
            }

            public KeyValueEntry(byte[] dataSource) : base(dataSource)
            {
                this.keyField = AddExistingStringNVarField();
                this.valueField = AddExistingStringNVarField();
                Size = this.valueField.FieldEnd;
            }
        }
        
        public static byte[] ToBytes(string key, string value)
        {
            int outputLength = key.Length * sizeof(char) + value.Length * sizeof(char) + sizeof(int) + sizeof(int);
            byte[] output = new byte[outputLength];
            var entry = new KeyValueEntry(output, key, value);
            return output;
        }

        public static KeyValuePair<string, string> ToKeyValuePair(Span<byte> data, out int entrySize)
        {
            var entry = new KeyValueEntry(data.ToArray());
            entrySize = entry.Size;
            return new KeyValuePair<string, string>(entry.Key, entry.Value);
        }

        public static byte[] ToBytes(Dictionary<string, string> dict)
        {
            byte[] dictCount = BitConverter.GetBytes(dict.Count);

            List<byte> bytes = new List<byte>();
            bytes.AddRange(dictCount);

            foreach (var keyValuePair in dict)
            {
                bytes.AddRange(ToBytes(keyValuePair.Key, keyValuePair.Value));
            }

            return bytes.ToArray();
        }

        public static Dictionary<string, string> ToDictionary(Span<byte> bytes)
        {
            var dictionaryLength = BitConverter.ToInt32(bytes);
            Dictionary<string, string> output = new Dictionary<string, string>();
            int byteOffset = sizeof(int); //dictionaryLength
            for (int i = 0; i < dictionaryLength; i++)
            {
                var pairSlice = bytes.Slice(byteOffset);
                var (key, value) = ToKeyValuePair(pairSlice, out var entryByteSize);
                byteOffset += entryByteSize;

                output.Add(key, value);
            }

            return output;

        }
    }
}
