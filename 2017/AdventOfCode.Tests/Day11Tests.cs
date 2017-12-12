using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day11Tests
    {
        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 0)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void Part1Sample(string input, int expectedSteps)
        {
            var distance = Day11.SolvePart1(input);
            Assert.Equal(expectedSteps, distance);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day11.SolvePart1(GetPuzzleInput());
            Assert.Equal(812, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day11.SolvePart2(GetPuzzleInput());
            Assert.Equal(1603, result);
        }

        private string GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day11Input.txt")[0];
        }
    }
}
