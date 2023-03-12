using Soltys.Library.Bson;
using Xunit;

namespace Soltys.Library.Test.Bson;

public class BsonDeserializerTests
{
    [Fact]
    public void Deserialize_EmptyDocument()
    {
        // { }
        var bytes = Convert.FromBase64String("BQAAAAA=");
        var doc = BsonSerializer.Deserialize(bytes);
        Assert.Equal(0, doc.Elements.Count);
    }

    [Fact]
    public void Deserialize_DoubleElement()
    {
        //{ "foo" : 42.689999999999998 }
        var bsonValue = AssertDecodedElement<BsonDouble>("EgAAAAFmb28AuB6F61FYRUAA");
        Assert.Equal(42.69, bsonValue.Value, 10);
    }

    [Fact]
    public void Deserialize_Integer32Element()
    {
        // { "foo" : 42 }
        var bsonValue = AssertDecodedElement<BsonInteger>("DgAAABBmb28AKgAAAAA=");
        Assert.Equal(42, bsonValue.Value);
    }

    [Fact]
    public void Deserialize_Integer64Element()
    {
        //{ "foo" : NumberLong(42) }
        var bsonValue = AssertDecodedElement<BsonLongInteger>("EgAAABJmb28AKgAAAAAAAAAA");
        Assert.Equal(42L, bsonValue.Value);
    }

    [Theory]
    [InlineData("CwAAAAhmb28AAQA=", true)]
    [InlineData("CwAAAAhmb28AAAA=", false)]
    public void Deserialize_BooleanElement(string bsonBase64, bool expectedValue)
    {
        //{ "foo" : true }
        var bsonValue = AssertDecodedElement<BsonBoolean>(bsonBase64);
        Assert.Equal(expectedValue, bsonValue.Value);
    }

    [Fact]
    public void Deserialize_StringElement()
    {
        //{ "foo" : "bar" }
        var bsonValue = AssertDecodedElement<BsonString>("EgAAAAJmb28ABAAAAGJhcgAA");
        Assert.Equal("bar", bsonValue.Value);
    }

    [Fact]
    public void Deserialize_BsonDocument()
    {
        // { "foo" : { "bar" : "dog" } }
        var bsonValue = AssertDecodedElement<BsonDocument>("HAAAAANmb28AEgAAAAJiYXIABAAAAGRvZwAAAA==");
        Assert.Equal(1, bsonValue.Elements.Count);
        var element = bsonValue.Elements.First();
        Assert.Equal("bar", element.Name);

        Assert.IsType<BsonString>(element.Value);
        var subString = (BsonString)element.Value;
        Assert.Equal("dog", subString.Value);
    }

    [Fact]
    public void Deserialize_ArrayElement()
    {
        // { "foo" : ["one", "two", "three", "four", "five"] }
        var bsonValue = AssertDecodedElement<BsonArray>("SgAAAARmb28AQAAAAAIwAAQAAABvbmUAAjEABAAAAHR3bwACMgAGAAAAdGhyZWUAAjMABQAAAGZvdXIAAjQABQAAAGZpdmUAAAA=");
        Assert.Equal(5, bsonValue.Values.Count);
        AssertElementValueAt(0, "one");
        AssertElementValueAt(1, "two");
        AssertElementValueAt(2, "three");
        AssertElementValueAt(3, "four");
        AssertElementValueAt(4, "five");

        void AssertElementValueAt(int index, string expectedValue)
        {
            var value = bsonValue.Values.ElementAt(index);
            Assert.IsType<BsonString>(value);
            var stringValue = (BsonString)value;
            Assert.Equal(expectedValue, stringValue.Value);
        }
    }

    [Fact]
    public void Deserialize_NullValue()
    {
        // { "foo" : null }
        var bsonValue = AssertDecodedElement<BsonNull>("CgAAAApmb28AAA==");
        Assert.Equal(BsonNull.Value, bsonValue);
    }

    [Fact]
    public void Deserialize_DateTimeValue()
    {
        // { "foo" : ISODate("2020-09-09T19:14:15.099Z") }
        var bsonValue = AssertDecodedElement<BsonDatetime>("EgAAAAlmb28AuzdKdHQBAAAA");

        var expectedDate = DateTime.Parse("2020-09-09T19:14:15.099Z").ToUniversalTime();
        Assert.Equal(expectedDate, bsonValue.Value);
    }

    [Theory]
    [InlineData("EgAAAAVmb28AAwAAAAABAgMA", BinarySubType.Binary)]
    [InlineData("EgAAAAVmb28AAwAAAAEBAgMA", BinarySubType.Function)]
    [InlineData("EgAAAAVmb28AAwAAAAUBAgMA", BinarySubType.MD5)]
    [InlineData("EgAAAAVmb28AAwAAAAYBAgMA", BinarySubType.Encrypted)]
    [InlineData("EgAAAAVmb28AAwAAAIABAgMA", BinarySubType.UserDefined)]
    public void Deserialize_BinaryData(string base64Input, BinarySubType binarySubType)
    {
        // { "foo" : new BinData(0, "AQID") }
        var bsonValue = AssertDecodedElement<BsonBinary>(base64Input);
        Assert.Equal(binarySubType, bsonValue.BinarySubType);
        Assert.Equal(new byte[] { 1, 2, 3 }, bsonValue.Bytes);
    }

    private static TValue AssertDecodedElement<TValue>(string base64String) where TValue : BsonValue
    {
        var bytes = Convert.FromBase64String(base64String);
        var doc = BsonSerializer.Deserialize(bytes);

        Assert.Equal(1, doc.Elements.Count);
        var element = doc.Elements.First();

        Assert.Equal("foo", element.Name);

        Assert.IsType<TValue>(element.Value);
        return (TValue)element.Value;
    }

    [Fact]
    public void Deserialize_MultipleValues()
    {
        // { "doubleValue" : 0.68999999999999995, "stringValue" : "myString", "intValue" : 42 }
        var bytes = Convert.FromBase64String("QgAAAAFkb3VibGVWYWx1ZQAUrkfhehTmPwJzdHJpbmdWYWx1ZQAJAAAAbXlTdHJpbmcAEGludFZhbHVlACoAAAAA");
        var doc = BsonSerializer.Deserialize(bytes);

        Assert.Equal(3, doc.Elements.Count);

        var doubleValue = (BsonDouble)doc.Elements.First(el => el.Name == "doubleValue").Value;
        Assert.Equal(0.69, doubleValue.Value, 10);

        var stringValue = (BsonString)doc.Elements.First(el => el.Name == "stringValue").Value;
        Assert.Equal("myString", stringValue.Value);

        var intValue = (BsonInteger)doc.Elements.First(el => el.Name == "intValue").Value;
        Assert.Equal(42, intValue.Value);
    }
}
