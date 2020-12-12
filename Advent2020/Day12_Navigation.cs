using System;
using System.Collections.Generic;
using System.Numerics;

namespace Advent2020
{
    public class Day12_Navigation : IPuzzleResult<float>
    {
        public static Vector2 Navigate(IEnumerable<string> instructions)
        {
            var position = new Vector2(0, 0);
            var orientation = new Vector2(1, 0); // start facing east

            foreach (var instruction in instructions)
            {
                var command = instruction[0..1];
                var param = int.Parse(instruction[1..]);
                switch (command)
                {
                    case "N":
                        position += new Vector2(0, param);
                        break;
                    case "S":
                        position -= new Vector2(0, param);
                        break;
                    case "E":
                        position += new Vector2(param, 0);
                        break;
                    case "W":
                        position -= new Vector2(param, 0);
                        break;
                    case "L":
                        orientation = orientation.RotateLeft(param);
                        break;
                    case "R":
                        orientation = orientation.RotateRight(param);
                        break;
                    case "F":
                        position += param * orientation;
                        break;
                    default:
                        throw new ArgumentException($"{instruction} is not a valid instrution.");
                }
            }

            return position;
        }

        public float GetResult() => Navigate(Resources.Day12Values.Split(Environment.NewLine)).ManhattanDistance();
    }
}