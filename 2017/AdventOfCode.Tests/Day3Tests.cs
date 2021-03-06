﻿
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day3Tests
    {
        [Fact]
        public void ManhattanTest()
        {
            var starting = new Point(1,3);
            var finishing = new Point(4,3);
            Assert.Equal(3, Day3.CalculateManhattanDistance(starting, finishing));
        }

        [Theory]
        [InlineData(3, 3)]
        [InlineData(4, 3)]
        [InlineData(8, 3)]
        [InlineData(9, 3)]
        [InlineData(11, 5)]
        [InlineData(16, 5)]
        [InlineData(19, 5)]
        [InlineData(25, 5)]
        public void CalculateGridSize(int input, int expected)
        {
            var result = Day3.CalculateGridSize(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FindPointTest()
        {
            var result = Day3.FindPointForNumber(16);
            Assert.Equal(new Point(2, 5), result);

            result = Day3.FindPointForNumber(10);
            Assert.Equal(new Point(5, 2), result);

            result = Day3.FindPointForNumber(25);
            Assert.Equal(new Point(5, 1), result);

            result = Day3.FindPointForNumber(9);
            Assert.Equal(new Point(3, 1), result);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(12, 3)]
        [InlineData(23, 2)]
        [InlineData(1024, 31)]
        public void Part1Tests(int input, int expected)
        {
            var result = Day3.SolvePart1(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(6, 10)]
        [InlineData(800, 806)]
        public void Part2Tests(int input, int expected)
        {
            var result = Day3.SolvePart2(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var result = Day3.SolvePart1(GetPuzzleInput());
            Assert.Equal(371, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var result = Day3.SolvePart2(GetPuzzleInput());
            Assert.Equal(369601, result);
        }

        private int GetPuzzleInput()
        {
            return 368078;
        }
    }
}
