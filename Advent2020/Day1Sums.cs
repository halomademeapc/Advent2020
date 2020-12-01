using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class Day1_Sums2 : IPuzzleResult
    {
        protected readonly IEnumerable<int> SampleValues;

        public Day1_Sums2()
        {
            SampleValues = Resources.Day1Values.Split(Environment.NewLine).Select(int.Parse);
        }

        /// <summary>
        /// Pick out n numbers from the data set which sum to the specified number and multiply them together
        /// </summary>
        /// <param name="numberPerSet">How many numbers to group together</param>
        /// <param name="options">Data set to run against</param>
        /// <param name="sum">Sum to look for</param>
        /// <returns>Product of components</returns>
        public static int GetProduct(IEnumerable<int> options, int sum, int numberPerSet)
        {
            var pairs = GetPairings(options, numberPerSet);
            var match = pairs.First(p => p.Sum() == sum);
            return match.Aggregate((accumulate, source) => accumulate * source);
        }

        /// <summary>
        /// Get all unique combinations of a set
        /// </summary>
        /// <typeparam name="TCollection">Type of items in collection</typeparam>
        /// <param name="collection">Items in collection</param>
        /// <param name="numberPerSet">Number of elements to put in each set</param>
        private static IEnumerable<IEnumerable<TCollection>> GetPairings<TCollection>(IEnumerable<TCollection> collection, int numberPerSet)
        {
            IEnumerable<IEnumerable<TCollection>> result = collection.Select(Extensions.ToSingleElementSequence);
            foreach (var _ in Enumerable.Range(0, numberPerSet - 1))
                result = result.SelectMany(r => collection.Select(o => r.Append(o)));
            return result;
        }

        public virtual object GetResult() => GetProduct(SampleValues, 2020, 2);
    }
}
