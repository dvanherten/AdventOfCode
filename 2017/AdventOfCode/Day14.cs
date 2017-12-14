using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AdventOfCode.Supporting;

namespace AdventOfCode
{
    public class Day14
    {
        public static int SolvePart1(string input)
        {
            var squaresUsed = 0;
            for (var i = 0; i < 128; i++)
            {
                var knotHash = Day10.SolvePart2($"{input}-{i}", 256);
                var binaryString = String.Join("", knotHash.Select(x => Convert.ToString(Convert.ToInt32(x.ToString(), 16), 2).PadLeft(4, '0')));
                squaresUsed += binaryString.Replace("0", "").Length;
            }
            return squaresUsed;
        }

        public static int SolvePart2(string input)
        {
            var squares = new List<DiskSquare>();
            for (var i = 0; i < 128; i++)
            {
                var knotHash = Day10.SolvePart2($"{input}-{i}", 256);
                var binaryString = String.Join("", knotHash.Select(x => Convert.ToString(Convert.ToInt32(x.ToString(), 16), 2).PadLeft(4, '0')));
                for (int j = 0; j < binaryString.Length; j++)
                    squares.Add(new DiskSquare {Point = new Point(j, i), Used = binaryString[j] == '1'});
            }

            var currentRegionNumber = 0;
            foreach (var square in squares)
            {
                if (square.Used && square.RegionNumber == null)
                {
                    var queue = new Queue<DiskSquare>(new [] {square});
                    while (queue.Count != 0)
                    {
                        var queueSquare = queue.Dequeue();
                        var neighbours = squares.Where(x =>
                            x.Used && Day3.CalculateManhattanDistance(queueSquare.Point, x.Point) == 1).ToArray();

                        var neighbourRegionNumber = neighbours.Where(x => x.RegionNumber.HasValue)
                            .Select(x => x.RegionNumber).FirstOrDefault();

                        queueSquare.RegionNumber = neighbourRegionNumber ?? ++currentRegionNumber;
                        foreach (var neighbour in neighbours.Where(x => !x.RegionNumber.HasValue))
                            queue.Enqueue(neighbour);
                    }
                }
            }
            return currentRegionNumber;
        }


        [DebuggerDisplay("X = {Point.X}, Y = {Point.Y}, Used = {Used}, RegionNumber = {RegionNumber}")]
        public class DiskSquare
        {
            public Point Point { get; set; }
            public bool Used { get; set; }
            public int? RegionNumber { get; set; }
        }
    }
}
