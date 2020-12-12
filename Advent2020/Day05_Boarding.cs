using Advent2020;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Day05_Boarding : IPuzzleResult<double>
{
    private static readonly Regex SeatRgx = new Regex(@"^[BF]{7}[LR]{3}$");

    public static double GetSeatId(string boardingPass)
    {
        if (!SeatRgx.IsMatch(boardingPass))
            throw new ArgumentException($"{boardingPass} is not a valid boarding pass.");

        var row = GetPartitionPosition(boardingPass.Substring(0, 7), 'B');
        var col = GetPartitionPosition(boardingPass.Substring(7, 3), 'R');
        return row * 8 + col;
    }

    public static double GetPartitionPosition(string sequence, char upper) => sequence
            .Select((@char, index) => (@char, index))
            .Where(group => group.@char == upper)
            .Select(group => Math.Pow(2, sequence.Count() - group.index - 1))
            .Sum();

    public static IEnumerable<double> GetSeatIds() =>
        Resources.Day5Values.Split(Environment.NewLine).Select(GetSeatId);

    public double GetResult() => GetSeatIds().Max();
}
