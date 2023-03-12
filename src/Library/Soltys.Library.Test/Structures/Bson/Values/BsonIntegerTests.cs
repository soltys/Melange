using Soltys.Library.Bson;
using Xunit;

namespace Soltys.Library.Test.Bson;

public class BsonIntegerTests
{
    public static IEnumerable<object[]> EqualOperatorData =>
        new List<object[]>
        {
            new object[] { new BsonInteger(42), new BsonInteger(42), true },
            new object[] { new BsonInteger(42), new BsonInteger(41), false },
            new object[] { null, new BsonInteger(42), false },
            new object[] { new BsonInteger(42), null, false },
            new object[] { null, null, true },
        };

    [Theory]
    [MemberData(nameof(EqualOperatorData))]
    public void EqualOperator_EqualObjects_ReturnsExpectedValue(BsonInteger lhs, BsonInteger rhs, bool expectedValue)
        => Assert.Equal(expectedValue, lhs == rhs);

    [Fact]
    public void NotEqualOperator_NotEqualObject_ReturnsFalse() =>
        // ReSharper disable once EqualExpressionComparison
        Assert.False(new BsonInteger(42) != new BsonInteger(42));

    [Fact]
    public void Equals_ValuesEqual_ReturnsTrue() => Assert.True(new BsonInteger(42).Equals(new BsonInteger(42)));

    [Fact]
    public void Equals_ValuesNotEqual_ReturnsFalse() => Assert.False(new BsonInteger(41).Equals(new BsonInteger(42)));

    [Fact]
    public void ToString_BsonInteger() => 
        Assert.Equal("42", new BsonInteger(42).ToString());

    [Fact]
    public void GetHashCode_SameAsValueHashCode() =>
        Assert.Equal(42.GetHashCode(), new BsonInteger(42).GetHashCode());

}
