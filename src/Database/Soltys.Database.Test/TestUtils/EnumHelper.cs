using System;
using System.Collections.Generic;

namespace Soltys.Database.Test.TestUtils
{
    public static class EnumHelper
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>

        public static IEnumerable<TEnum> GetKeywords<TEnum>() where TEnum : Enum => Library.EnumHelper.GetEnumValuesWithAttribute<TEnum>(typeof(KeywordAttribute));
    }
}
