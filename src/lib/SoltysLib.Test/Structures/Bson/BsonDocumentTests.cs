using SoltysLib.Bson;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class BsonDocumentTests
    {

        [Fact]
        public void ToString_EmptyDocument()
        {
            var doc = new BsonDocument();
            Assert.Equal("{  }", doc.ToString());
        }

        [Fact]
        public void ToString_IntegerElement()
        {
            var doc = new BsonDocument(new Element("foo", new BsonInteger(42)));
            Assert.Equal("{ \"foo\": 42 }", doc.ToString());
        }

        [Fact]
        public void ToString_TwoElements()
        {
            var doc = new BsonDocument(new Element("foo", new BsonInteger(42)), new Element("bar", new BsonString("dog")));
            Assert.Equal("{ \"foo\": 42, \"bar\": \"dog\" }", doc.ToString());
        }
    }
}
