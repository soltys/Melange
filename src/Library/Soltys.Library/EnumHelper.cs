using System;
using System.Collections.Generic;
using System.Linq;

namespace Soltys.Library
{
    public static class EnumHelper
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>

        public static IEnumerable<TEnum> GetEnumValuesWithAttribute<TEnum>(Type attributeType) where TEnum : Enum
        {
            var type = typeof(TEnum);
            var members = type.GetMembers();

            return members.Where(x => x.GetCustomAttributes(attributeType, false).Any())
                .Select(x => (TEnum)Enum.Parse(type, x.Name));
        }

        public static TAttribute? GetEnumFieldAttribute<TAttribute>(Enum e) where TAttribute : Attribute
        {
            var memberInfo = e.GetType()
                .GetMember(e.ToString())
                .FirstOrDefault();

            if (memberInfo == null)
            {
                return null;
            }

            return (TAttribute)memberInfo
                .GetCustomAttributes(typeof(TAttribute), false)
                .FirstOrDefault();
        }
    }
}
