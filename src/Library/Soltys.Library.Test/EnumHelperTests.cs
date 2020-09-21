using System;
using Xunit;

namespace Soltys.Library.Test
{
    public class EnumHelperTests
    {
        [Fact]
        public void GetEnumValues_GetsSelectedEnumValuesByAttribute()
        {
            var actualValues =
                EnumHelper.GetEnumValuesWithAttribute<TestGetEnumValues>(typeof(TestEnumAttribute));

            var expectedValues = new[] {TestGetEnumValues.B, TestGetEnumValues.C};
            Assert.Equal(expectedValues, actualValues);
        }

        [Fact]
        public void GetEnumFieldAttribute_GetsEnumFieldAttribute()
        {
            var actualEnumAttribute = EnumHelper.GetEnumFieldAttribute<TestEnumAttribute>(TestGetEnumValues.B);
            Assert.Equal(42, actualEnumAttribute.Value);
        }

        private enum TestGetEnumValues
        {
            A,
            [TestEnum(Value = 42)]
            B,
            [TestEnum(Value = 69)]
            C
        }

        [AttributeUsage(AttributeTargets.Field)]
        private class TestEnumAttribute : Attribute
        {
            public int Value
            {
                get;
                set;
            }
        }
    }
}
