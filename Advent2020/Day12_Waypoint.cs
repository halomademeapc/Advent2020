using System;
using System.Collections.Generic;
using System.Numerics;

namespace Advent2020
{
    public class Day12_Waypoint : IPuzzleResult<float>
    {
        public static NavigationResult Navigate(IEnumerable<string> instructions)
        {
            var ship = new Vector2(0, 0);
            var waypoint = new Vector2(10, 1); // relative to ship, start waypoint 10 east 1 north

            foreach (var instruction in instructions)
            {
                var command = instruction[0];
                var param = int.Parse(instruction[1..]);
                switch (command)
                {
                    case 'N': // move waypoint
                        waypoint += new Vector2(0, param);
                        break;
                    case 'S':
                        waypoint -= new Vector2(0, param);
                        break;
                    case 'E':
                        waypoint += new Vector2(param, 0);
                        break;
                    case 'W':
                        waypoint -= new Vector2(param, 0);
                        break;
                    case 'L': // rotate waypoint about ship n degrees
                        waypoint = waypoint.RotateLeft(param);
                        break;
                    case 'R':
                        waypoint = waypoint.RotateRight(param);
                        break;
                    case 'F': // move to waypoint n times
                        ship += waypoint * param;
                        break;
                    default:
                        throw new ArgumentException($"{instruction} is not a valid instrution.");
                }
            }

            return new NavigationResult
            {
                Ship = ship,
                Waypoint = waypoint
            };
        }

        public struct NavigationResult
        {
            public Vector2 Ship;
            public Vector2 Waypoint;
        }

        public float GetResult() => Navigate(Resources.Day12Values.Split(Environment.NewLine)).Ship.ManhattanDistance();
    }
}