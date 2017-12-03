using System;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day2Tests
    {
        [Fact]
        public void Part1Tests()
        {
            var input = new[] { "5 1 9 5", "7 5 3", "2 4 6 8" };
            var result = Day2.SolvePart1(input);
            Assert.Equal(18, result);
        }

        [Fact]
        public void Part2Tests()
        {
            var input = new[] { "5 9 2 8", "9 4 7 3", "3 8 6 5" };
            var result = Day2.SolvePart2(input);
            Assert.Equal(9, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day2.SolvePart1(GetPuzzleInput());
            Assert.Equal(47136, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day2.SolvePart2(GetPuzzleInput());
            Assert.Equal(250, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day2Input.txt");
        }
    }
}
