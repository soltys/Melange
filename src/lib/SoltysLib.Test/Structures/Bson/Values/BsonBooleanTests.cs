using System.Collections.Generic;
using SoltysLib.Bson;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class BsonBooleanTests
    {
        public static IEnumerable<object[]> EqualOperatorData =>
            new List<object[]>
            {
                new object[] { new BsonBoolean(true), new BsonBoolean(true), true },
                new object[] { new BsonBoolean(true), new BsonBoolean(false), false },
                new object[] { null, new BsonBoolean(false), false },
                new object[] { new BsonBoolean(false), null, false },
                new object[] { null, null, true },
            };

        [Theory]
        [MemberData(nameof(EqualOperatorData))]
        public void EqualOperator_EqualObjects_ReturnsExpectedValue(BsonBoolean lhs, BsonBoolean rhs, bool expectedValue)
            => Assert.Equal(expectedValue, lhs == rhs);

        [Fact]
        // ReSharper disable once EqualExpressionComparison
        public void NotEqualOperator_EqualObject_ReturnsFalse() => Assert.False(new BsonBoolean(true) != new BsonBoolean(true));

        [Fact]
        // ReSharper disable once EqualExpressionComparison
        public void Equals_ValuesEqual_ReturnsTrue() => Assert.True(new BsonBoolean(true).Equals(new BsonBoolean(true)));

        [Fact]
        public void Equals_ValuesNotEqual_ReturnsFalse() => Assert.False(new BsonBoolean(false).Equals(new BsonBoolean(true)));
        [Fact]
        public void ToString_BsonBooleanTrue() => Assert.Equal("true", new BsonBoolean(true).ToString());
        [Fact]
        public void ToString_BsonBooleanFalse() => Assert.Equal("false", new BsonBoolean(false).ToString());

        [Fact]
        public void GetHashCode_SameAsValueHashCode_True() =>
            Assert.Equal(true.GetHashCode(), new BsonBoolean(true).GetHashCode());

        [Fact]
        public void GetHashCode_SameAsValueHashCode_False() =>
            Assert.Equal(false.GetHashCode(), new BsonBoolean(false).GetHashCode());
    }
}
