using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Advent2020
{
    public class Day11_SeatQueue : IPuzzleResult<int>
    {
        protected PositionContent[][] Positions;

        public Day11_SeatQueue()
        {
            Positions = Parse(Resources.Day11Values);
        }

        public static PositionContent[][] Parse(string serialized) => serialized.Split(Environment.NewLine)
                .Select(line => line.Select(@char => @char switch
                {
                    '#' => PositionContent.OccupiedSeat,
                    'L' => PositionContent.EmptySeat,
                    _ => PositionContent.Floor
                }).ToArray()).ToArray();

        public static PositionContent[][] Simulate(PositionContent[][] positions, int maxIterations = 1000)
        {
            var previousPositions = positions;
            var newPositions = positions;
            var iterations = 0;
            do
            {
                previousPositions = newPositions;
                newPositions = previousPositions.Select((row, rowIndex) => row.Select((col, colIndex) => GetNextState(rowIndex, colIndex)).ToArray()).ToArray();
                iterations++;
            }
            while (iterations < maxIterations && HasChange());

            return newPositions;

            IEnumerable<PositionContent> GetAdjacents(int row, int col)
            {
                for (int r = row - 1; r <= row + 1; r++) // row/column before, on, and after
                    for (int c = col - 1; c <= col + 1; c++)
                        if (r >= 0 && r < previousPositions.Length) // in bounds
                            if (c >= 0 && c < previousPositions[0].Length)
                                if (r != row || c != col) // not the one we're checking
                                    yield return previousPositions[r][c];
            }

            PositionContent GetNextState(int row, int col)
            {
                var current = previousPositions[row][col];
                var adjacents = GetAdjacents(row, col).ToList();
                if (current == PositionContent.EmptySeat && !GetAdjacents(row, col).Any(a => a == PositionContent.OccupiedSeat))
                    return PositionContent.OccupiedSeat; // if empty and none around are taken, becomes occupied
                if (current == PositionContent.OccupiedSeat && GetAdjacents(row, col).Count(a => a == PositionContent.OccupiedSeat) >= 4)
                    return PositionContent.EmptySeat; // if taken and 4+ seats around taken, person bails
                return current;
            }

            bool HasChange() => previousPositions.SelectMany(p => p).Zip(newPositions.SelectMany(p => p)).Any(p => p.First != p.Second);
        }

        public virtual int GetResult() => Simulate(Positions).SelectMany(r => r).Count(s => s == PositionContent.OccupiedSeat);

        public enum PositionContent
        {
            Floor,
            EmptySeat,
            OccupiedSeat
        }

        public static string Stringify(PositionContent[][] positions)
        {
            return string.Concat(Translate());
            IEnumerable<string> Translate()
            {
                for (int row = 0; row < positions.Count(); row++)
                {
                    for (int col = 0; col < positions[0].Count(); col++)
                    {
                        yield return positions[row][col] switch
                        {
                            PositionContent.EmptySeat => "L",
                            PositionContent.OccupiedSeat => "#",
                            _ => "."
                        };
                    }
                    if (row != positions.Count() - 1)
                        yield return Environment.NewLine;
                }
            }
        }
    }
}