using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day9Tests
    {
        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day9.SolvePart1(GetPuzzleInput());
            Assert.Equal(9251, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day9.SolvePart2(GetPuzzleInput());
            Assert.Equal(-1, result);
        }

        private string GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day9Input.txt")[0];
        }
    }
}
