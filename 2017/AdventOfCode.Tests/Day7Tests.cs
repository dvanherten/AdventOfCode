using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day7Tests
    {
        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day7.SolvePart1(GetPuzzleInput());
            Assert.Equal("aapssr", result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day7.SolvePart2(GetPuzzleInput());
            Assert.Equal(1458, result);
        }

        [Fact]
        public void ParseNoChildSuccessfully()
        {
            var input = "havc (66)";
            var program = Day7.Parse(input);
            Assert.Equal("havc", program.Name);
            Assert.Equal(66, program.Weight);
            Assert.Empty(program.ChildProgramNames);
        }

        [Fact]
        public void ParseWithChildSuccessfully()
        {
            var input = "fwft (72) -> ktlj, cntj, xhth";
            var program = Day7.Parse(input);
            Assert.Equal("fwft", program.Name);
            Assert.Equal(72, program.Weight);
            Assert.Equal(3, program?.ChildProgramNames.Length);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day7Input.txt");
        }
    }
}
