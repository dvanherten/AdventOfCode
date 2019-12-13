using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Advent2019.Day04
{
    public class Tests
    {
        private readonly ITestOutputHelper _output;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Part1()
        {
            var low = 372304;
            var high = 847060;
            var validPasswords = 0;
            for (var password = low; password <= high; password++)
            {
                if (IsValidPasswordPart1(password.ToString()))
                    validPasswords++;
            }
            _output.WriteLine(validPasswords.ToString());
        }

        [Fact]
        public void Part2()
        {
            var low = 372304;
            var high = 847060;
            var validPasswords = 0;
            for (var password = low; password <= high; password++)
            {
                if (IsValidPasswordPart2(password.ToString()))
                    validPasswords++;
            }
            _output.WriteLine(validPasswords.ToString());
        }

        public static bool IsValidPasswordPart1(string password)
        {
            var hasDouble = false;
            var neverDecreases = true;
            var bytes = Encoding.ASCII.GetBytes(password);
            for (var i = 0; i < bytes.Length - 1; i++)
            {
                if (!hasDouble)
                    hasDouble = bytes[i] == bytes[i + 1];
                if (neverDecreases)
                    neverDecreases = bytes[i] <= bytes[i + 1];
            }
            return hasDouble && neverDecreases;
        }

        [Theory]
        [InlineData("112233", true)]
        [InlineData("123444", false)]
        [InlineData("111122", true)]
        public void Part2Tests(string password, bool expected)
        {
            Assert.Equal(expected, IsValidPasswordPart2(password));
        }

        public static bool IsValidPasswordPart2(string password)
        {
            var neverDecreases = true;
            var bytes = Encoding.ASCII.GetBytes(password);

            var hasDoubleGroups = bytes
                .GroupBy(l => l);
            var hasDouble = hasDoubleGroups.Select(g => new
            {
                Value = g.Key,
                Count = g.Count()
            }).Any(x => x.Count == 2);
            
            for (var i = 0; i < bytes.Length - 1; i++)
            {
                if (neverDecreases)
                    neverDecreases = bytes[i] <= bytes[i + 1];
            }
            return hasDouble && neverDecreases;
        }
    }
}
