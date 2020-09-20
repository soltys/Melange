using SoltysLib.Bson;
using SoltysLib.Bson.BQuery;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class BQueryTests
    {
        [Theory]
        [InlineData("foo")]
        [InlineData("['foo']")]
        [InlineData("[\"foo\"]")]
        public void QueryValue_AccessingSimpleData(string input)
        {
            var stringValue = new BsonString("bar");
            var doc = new BsonDocument(new Element("foo", stringValue));

            var value = doc.QueryValue(input);
            Assert.Equal("bar", stringValue.Value);
            Assert.Same(stringValue, value);
        }

        [Theory]
        [InlineData("foo.bar")]
        [InlineData("['foo'].['bar']")]
        [InlineData("[\"foo\"].['bar']")]
        public void QueryValue_AccessingNestedDocumentData(string input)
        {
            var stringValue = new BsonString("dog");
            var doc = new BsonDocument(
                new Element("foo",
                    new BsonDocument(new Element("bar", stringValue))));

            var value = doc.QueryValue(input);
            Assert.Equal("dog", stringValue.Value);
            Assert.Same(stringValue, value);
        }

        [Theory]
        [InlineData("foo[1]")]
        [InlineData("['foo'][1]")]
        [InlineData("[\"foo\"][1]")]
        public void QueryValue_AccessingArrayValue(string input)
        {
            var arrayValue = new BsonArray(new BsonInteger(1), new BsonInteger(42), new BsonInteger(69));
            var doc = new BsonDocument(new Element("foo", arrayValue));
            var value = (BsonInteger)doc.QueryValue(input);
            Assert.Equal(42, value.Value);
        }

        [Fact]
        public void QueryValue_AccessingDocumentValueFromArray()
        {
            var arrayValue = new BsonArray(
                new BsonDocument(new Element("bar", new BsonInteger(1))),
                new BsonDocument(new Element("bar", new BsonInteger(42))),
                new BsonDocument(new Element("bar", new BsonInteger(69)))
            );
            var doc = new BsonDocument(new Element("foo", arrayValue));
            var value = (BsonInteger)doc.QueryValue("foo[1].bar");
            Assert.Equal(42, value.Value);
        }
    }
}
