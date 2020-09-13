using SoltysLib.Bson;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class BsonValuesToStringTests
    {
        [Fact]
        public void ToString_BsonString() => AssertToString("\"foo\"", new BsonString("foo"));
        [Fact]
        public void ToString_BsonInteger() => AssertToString("42", new BsonInteger(42));
        [Fact]
        public void ToString_BsonDouble() => AssertToString("0.69", new BsonDouble(0.69));
        [Fact]
        public void ToString_BsonLongInteger() => AssertToString("42", new BsonLongInteger(42L));
        [Fact]
        public void ToString_BsonBooleanTrue() => AssertToString("true", new BsonBoolean(true));
        [Fact]
        public void ToString_BsonBooleanFalse() => AssertToString("false", new BsonBoolean(false));
        [Fact]
        public void ToString_BsonNull() => AssertToString("null", BsonNull.Value);
        [Fact]
        public void ToString_Array() => AssertToString("[1, 2, 3]", new BsonArray(new BsonInteger(1), new BsonInteger(2), new BsonInteger(3)));
        [Fact]
        public void ToString_DateTime() => AssertToString("Date(1599760732000)", new BsonDatetime(1599760732000L));

        [Theory]
        [InlineData(BinarySubType.Binary, "BinData(Binary, AQID)")]
        [InlineData(BinarySubType.Function, "BinData(Function, AQID)")]
        [InlineData(BinarySubType.MD5, "BinData(MD5, AQID)")]
        [InlineData(BinarySubType.Encrypted, "BinData(Encrypted, AQID)")]
        [InlineData(BinarySubType.UserDefined, "BinData(UserDefined, AQID)")]
        public void ToString_BinaryData(BinarySubType binarySubType, string expectedToString) =>
            AssertToString(expectedToString, new BsonBinary(binarySubType, new byte[] { 1, 2, 3 }));

        private static void AssertToString(string expectedToString, BsonValue bsonValue)
        {
            Assert.Equal(expectedToString, bsonValue.ToString());
        }
    }
}
