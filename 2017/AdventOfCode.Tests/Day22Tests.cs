using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day22Tests
    {
        [Fact]
        public void Part1SamplePuzzle()
        {
            var input = GetSampleInput();
            var result = Day22.SolvePart1(input);
            Assert.Equal(5587, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day22.SolvePart1(input);
            Assert.Equal(5256, result);
        }

        [Fact]
        public void Part2SamplePuzzle()
        {
            var input = GetSampleInput();
            var result = Day22.SolvePart2(input);
            Assert.Equal(2511944, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day22.SolvePart2(input);
            Assert.Equal(2511345, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day22Input.txt");
        }

        private string[] GetSampleInput()
        {
            return new[]
            {
                "..#",
                "#..",
                "..."
            };
        }
    }
}
