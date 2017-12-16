using System;
using System.IO;
using System.Linq;
using AdventOfCode.Supporting;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day15Tests
    {
        [Fact]
        public void Part1GeneratorTest()
        {
            var generator = new Day15.Generator(65, 16807);
            generator.GenerateNewValue();
            Assert.Equal(1092455, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(1181022009, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(245556042, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(1744312007, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(1352636452, generator.Value);
        }

        [Fact]
        public void Part2GeneratorATest()
        {
            var generator = new Day15.Generator(65, Day15.FactorA, x => x % 4 == 0);
            generator.GenerateNewValue();
            Assert.Equal(1352636452, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(1992081072, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(530830436, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(1980017072, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(740335192, generator.Value);
        }

        [Fact]
        public void Part2GeneratorBTest()
        {
            var generator = new Day15.Generator(8921, Day15.FactorB, x => x % 8 == 0);
            generator.GenerateNewValue();
            Assert.Equal(1233683848, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(862516352, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(1159784568, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(1616057672, generator.Value);
            generator.GenerateNewValue();
            Assert.Equal(412269392, generator.Value);
        }

        [Fact]
        public void Part1SamplePuzzle()
        {
            var result = Day15.SolvePart1(65, 8921, 40000000);
            Assert.Equal(588, result);
        }

        [Fact]
        public void Part1RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day15.SolvePart1(input.SeedA, input.SeedB, 40000000);
            Assert.Equal(569, result);
        }

        [Fact]
        public void Part2SamplePuzzle()
        {
            var result = Day15.SolvePart2(65, 8921, 5000000);
            Assert.Equal(309, result);
        }

        [Fact]
        public void Part2RealPuzzle()
        {
            var input = GetPuzzleInput();
            var result = Day15.SolvePart2(input.SeedA, input.SeedB, 5000000);
            Assert.Equal(298, result);
        }

        private (int SeedA, int SeedB) GetPuzzleInput()
        {
            return (116, 299);
        }
    }
}
