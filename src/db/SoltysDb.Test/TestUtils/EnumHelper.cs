using System;
using System.Collections.Generic;
using System.Linq;

namespace SoltysDb.Test.TestUtils
{
    public static class EnumHelper
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>

        public static IEnumerable<TEnum> GetKeywords<TEnum>() where TEnum : Enum
        {
            var type = typeof(TEnum);
            var members = type.GetMembers();

            return members.Where(x => x.GetCustomAttributes(typeof(KeywordAttribute), false).Any())
                            .Select(x => (TEnum)Enum.Parse(type, x.Name));
        }
    }
}
