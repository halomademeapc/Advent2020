using System.Linq;
using Xunit;

namespace Advent2020.Tests
{
    public class Day10Test
    {
        private int[] smallSample = new int[] { 16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4 };

        [Fact]
        public void Small_Counts()
        {
            var jumps = Day10_PowerAdapters.GetJumps(smallSample);
            Assert.Equal(7, jumps.Count(j => j == 1));
            Assert.Equal(5, jumps.Count(j => j == 3));
        }
    }
}
