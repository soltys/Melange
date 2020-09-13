using System;
using SoltysLib.Bson;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class BsonSerializerTests
    {

        [Fact]
        public void Serialize_EmptyDocument()
        {
            var bsonDocument = new BsonDocument();
            var bytes = BsonSerializer.Serialize(bsonDocument);
            var base64String = Convert.ToBase64String(bytes);
            Assert.Equal("BQAAAAA=", base64String);
        }

        [Fact]
        public void Serialize_DoubleElement() =>
            //{ "foo" : 42.689999999999998 }
            AssertSerialization("EgAAAAFmb28AuB6F61FYRUAA", new BsonDouble(42.69));

        [Fact]
        public void Serialize_Integer32Element() =>
            // { "foo" : 42 }
            AssertSerialization("DgAAABBmb28AKgAAAAA=", new BsonInteger(42));

        [Fact]
        public void Serialize_Integer64Element() =>
            //{ "foo" : NumberLong(42) }
            AssertSerialization("EgAAABJmb28AKgAAAAAAAAAA", new BsonLongInteger(42L));

        [Theory]
        [InlineData(true, "CwAAAAhmb28AAQA=")]
        [InlineData(false, "CwAAAAhmb28AAAA=")]
        public void Serialize_BooleanElement(bool inputBoolean, string expectedBase64) =>
            //{ "foo" : [true | false] }
            AssertSerialization(expectedBase64, new BsonBoolean(inputBoolean));

        [Fact]
        public void Serialize_StringElement() =>
            //{ "foo" : "bar" }
            AssertSerialization("EgAAAAJmb28ABAAAAGJhcgAA", new BsonString("bar"));

        [Fact]
        public void Serialize_NullValue() =>
            // { "foo" : null }
            AssertSerialization("CgAAAApmb28AAA==", BsonNull.Value);

        [Fact]
        public void Serialize_ArrayValue() =>
            // { "foo" : ["one", "two", "three", "four", "five"] }
            AssertSerialization(
                "SgAAAARmb28AQAAAAAIwAAQAAABvbmUAAjEABAAAAHR3bwACMgAGAAAAdGhyZWUAAjMABQAAAGZvdXIAAjQABQAAAGZpdmUAAAA=",
                new BsonArray(new BsonString("one"), new BsonString("two"), new BsonString("three"), new BsonString("four"), new BsonString("five"))
            );

        [Fact]
        public void Serialize_DateTimeValue() =>
            // { "foo" : ISODate("2020-09-09T19:14:15.099Z") }
            AssertSerialization("EgAAAAlmb28AuzdKdHQBAAAA", 
                new BsonDatetime(DateTimeOffset.Parse("2020-09-09T19:14:15.099Z").ToUnixTimeMilliseconds()));

        [Fact]
        public void Serialize_BsonDocument() =>
            // { "foo" : { "bar" : "dog" } }
            AssertSerialization("HAAAAANmb28AEgAAAAJiYXIABAAAAGRvZwAAAA==", 
                new BsonDocument(new Element("bar", new BsonString("dog"))));

        [Theory]
        [InlineData(BinarySubType.Binary, "EgAAAAVmb28AAwAAAAABAgMA")]
        [InlineData(BinarySubType.Function, "EgAAAAVmb28AAwAAAAEBAgMA")]
        [InlineData(BinarySubType.MD5, "EgAAAAVmb28AAwAAAAUBAgMA")]
        [InlineData(BinarySubType.Encrypted, "EgAAAAVmb28AAwAAAAYBAgMA")]
        [InlineData(BinarySubType.UserDefined, "EgAAAAVmb28AAwAAAIABAgMA")]
        public void Serialize_BinaryDocument(BinarySubType binarySubType, string expectedSerializationOutput) =>
            AssertSerialization(expectedSerializationOutput, new BsonBinary(binarySubType, new byte[] { 1, 2, 3 }));

        private static void AssertSerialization(string expectedBase64, BsonValue bsonValue)
        {
            var bsonDocument = new BsonDocument();
            bsonDocument.Add(new Element("foo", bsonValue));
            var bytes = BsonSerializer.Serialize(bsonDocument);
            var base64String = Convert.ToBase64String(bytes);
            Assert.Equal(expectedBase64, base64String);
        }
    }
}
