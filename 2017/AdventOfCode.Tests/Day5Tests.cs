using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day5Tests
    {
        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day5.SolvePart1(GetPuzzleInput());
            Assert.Equal(351282, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day5.SolvePart2(GetPuzzleInput());
            Assert.Equal(24568703, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day5Input.txt");
        }
    }
}
