using SoltysLib.Bson;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class ElementTests
    {
        [Fact]
        public void ToString_StringValue() => AssertToString("\"foo\": \"bar\"", new Element("foo", new BsonString("bar")));
        [Fact]
        public void ToString_IntegerValue() => AssertToString("\"foo\": 42", new Element("foo", new BsonInteger(42)));
        [Fact]
        public void ToString_DoubleValue() => AssertToString("\"foo\": 0.69", new Element("foo", new BsonDouble(0.69)));
        [Fact]
        public void ToString_BooleanTrueValue() => AssertToString("\"foo\": true", new Element("foo", new BsonBoolean(true)));
        [Fact]
        public void ToString_BooleanFalseValue() => AssertToString("\"foo\": false", new Element("foo", new BsonBoolean(false)));
        [Fact]
        public void ToString_NullValue() => AssertToString("\"foo\": null", new Element("foo",  BsonNull.Value));

        private void AssertToString(string expectedToString, Element element) => Assert.Equal(expectedToString, element.ToString());
    }
}
