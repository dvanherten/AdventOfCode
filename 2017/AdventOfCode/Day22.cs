using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Supporting;

namespace AdventOfCode
{
    public class Day22
    {
        public static int SolvePart1(string[] input)
        {
            var infectedPoints = ParseInfected(input);
            var location = new Point(input.Length / 2, input.Length / 2);
            var direction = Direction.Up;

            var bursts = 10000;
            var infectionsCaused = 0;
            for (var i = 0; i < bursts; i++)
            {
                if (infectedPoints.Contains(location))
                {
                    direction = ChangeDirection(direction, false);
                    infectedPoints.Remove(location);
                }
                else
                {
                    direction = ChangeDirection(direction, true);
                    infectedPoints.Add(location);
                    infectionsCaused++;
                }

                location = location.GetNextPoint(direction);
            }

            return infectionsCaused;
        }

        public static int SolvePart2(string[] input)
        {
            var knownPoints = ParseInfected(input).ToDictionary(x => x, y => InfectionStatus.Infected);
            var location = new Point(input.Length / 2, input.Length / 2);
            var direction = Direction.Up;

            var bursts = 10000000;
            var infectionsCaused = 0;
            for (var i = 0; i < bursts; i++)
            {
                if (!knownPoints.ContainsKey(location))
                    knownPoints[location] = InfectionStatus.Clean;

                switch (knownPoints[location])
                {
                    case InfectionStatus.Clean:
                        direction = ChangeDirection(direction, true);
                        knownPoints[location] = InfectionStatus.Weakened;
                        break;
                    case InfectionStatus.Weakened:
                        knownPoints[location] = InfectionStatus.Infected;
                        infectionsCaused++;
                        break;
                    case InfectionStatus.Infected:
                        direction = ChangeDirection(direction, false);
                        knownPoints[location] = InfectionStatus.Flagged;
                        break;
                    case InfectionStatus.Flagged:
                        direction = (Direction)((int)(direction + 2) % 4);
                        knownPoints[location] = InfectionStatus.Clean;
                        break;
                }

                location = location.GetNextPoint(direction);
            }

            return infectionsCaused;
        }

        public enum InfectionStatus
        {
            Clean = 0,
            Weakened = 1,
            Infected = 2,
            Flagged = 3
        }

        public static IList<Point> ParseInfected(string[] input)
        {
            var infectedPoints = new List<Point>();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                        infectedPoints.Add(new Point(x, y));
                }
            }

            return infectedPoints;
        }

        public static Direction ChangeDirection(Direction direction, bool goLeft)
        {
            var change = goLeft ? 1 : -1;
            var result = (direction + change);
            if ((int)result == -1)
                return (Direction)3;
            if ((int)result == 4)
                return (Direction)0;
            return result;
        }
    }
}
