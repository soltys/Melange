using System.Collections.Generic;
using SoltysLib.Bson;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class BsonStringTests
    {
        public static IEnumerable<object[]> EqualOperatorData =>
            new List<object[]>
            {
                new object[] { new BsonString("foo"), new BsonString("foo"), true },
                new object[] { new BsonString("foo"), new BsonString("bar"), false },
                new object[] { null, new BsonString("foo"), false },
                new object[] { new BsonString("foo"), null, false },
                new object[] { null, null, true },
            };

        [Theory]
        [MemberData(nameof(EqualOperatorData))]
        public void EqualOperator_EqualObjects_ReturnsExpectedValue(BsonString lhs, BsonString rhs, bool expectedValue)
            => Assert.Equal(expectedValue, lhs == rhs);

        [Fact]
        public void NotEqualOperator_EqualObject_ReturnsFalse() =>
            // ReSharper disable once EqualExpressionComparison
            Assert.False(new BsonString("foo") != new BsonString("foo"));

        [Fact]
        public void Equals_ValuesEqual_ReturnsTrue() => Assert.True(new BsonString("foo").Equals(new BsonString("foo")));

        [Fact]
        public void Equals_ValuesNotEqual_ReturnsFalse() => Assert.False(new BsonString("bar").Equals(new BsonString("foo")));

        [Fact]
        public void ToString_BsonInteger() =>
            Assert.Equal("\"foo\"", new BsonString("foo").ToString());

        [Fact]
        public void GetHashCode_SameAsValueHashCode() =>
            Assert.Equal("foo".GetHashCode(), new BsonString("foo").GetHashCode());
    }
}
