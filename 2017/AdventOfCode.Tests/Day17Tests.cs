using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day17Tests
    {
        [Fact]
        public void Part1Sample()
        {
            var result = Day17.SolvePart1(3);
            Assert.Equal(638, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day17.SolvePart1(input);
            Assert.Equal(1487, result);
        }
        
        [Fact]
        public void Part2RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day17.SolvePart2(input);
            Assert.Equal(25674054, result);
        }

        private int GetPuzzleInput()
        {
            return 367;
        }
    }
}
