using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day14Tests
    {
        public void Part1SamplePuzzle()
        {
            var result = Day14.SolvePart1("flqrgnkx");
            Assert.Equal(8108, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day14.SolvePart1(GetPuzzleInput());
            Assert.Equal(8250, result);
        }

        [Fact(Skip = "Takes too long. :(")]
        public void Part2SamplePuzzle()
        {
            var result = Day14.SolvePart2("flqrgnkx");
            Assert.Equal(1242, result);
        }

        [Fact(Skip = "Takes too long. :(")]
        public void Part2RealPuzzle()
        {
            var result = Day14.SolvePart2(GetPuzzleInput());
            Assert.Equal(-1, result);
        }

        private string GetPuzzleInput()
        {
            return "stpzcrnm";
        }
    }
}
