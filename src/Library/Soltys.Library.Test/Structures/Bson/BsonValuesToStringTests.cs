using Soltys.Library.Bson;
using Xunit;

namespace Soltys.Library.Test.Bson;

public class BsonValuesToStringTests
{
        
    [Fact]
    public void ToString_BsonLongInteger() => AssertToString("42", new BsonLongInteger(42L));
    [Fact]
    public void ToString_BsonNull() => AssertToString("null", BsonNull.Value);

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