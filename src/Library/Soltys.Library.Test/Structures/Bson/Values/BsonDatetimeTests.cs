using Soltys.Library.Bson;
using Xunit;

namespace Soltys.Library.Test.Bson;

public class BsonDatetimeTests
{
    [Fact]
    public void EqualOperator_EqualObjects_ReturnsTrue() =>
        // ReSharper disable once EqualExpressionComparison
        Assert.True((bool) (new BsonDatetime(1599760732000L) == new BsonDatetime(1599760732000L)));

    [Fact]
    public void NotEqualOperator_NotEqualObject_ReturnsFalse() =>
        // ReSharper disable once EqualExpressionComparison
        Assert.False((bool) (new BsonDatetime(1599760732000L) != new BsonDatetime(1599760732000L)));

    [Fact]
    public void Equals_ValuesEqual_ReturnsTrue() => Assert.True((bool) new BsonDatetime(1599760732000L).Equals(new BsonDatetime(1599760732000L)));

    [Fact]
    public void Equals_ValuesNotEqual_ReturnsFalse() => Assert.False((bool) new BsonDatetime(1599750732000L).Equals(new BsonDatetime(1599760732000L)));

    [Fact]
    public void ToString_DateTime() => Assert.Equal((string) "Date(1599760732000)", (string) new BsonDatetime(1599760732000L).ToString());

    [Fact]
    public void GetHashCode_SameAsValueHashCode() =>
        Assert.Equal(1599760732000L.GetHashCode(), new BsonDatetime(1599760732000L).GetHashCode());
}