using SoltysLib.Bson;
using SoltysLib.Bson.BQuery;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class BQueryTests
    {
        [Fact]
        public void QueryValue_AccessingSimpleData()
        {
            var stringValue = new BsonString("bar");
            var doc = new BsonDocument(new Element("foo", stringValue));

            var value = doc.QueryValue("foo");
            Assert.Equal("bar", stringValue.Value);
            Assert.Same(stringValue, value);
        }

        [Fact]
        public void QueryValue_AccessingNestedDocumentData()
        {
            var stringValue = new BsonString("dog");
            var doc = new BsonDocument(
                new Element("foo",
                    new BsonDocument(new Element("bar", stringValue))));

            var value = doc.QueryValue("foo.bar");
            Assert.Equal("dog", stringValue.Value);
            Assert.Same(stringValue, value);
        }

        [Fact]
        public void QueryValue_AccessingArrayValue()
        {
            var arrayValue = new BsonArray(new BsonInteger(1), new BsonInteger(42), new BsonInteger(69));
            var doc = new BsonDocument(new Element("foo", arrayValue));
            var value = (BsonInteger)doc.QueryValue("foo[1]");
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
