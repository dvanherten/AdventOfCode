using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Advent2019.Day10
{
    public class Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly string _input;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
            _input = File.ReadAllText("Day10/Input.txt");
        }

        [Fact]
        public void Part1()
        {
        }

        [Fact]
        public void Part2()
        {
        }
    }
}
