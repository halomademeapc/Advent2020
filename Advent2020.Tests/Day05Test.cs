using Xunit;

namespace Advent2020.Tests
{
    public class Day05Test
    {
        [Fact]
        public void Validate_Example_Cases()
        {
            Assert.Equal(44, Day05_Boarding.GetPartitionPosition("FBFBBFF", 'B'));
            Assert.Equal(5, Day05_Boarding.GetPartitionPosition("RLR", 'R'));
            Assert.Equal(567, Day05_Boarding.GetSeatId("BFFFBBFRRR"));
            Assert.Equal(119, Day05_Boarding.GetSeatId("FFFBBBFRRR"));
            Assert.Equal(820, Day05_Boarding.GetSeatId("BBFFBBFRLL"));
        }
    }
}
