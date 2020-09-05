using System.Collections.Generic;
using Xunit;

namespace SoltysDb.Test.Features
{
    public class KeyValueStoreSerializerTests
    {
        [Fact]
        public void SerializeToBytesAndDeserialize()
        {
            var bytes = KeyValueStoreSerializer.ToBytes("myKey", "myValue");
            int entrySize;
            var pair = KeyValueStoreSerializer.ToKeyValuePair(bytes, out entrySize);

            Assert.Equal("myKey", pair.Key);
            Assert.Equal("myValue", pair.Value);
            //Length of the string times - sizeof(char) == 2
            //plus 2 times - sizeof(int) == 4 - for string length
            int expectedEntrySize = 32;
            Assert.Equal(expectedEntrySize, entrySize);
        }

        [Fact]
        public void Serialize_Dictionary_string_string()
        {
            var dict = new Dictionary<string, string>
            {
                {"foo", "bar"},
                {"me", "you"}
            };
            var bytes = KeyValueStoreSerializer.ToBytes(dict);

            var dictFromBytes = KeyValueStoreSerializer.ToDictionary(bytes);

            Assert.Equal("bar", dictFromBytes["foo"]);
            Assert.Equal("you", dictFromBytes["me"]);
        }
    }
}
