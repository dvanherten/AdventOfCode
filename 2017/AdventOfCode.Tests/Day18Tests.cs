using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day18Tests
    {
        [Fact]
        public void Part1SamplePuzzle()
        {
            var input = GetSampleInput();
            var result = Day18.SolvePart1(input);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day18.SolvePart1(input);
            Assert.Equal(-1, result);
        }
        
        [Fact]
        public void Part2RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day18.SolvePart2(input);
            Assert.Equal(-1, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day18Input.txt");
        }

        private string[] GetSampleInput()
        {
            return new[]
            {
                "set a 1",
                "add a 2",
                "mul a a",
                "mod a 5",
                "snd a",
                "set a 0",
                "rcv a",
                "jgz a -1",
                "set a 1",
                "jgz a -2"
            };
        }
    }
}
