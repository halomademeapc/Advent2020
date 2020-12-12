using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class Day11_SeatVisibility : Day11_SeatQueue
    {
        public static new PositionContent[][] Simulate(PositionContent[][] positions, int maxIterations = 1000)
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

            IEnumerable<PositionContent> GetVisible(int row, int col)
            {
                var directions = Enumerable.Range(-1, 3).SelectMany(c => Enumerable.Range(-1, 3).Select(r => (Column: c, Row: r)))
                    .Where(p => p.Column != 0 || p.Row != 0);

                PositionContent? GetSeat((int Row, int Column) direction)
                {
                    var r = row;
                    var c = col;

                    bool IsInBounds() => (r >= 0 && r < previousPositions.Length) && (c >= 0 && c < previousPositions[0].Length);
                    bool IsViewer() => r == row && c == col;

                    while (IsInBounds())
                    {
                        if (!IsViewer() && previousPositions[r][c] != PositionContent.Floor)
                            return previousPositions[r][c];
                        r += direction.Row;
                        c += direction.Column;
                    }
                    return null;
                }

                foreach (var direction in directions)
                {
                    var firstSeat = GetSeat(direction);
                    if (firstSeat.HasValue)
                        yield return firstSeat.Value;
                }
            }

            PositionContent GetNextState(int row, int col)
            {
                var current = previousPositions[row][col];
                if (current == PositionContent.EmptySeat && !GetVisible(row, col).Any(a => a == PositionContent.OccupiedSeat))
                    return PositionContent.OccupiedSeat; // if empty and none visible are taken, becomes occupied
                if (current == PositionContent.OccupiedSeat && GetVisible(row, col).Count(a => a == PositionContent.OccupiedSeat) >= 5)
                    return PositionContent.EmptySeat; // if taken and 5+ visible seats taken, person bails
                return current;
            }

            bool HasChange() => previousPositions.SelectMany(p => p).Zip(newPositions.SelectMany(p => p)).Any(p => p.First != p.Second);
        }

        public override int GetResult() => Simulate(Positions).SelectMany(r => r).Count(s => s == PositionContent.OccupiedSeat);
    }
}