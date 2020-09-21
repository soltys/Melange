using Soltys.Library.Bson;
using Xunit;

namespace Soltys.Library.Test.Bson
{
    public class BsonDoubleTests
    {

        [Fact]
        public void EqualOperator_EqualObjects_ReturnsTrue() =>
            // ReSharper disable once EqualExpressionComparison
            Assert.True(new BsonDouble(0.69) == new BsonDouble(0.69));

        [Fact]
        public void NotEqualOperator_NotEqualObject_ReturnsFalse() =>
            // ReSharper disable once EqualExpressionComparison
            Assert.False(new BsonDouble(0.69) != new BsonDouble(0.69));

        [Fact]
        public void Equals_ValuesEqual_ReturnsTrue() => Assert.True(new BsonDouble(0.69).Equals(new BsonDouble(0.69)));

        [Fact]
        public void Equals_ValuesNotEqual_ReturnsFalse() => Assert.False(new BsonDouble(0.6).Equals(new BsonDouble(0.69)));

        [Fact]
        public void ToString_BsonDouble() => Assert.Equal("0.69", new BsonDouble(0.69).ToString());

        [Fact]
        public void GetHashCode_SameAsValueHashCode() =>
            Assert.Equal(0.69.GetHashCode(), new BsonDouble(0.69).GetHashCode());
    }
}
