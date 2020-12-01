using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2020
{
    public static class Extensions
    {
        public static IEnumerable<TItem> ToSingleElementSequence<TItem>(this TItem item)
        {
            yield return item;
        }
    }
}
