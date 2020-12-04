using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advent2020
{
    public static class Extensions
    {
        public static IEnumerable<TItem> ToSingleElementSequence<TItem>(this TItem item)
        {
            yield return item;
        }

        public static long Product(this IEnumerable<int> ints) => ints.Aggregate<int, long>(1, (total, next) => total * next);

        public static TEnum ToEnum<TEnum>(this string @string) where TEnum : Enum
        {
            var mappings = GetMappings<TEnum>();
            if (mappings.ContainsKey(@string))
                return mappings[@string];
            return default;
        }

        public static Dictionary<string, TEnum> GetMappings<TEnum>() where TEnum : Enum
        {
            var result = new Dictionary<string, TEnum>();
            var enumType = typeof(TEnum);
            var enumValues = Enum.GetValues(enumType).Cast<TEnum>();
            foreach (var member in enumType.GetMembers().Where(m => enumValues.Select(e => e.ToString()).Contains(m.Name)))
            {
                var valueAttribute = member.GetCustomAttribute<EnumValueAttribute>();
                result[valueAttribute == default ? member.Name : valueAttribute.Value] = enumValues.First(e => e.ToString() == member.Name);
            }
            return result;
        }
    }
}
