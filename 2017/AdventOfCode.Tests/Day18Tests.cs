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
            var input = GetPart1SampleInput();
            var result = Day18.SolvePart1(input);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day18.SolvePart1(input);
            Assert.Equal(1187, result);
        }

        [Fact]
        public void Part2SamplePuzzle()
        {
            var input = GetPart2SampleInput();
            var result = Day18.SolvePart2(input);
            Assert.Equal(3, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day18.SolvePart2(input);
            Assert.Equal(5969, result);
        }

        private string[] GetPuzzleInput()
        {
            return File.ReadAllLines(@"Data\Day18Input.txt");
        }

        private string[] GetPart1SampleInput()
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

        private string[] GetPart2SampleInput()
        {
            return new[]
            {
                "snd 1",
                "snd 2",
                "snd p",
                "rcv a",
                "rcv b",
                "rcv c",
                "rcv d"
            };
        }
    }
}
