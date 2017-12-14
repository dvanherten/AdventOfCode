using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day13Tests
    {
        [Fact]
        public void Part1Sample()
        {
            var puzzleInput = new[] {"0: 3", "1: 2", "4: 4", "6: 4"};
            var result = Day13.SolvePart1(puzzleInput);
            Assert.Equal(24, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day13.SolvePart1(GetPuzzleInput());
            Assert.Equal(2160, result);
        }

        [Fact]
        public void Part2Sample()
        {
            var puzzleInput = new[] { "0: 3", "1: 2", "4: 4", "6: 4" };
            var result = Day13.SolvePart2(puzzleInput);
            Assert.Equal(10, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day13.SolvePart2(GetPuzzleInput());
            Assert.Equal(3907470, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day13Input.txt");
        }
    }
}
