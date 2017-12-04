using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using AdventOfCode.Supporting;
using Microsoft.Build.Tasks;

namespace AdventOfCode
{
    public class Day4
    {
        public static int SolvePart1(string[] input)
        {
            var counter = 0;
            foreach (var line in input)
            {
                var split = line.Split(null);
                if (split.Distinct().Count() == split.Length)
                    counter++;
            }
            return counter;
        }

        public static int SolvePart2(string[] input)
        {
            var counter = 0;
            foreach (var line in input)
            {
                var split = line.Split(null);
                if (split.Select(x => string.Concat(x.OrderBy(c => c))).Distinct().Count() == split.Length)
                    counter++;
            }
            return counter;
        }
    }
}