using Advent2020;
using System;
using System.Linq;

namespace Advent2020
{
    public class Day05_MissingPass : IPuzzleResult<int>
    {
        public int GetResult()
        {
            var ids = Day05_Boarding.GetSeatIds().Select(i => (int)i);
            var range = Enumerable.Range(ids.Min(), ids.Max() - ids.Min());
            return range.Where(i => !ids.Contains(i))
                .Where(i => ids.Contains(i - 1))
                .Where(i => ids.Contains(i + 1))
                .FirstOrDefault();
        }
    }
}