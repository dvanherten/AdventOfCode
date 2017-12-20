using System;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day19Tests
    {
        [Fact]
        public void Part1SamplePuzzle()
        {
            var input = GetSampleInput();
            var result = Day19.SolvePart1(input);
            Assert.Equal("ABCDEF", result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day19.SolvePart1(input);
            Assert.Equal("EPYDUXANIT", result);
        }
        
        [Fact]
        public void Part2RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day19.SolvePart2(input);
            Assert.Equal(17544, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day19Input.txt");
        }

        private string[] GetSampleInput()
        {
            return new[]
            {
                "     |          ",
                "     |  +--+    ",
                "     A  |  C    ",
                " F---|----E|--+ ",
                "     |  |  |  D ",
                "     +B-+  +--+ ",
                "                "
            };
        }
    }
}
