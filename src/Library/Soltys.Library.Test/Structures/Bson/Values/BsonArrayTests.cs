using Soltys.Library.Bson;
using Xunit;

namespace Soltys.Library.Test.Bson;

public class BsonArrayTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 42)]
    [InlineData(2, 69)]
    public void ValueAccessor(int index, int expectedValue)
    {
        var a = new BsonArray(new BsonInteger(1), new BsonInteger(42), new BsonInteger(69));
        var actual = a[index];
        var actualInt = (BsonInteger)actual;
        Assert.Equal(expectedValue, actualInt.Value);
    }

    [Fact]
    public void ToString_Array() => 
        Assert.Equal("[1, 2, 3]", new BsonArray(new BsonInteger(1), new BsonInteger(2), new BsonInteger(3)).ToString());
}