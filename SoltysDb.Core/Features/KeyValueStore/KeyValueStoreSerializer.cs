using System;
using System.Collections.Generic;
using System.Text;

namespace SoltysDb.Core
{
    internal static class KeyValueStoreSerializer
    {
        public static byte[] ConvertKeyValueStringPairToBytes(string key, string value)
        {
            byte[] keyLength = BitConverter.GetBytes(key.Length);
            byte[] valueLength = BitConverter.GetBytes(value.Length);
            byte[] keyString = Encoding.Default.GetBytes(key);
            byte[] valueString = Encoding.Default.GetBytes(value);

            int outputLength = keyLength.Length + valueLength.Length + keyString.Length + valueString.Length;

            byte[] output = new byte[outputLength];

            Buffer.BlockCopy(keyLength, 0, output, 0, keyLength.Length);
            Buffer.BlockCopy(valueLength, 0, output, keyLength.Length, valueLength.Length);
            Buffer.BlockCopy(keyString, 0, output, keyLength.Length + valueLength.Length, keyString.Length);
            Buffer.BlockCopy(valueString, 0, output, keyLength.Length + valueLength.Length + keyString.Length, valueString.Length);

            return output;
        }

        public static KeyValuePair<string, string> GetKeyValuePairFromBytes(Span<byte> data)
        {
            int keyLength = BitConverter.ToInt32(data[0..4]);
            int valueLength = BitConverter.ToInt32(data[4..8]);

            const int stringsLengthMetadata = 8;

            var key = Encoding.Default.GetString(data.Slice(stringsLengthMetadata, keyLength));
            var value = Encoding.Default.GetString(data.Slice(stringsLengthMetadata + keyLength, valueLength));

            return new KeyValuePair<string, string>(key, value);
        }


        public static byte[] CovertDictionaryToBytes(Dictionary<string, string> dict)
        {
            byte[] dictCount = BitConverter.GetBytes(dict.Count);

            List<byte> bytes = new List<byte>();
            bytes.AddRange(dictCount);

            foreach (var keyValuePair in dict)
            {
                bytes.AddRange(ConvertKeyValueStringPairToBytes(keyValuePair.Key, keyValuePair.Value));
            }

            return bytes.ToArray();
        }


        public static Dictionary<string, string> GetDictionaryFromBytes(Span<byte> bytes)
        {
            const int stringsLengthMetadata = 8;

            var dictionaryLength = BitConverter.ToInt32(bytes[0..4]);

            Dictionary<string, string> output = new Dictionary<string, string>();
            int byteOffset = 4; //dictionaryLength
            for (int i = 0; i < dictionaryLength; i++)
            {
                var pairSlice = bytes.Slice(byteOffset);
                var pair = GetKeyValuePairFromBytes(pairSlice);
                byteOffset +=
                    pair.Key.Length + pair.Value.Length +
                    stringsLengthMetadata; // length of metadata in each pair; 4 bytes for length of key and value

                output.Add(pair.Key, pair.Value);
            }

            return output;

        }
    }
}
