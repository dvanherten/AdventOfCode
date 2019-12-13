using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Xunit;
using Xunit.Abstractions;

namespace Advent2019.Day03
{
    public class Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly string[][] _input;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
            _input = File.ReadAllText("Day03/Input.txt").Split(Environment.NewLine).Select(x => x.Split(',')).ToArray();
        }

        [Fact]
        public void Part1()
        {
            var firstWireLocations = GetWireLocations(_input[0]);
            var secondWireLocations = GetWireLocations(_input[1]);
            firstWireLocations.IntersectWith(secondWireLocations);
            var distance = Int32.MaxValue;
            foreach (var collision in firstWireLocations)
                distance = Math.Min(distance, CalculateManhattanDistance(new HashPoint(), collision));
            _output.WriteLine(distance.ToString());
        }

        [Fact]
        public void Part2()
        {
            var firstWireLocations = GetWireLocations(_input[0]);
            var secondWireLocations = GetWireLocations(_input[1]);
            firstWireLocations.IntersectWith(secondWireLocations);

            var steps = Int32.MaxValue;
            foreach (var collision in firstWireLocations)
                steps = Math.Min(steps, collision.Steps + secondWireLocations.First(x => x.Equals(collision)).Steps);
            _output.WriteLine(steps.ToString());
        }

        public static HashSet<HashPoint> GetWireLocations(string[] paths)
        {
            var hashset = new HashSet<HashPoint>();
            var location = (X:0, Y:0);
            var steps = 1;
            foreach (var path in paths)
            {
                var direction = path[0];
                var distance = Convert.ToInt32(path.Substring(1));
                for (var i = 0; i < distance; i++)
                {
                    if (direction == 'U')
                        location.Y++;
                    if (direction == 'R')
                        location.X++;
                    if (direction == 'D')
                        location.Y--;
                    if (direction == 'L')
                        location.X--;
                    hashset.Add(new HashPoint{X = location.X, Y = location.Y, Steps = steps++});
                }
            }
            return hashset;
        }

        public class HashPoint
        {
            public int X;
            public int Y;
            public int Steps;

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = X.GetHashCode();
                    hashCode = (hashCode * 397) ^ Y.GetHashCode();
                    return hashCode;
                }
            }

            public override bool Equals(object obj)
            {
                return X == ((HashPoint)obj).X && Y == ((HashPoint)obj).Y;
            }
        }

        public static int CalculateManhattanDistance(HashPoint starting, HashPoint destination)
        {
            // https://en.wikipedia.org/wiki/Taxicab_geometry
            return Math.Abs(starting.X - destination.X) + Math.Abs(starting.Y - destination.Y);
        }

    }
}
