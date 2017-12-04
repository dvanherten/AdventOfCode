using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day4Tests
    {
        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day4.SolvePart1(GetPuzzleInput());
            Assert.Equal(455, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day4.SolvePart2(GetPuzzleInput());
            Assert.Equal(186, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day4Input.txt");
        }
    }
}
