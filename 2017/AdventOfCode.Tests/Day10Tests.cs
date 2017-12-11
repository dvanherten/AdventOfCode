using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day10Tests
    {
        [Fact]
        public void Part1SampleData()
        {
            var result = Day10.SolvePart1("3,4,1,5".Split(','), 5);
            Assert.Equal(12, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day10.SolvePart1(GetPuzzleInput().Split(','), 256);
            Assert.Equal(52070, result);
        }

        [Theory]
        [InlineData("", "a2582a3a0e66e6e86e3812dcb672a272")]
        [InlineData("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd")]
        [InlineData("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d")]
        [InlineData("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e")]
        public void Part2SampleData(string input, string expected)
        {
            var result = Day10.SolvePart2(input, 256);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day10.SolvePart2(GetPuzzleInput(), 256);
            Assert.Equal("7f94112db4e32e19cf6502073c66f9bb", result);
        }

        private string GetPuzzleInput()
        {
            return "46,41,212,83,1,255,157,65,139,52,39,254,2,86,0,204";
        }
    }
}
