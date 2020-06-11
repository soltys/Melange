using System.Collections.Generic;
using Xunit;

namespace SoltysDb.Core.Test.Features
{
    public class KeyValueStoreSerializerTests
    {
        [Fact]
        public void SerializeToBytesAndDeserialize()
        {
            var bytes = KeyValueStoreSerializer.ConvertKeyValueStringPairToBytes("myKey", "myValue");

            var pair = KeyValueStoreSerializer.GetKeyValuePairFromBytes(bytes);

            Assert.Equal("myKey",pair.Key);
            Assert.Equal("myValue",pair.Value);
        }

        [Fact]
        public void Serialize_Dictionary_string_string()
        {
            var dict = new Dictionary<string, string>
            {
                {"foo", "bar"},
                {"me", "you"}
            };
            var bytes=  KeyValueStoreSerializer.CovertDictionaryToBytes(dict);

            var dictFromBytes = KeyValueStoreSerializer.GetDictionaryFromBytes(bytes);

            Assert.Equal("bar", dictFromBytes["foo"]);
            Assert.Equal("you", dictFromBytes["me"]);
        }
    }
}
