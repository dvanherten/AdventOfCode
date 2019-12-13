using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Advent2019.Day01
{
    public class Tests
    {
        private readonly ITestOutputHelper _output;
        private readonly string[] _input;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
            _input = File.ReadAllText("Day01/Input.txt").Split(Environment.NewLine);
        }

        [Fact]
        public void Part1()
        {
            var totalFuel = _input.Select(x => GetFuel(Convert.ToInt32(x))).Sum();
            _output.WriteLine(totalFuel.ToString());
        }

        [Fact]
        public void Part2()
        {
            var totalFuel = _input.Select(x => GetFuelForMassAndFuel(Convert.ToInt32(x))).Sum();
            _output.WriteLine(totalFuel.ToString());
        }

        [Fact]
        public void Part2Test()
        {
            var mass = 100756;
            var fuel = GetFuelForMassAndFuel(mass);
            Assert.Equal(50346, fuel);
        }

        private int GetFuel(int mass)
        {
            return mass / 3 - 2;
        }

        private int GetFuelForMassAndFuel(int mass)
        {
            var fuel = GetFuel(mass);
            if (fuel > 0)
                fuel += Math.Max(GetFuelForMassAndFuel(fuel), 0);
            return fuel;
        }
    }
}
