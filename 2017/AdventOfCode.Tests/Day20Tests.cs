using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day20Tests
    {
        [Fact]
        public void Part1SamplePuzzle()
        {
            var input = GetSampleInput();
            var result = Day20.SolvePart1(input);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day20.SolvePart1(input);
            Assert.Equal(161, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day20.SolvePart2(input);
            Assert.Equal(438, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day20Input.txt");
        }

        private string[] GetSampleInput()
        {
            return new[]
            {
                "p=<3,0,0>, v=<2,0,0>, a=<-1,0,0>",
                "p=<4,0,0>, v=<0,0,0>, a=<-2,0,0>"
            };
        }
    }
}
