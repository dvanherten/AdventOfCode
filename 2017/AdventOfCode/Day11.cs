using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day11
    {
        public static int SolvePart1(string input)
        {
            return Solve(input).DistanceFromStart;
        }

        public static int SolvePart2(string input)
        {
            return Solve(input).FurthestDistance;
        }

        public static (int DistanceFromStart, int FurthestDistance) Solve(string input)
        {
            var path = input.Split(',');
            var start = new HexGridLocation();
            var location = new HexGridLocation();
            var furthestDistanceFromStart = 0;
            var currentDistance = 0;
            foreach (var move in path)
            {
                if (move == "n")
                    location.MoveNorth();
                else if (move == "ne")
                    location.MoveNorthEast();
                else if (move == "nw")
                    location.MoveNorthWest();
                else if (move == "s")
                    location.MoveSouth();
                else if (move == "se")
                    location.MoveSouthEast();
                else if (move == "sw")
                    location.MoveSouthWest();

                currentDistance = location.DistanceFrom(start);
                if (currentDistance > furthestDistanceFromStart)
                    furthestDistanceFromStart = currentDistance;
            }

            return (currentDistance, furthestDistanceFromStart);
        }
    }

    // https://www.redblobgames.com/grids/hexagons/
    public class HexGridLocation
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public void MoveNorth()
        {
            Y += 1;
            Z -= 1;
        }
        public void MoveNorthEast()
        {
            X += 1;
            Z -= 1;
        }
        public void MoveNorthWest()
        {
            Y += 1;
            X -= 1;
        }
        public void MoveSouth()
        {
            Z += 1;
            Y -= 1;
        }
        public void MoveSouthEast()
        {
            X += 1;
            Y -= 1;
        }
        public void MoveSouthWest()
        {
            Z += 1;
            X -= 1;
        }

        public int DistanceFrom(HexGridLocation location)
        {
            return (Math.Abs(X - location.X) + Math.Abs(Y - location.Y) + Math.Abs(Z - location.Z)) / 2;
        }
    }
}