using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day6Tests
    {
        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day6.SolvePart1(GetPuzzleInput());
            Assert.Equal(5042, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day6.SolvePart2(GetPuzzleInput());
            Assert.Equal(1086, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day6Input.txt")[0].Split(null);
        }
    }
}
