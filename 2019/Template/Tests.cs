using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Advent2019.Template
{
    public class Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly string _input;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
            _input = File.ReadAllText("Day##/Input.txt");
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
