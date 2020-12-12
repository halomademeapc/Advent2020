using System.Linq;
using Xunit;

namespace Advent2020.Tests
{
    public class Day11Test
    {
        private readonly Day11_SeatQueue.PositionContent[][] Example = Day11_SeatQueue.Parse(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL");

        [Fact]
        public void First_Iteration()
        {
            var result = Day11_SeatQueue.Simulate(Example, 1);
            Assert.Equal(@"#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##", Day11_SeatQueue.Stringify(result));
        }

        [Fact]
        public void Second_Iteration()
        {
            var result = Day11_SeatQueue.Simulate(Example, 2);
            Assert.Equal(@"#.LL.L#.##
#LLLLLL.L#
L.L.L..L..
#LLL.LL.L#
#.LL.LL.LL
#.LLLL#.##
..L.L.....
#LLLLLLLL#
#.LLLLLL.L
#.#LLLL.##", Day11_SeatQueue.Stringify(result));
        }
    }
}
