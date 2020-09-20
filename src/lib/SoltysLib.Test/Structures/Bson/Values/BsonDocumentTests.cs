using System.Collections.Generic;
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

        [Fact]
        public void ToDictionary_EmptyDocument_EmptyDictionary()
        {
            var doc = new BsonDocument();
            var actual = doc.ToDictionary();
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void ToDictionary_WithElements_NotSameReferenceToValue()
        {
            var intValue = new BsonInteger(42);
            var doc = new BsonDocument(
                new Element("foo", intValue));

            var actual = doc.ToDictionary();
            Assert.Single(actual);

            Assert.Equal(42, ((BsonInteger)actual["foo"]).Value);
        }

        [Fact]
        public void Constructor_CreatesElementsFromStringBsonValue()
        {
            var firstValue = new BsonString("bar");
            var secValue = new BsonInteger(42);
            var doc = new BsonDocument(new Dictionary<string, BsonValue> {
                {"foo" , firstValue},
                {"2nd" , secValue}
            });

            Assert.Equal(2, doc.Elements.Count);

            var dict = doc.ToDictionary();

            Assert.Equal(firstValue, dict["foo"]);
            Assert.Equal(secValue, dict["2nd"]);
        }
    }
}
