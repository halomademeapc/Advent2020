using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class Day03_Toboggan : IPuzzleResult<string>
    {
        // hill tiles infinitely horizontally
        private readonly HillContent[][] Hill;

        public Day03_Toboggan()
        {
            Hill = Resources.Day3Values
                .Split(Environment.NewLine)
                .Select(line => line.Select(Parse).ToArray())
                .ToArray();
        }

        public IEnumerable<HillContent> GetPositions(MovementPattern pattern)
        {
            var verticalIndex = 0;
            var horizontalIndex = 0;

            do
            {
                yield return Hill[verticalIndex][horizontalIndex];
                verticalIndex += pattern.Vertical;
                horizontalIndex += pattern.Horizontal;
                horizontalIndex = horizontalIndex % Hill[0].Length;
                if (horizontalIndex == Hill[0].Length)
                    horizontalIndex = 0;
            } while (verticalIndex < Hill.Length);
        }

        static HillContent Parse(char @char) => @char switch
        {
            '#' => HillContent.Tree,
            '.' => HillContent.Empty,
            _ => throw new ArgumentException($"{@char} is not a valid specifier.")
        };

        public string GetResult()
        {
            var tests = new List<MovementPattern>
            {
                new MovementPattern(1,1),
                new MovementPattern(3,1),
                new MovementPattern(5,1),
                new MovementPattern(7,1),
                new MovementPattern(1,2)
            };
            var results = tests.Select(t => new { Input = t, Output = GetPositions(t) });
            var formattedResults = results.Select(@case =>
            {
                var treeCount = @case.Output.Count(p => p == HillContent.Tree);
                var openCount = @case.Output.Count(p => p != HillContent.Tree);
                return $"Right {@case.Input.Horizontal}, Down {@case.Input.Vertical}: {treeCount} trees, {openCount} open.";
            });
            var product = results
                .Select(r => r.Output.Count(c => c == HillContent.Tree))
                .Product();

            return $@"
{string.Join(Environment.NewLine, formattedResults)}
Product: {product}";
        }

        public enum HillContent
        {
            Empty,
            Tree
        }

        // top-left origin
        public struct MovementPattern
        {
            public int Horizontal;
            public int Vertical;

            public MovementPattern(int horizontal, int vertical)
            {
                Horizontal = horizontal;
                Vertical = vertical;
            }
        }
    }
}
