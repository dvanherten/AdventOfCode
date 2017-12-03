using System;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day2Tests
    {
        [Theory]
        [InlineData("5 1 9 5", 8)]
        [InlineData("7 5 3", 4)]
        [InlineData("2 4 6 8", 6)]
        public void Part1LineTests(string input, int expected)
        {
            var result = Day2.Part1LineLogic(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1FullTest()
        {
            var input = new[] { "5 1 9 5", "7 5 3", "2 4 6 8" };
            var result = Day2.Solve(input, Day2.Part1LineLogic);
            Assert.Equal(18, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day2.Solve(GetPuzzleInput(), Day2.Part1LineLogic);
            Assert.Equal(47136, result);
        }

        [Theory]
        [InlineData("5 9 2 8", 4)]
        [InlineData("9 4 7 3", 3)]
        [InlineData("3 8 6 5", 2)]
        public void Part2LineTests(string input, int expected)
        {
            var result = Day2.Part2LineLogic(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2FullTest()
        {
            var input = new[] { "5 9 2 8", "9 4 7 3", "3 8 6 5" };
            var result = Day2.Solve(input, Day2.Part2LineLogic);
            Assert.Equal(9, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day2.Solve(GetPuzzleInput(), Day2.Part2LineLogic);
            Assert.Equal(250, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day2Input.txt");
        }
    }
}
