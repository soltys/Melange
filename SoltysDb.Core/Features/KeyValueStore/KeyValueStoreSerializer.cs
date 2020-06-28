using System;
using System.Collections.Generic;
using System.Text;

namespace SoltysDb.Core
{
    internal static class KeyValueStoreSerializer
    {
        private class KeyValueEntry : BinaryClass
        {
            private BinaryStringNVarField keyField;
            private BinaryStringNVarField valueField;

            public string Key => keyField.GetValue();
            public string Value => valueField.GetValue();
            public int Size { get; private set; }

            public KeyValueEntry(byte[] dataSource, string key, string value) : base(dataSource)
            {
                this.keyField = AddStringNVarField(key.Length);
                this.valueField = AddStringNVarField(value.Length);

                keyField.SetValue(key);
                valueField.SetValue(value);

                this.Size = valueField.FieldEnd;
            }

            public KeyValueEntry(byte[] dataSource) : base(dataSource)
            {
                this.keyField = AddStringNVarField();
                this.valueField = AddStringNVarField();
                this.Size = valueField.FieldEnd;
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
            int byteOffset = 4; //dictionaryLength
            for (int i = 0; i < dictionaryLength; i++)
            {
                var pairSlice = bytes.Slice(byteOffset);
                int entryByteSize;
                var pair = ToKeyValuePair(pairSlice, out entryByteSize);
                byteOffset += entryByteSize;

                output.Add(pair.Key, pair.Value);
            }

            return output;

        }
    }
}
