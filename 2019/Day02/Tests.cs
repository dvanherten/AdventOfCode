using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Advent2019.Day02
{
    public class Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly int[] _input;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
            _input = File.ReadAllText("Day02/Input.txt").Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        }

        [Fact]
        public void Part1()
        {
            // Adjust input
            _input[1] = 12;
            _input[2] = 2;

            var result = RunProgram(_input);

            _output.WriteLine(result[0].ToString());
            Assert.Equal(9581917, result[0]);
        }

        [Fact]
        public void Part2()
        {
            var found = false;
            for (var noun = 0; noun <= 99; noun++)
            {
                for (var verb = 0; verb <= 99; verb++)
                {
                    var input = (int[])_input.Clone();
                    input[1] = noun;
                    input[2] = verb;
                    var result = RunProgram(input);
                    if (result[0] == 19690720)
                    {
                        found = true;
                        _output.WriteLine(noun.ToString());
                        _output.WriteLine(verb.ToString());
                        _output.WriteLine((100 * noun + verb).ToString());

                        Assert.Equal(2505, 100 * noun + verb);
                        break;
                    }
                }

                if (found)
                    break;
            }
        }

        private static int[] RunProgram(int[] input)
        {
            var computer = new IntCodeComputer();
            var result = computer.RunProgram(input);
            return result;
        }
    }
}
