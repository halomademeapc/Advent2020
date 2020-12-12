using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace Advent2020.Tests
{
    public class Day12Test
    {
        private IEnumerable<string> instructions = @"F10
N3
F7
R90
F11".Split(Environment.NewLine);

        [Fact]
        public void Navigate_Simple()
        {
            Assert.Equal(new Vector2(17, -8), Day12_Navigation.Navigate(instructions));
        }

        [Fact]
        public void Follow_Waypoint_Forward()
        {
            var res = Day12_Waypoint.Navigate(instructions.Take(1));
            Assert.Equal(new Vector2(100, 10), res.Ship);
            Assert.Equal(new Vector2(10, 1), res.Waypoint);
        }

        [Fact]
        public void Move_Waypoint_North()
        {
            var res = Day12_Waypoint.Navigate(instructions.Take(2));
            Assert.Equal(new Vector2(100, 10), res.Ship);
            Assert.Equal(new Vector2(10, 4), res.Waypoint);
        }

        [Fact]
        public void Follow_Moved_Waypoint()
        {
            var res = Day12_Waypoint.Navigate(instructions.Take(3));
            Assert.Equal(new Vector2(170, 38), res.Ship);
            Assert.Equal(new Vector2(10, 4), res.Waypoint);
        }

        [Fact]
        public void Rotate_Waypoint_About_Ship()
        {
            var res = Day12_Waypoint.Navigate(instructions.Take(4));
            Assert.Equal(new Vector2(170, 38), res.Ship);
            Assert.Equal(new Vector2(4, -10), res.Waypoint);
        }

        [Fact]
        public void Follow_Waypoint()
        {
            Assert.Equal(new Vector2(214, -72), Day12_Waypoint.Navigate(instructions).Ship);
        }
    }
}
