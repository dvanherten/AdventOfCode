using System;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day1Tests
    {
        [Theory]
        [InlineData("1122", 3)]
        [InlineData("1111", 4)]
        [InlineData("1234", 0)]
        [InlineData("91212129", 9)]
        public void Part1Tests(string input, int expected)
        {
            var result = Day1.SolvePart1(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1212", 6)]
        [InlineData("1221", 0)]
        [InlineData("123425", 4)]
        [InlineData("123123", 12)]
        [InlineData("12131415", 4)]
        public void Part2Tests(string input, int expected)
        {
            var result = Day1.SolvePart2(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day1.SolvePart1(GetPuzzleInput());
            Assert.Equal(1223, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day1.SolvePart2(GetPuzzleInput());
            Assert.Equal(1284, result);
        }

        private string GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day1Input.txt").First();
        }
    }
}
