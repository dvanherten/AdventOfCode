using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day12Tests
    {
        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day12.SolvePart1(GetPuzzleInput());
            Assert.Equal(380, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day12.SolvePart2(GetPuzzleInput());
            Assert.Equal(181, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day12Input.txt");
        }
    }
}
