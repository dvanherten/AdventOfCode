using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day16Tests
    {
        [Fact]
        public void Part1Sample()
        {
            var dancers = new Day16.ArrayThatDancesWoooo("abcde");
            dancers.Dance("s1");
            Assert.Equal("eabcd", dancers.ToString());
            dancers.Dance("x3/4");
            Assert.Equal("eabdc", dancers.ToString());
            dancers.Dance("pe/b");
            Assert.Equal("baedc", dancers.ToString());
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day16.SolvePart1(input);
            Assert.Equal("namdgkbhifpceloj", result);
        }

        [Fact]
        public void Part2Sample()
        {
            var input = "s1,x3/4,pe/b";
            var result = Day16.SolvePart2(input, 2, "abcde");
            Assert.Equal("ceadb", result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day16.SolvePart2(input, 1000000000);
            Assert.Equal("ibmchklnofjpdeag", result);
        }

        private string GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day16Input.txt")[0];
        }
    }
}
