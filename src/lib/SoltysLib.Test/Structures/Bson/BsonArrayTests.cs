using SoltysLib.Bson;
using Xunit;

namespace SoltysLib.Test.Bson
{
    public class BsonArrayTests
    {
        [Theory]
        [InlineData(0,1)]
        [InlineData(1,42)]
        [InlineData(2,69)]
        public void ValueAccessor(int index, int expectedValue)
        {
            var a = new BsonArray(new BsonInteger(1), new BsonInteger(42), new BsonInteger(69));
            var actual = a[index];
            var actualInt = (BsonInteger)actual;
            Assert.Equal(expectedValue, actualInt.Value);
        }
    }
}
