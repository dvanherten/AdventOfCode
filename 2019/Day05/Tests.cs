using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Advent2019.Day05
{
    public class Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly int[] _input;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
            _input = File.ReadAllText("Day05/Input.txt").Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        }

        [Fact]
        public void Part1()
        {
            var computer = new IntCodeComputer();
            OpCode3.Input = 1;
            var outputOpCode = new OpCode4 {LogOutput = x => { _output.WriteLine(x.ToString()); }};
            computer.Register(outputOpCode);
            var result = computer.RunProgram(_input);
        }

        [Fact]
        public void Part2()
        {
            var computer = new IntCodeComputer();
            OpCode3.Input = 5;
            var outputOpCode = new OpCode4 { LogOutput = x => { _output.WriteLine(x.ToString()); } };
            computer.Register(outputOpCode);
            var result = computer.RunProgram(_input);
        }
    }
}
