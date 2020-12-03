using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent2020
{
    public static class Extensions
    {
        public static IEnumerable<TItem> ToSingleElementSequence<TItem>(this TItem item)
        {
            yield return item;
        }

        public static long Product(this IEnumerable<int> ints) => ints.Aggregate<int, long>(1, (total, next) => total * next);
    }
}
