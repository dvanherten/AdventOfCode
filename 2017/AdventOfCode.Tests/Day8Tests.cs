using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day8Tests
    {
        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day8.SolvePart1(GetPuzzleInput());
            Assert.Equal(4888, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day8.SolvePart2(GetPuzzleInput());
            Assert.Equal(7774, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day8Input.txt");
        }
    }
}
