using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Supporting;
using Microsoft.Build.Tasks;

namespace AdventOfCode
{
    public class Day7
    {
        public static string SolvePart1(string[] input)
        {
            var programs = input.Select(Parse).ToArray();
            return programs.Single(x => !programs.SelectMany(y => y.ChildProgramNames).Contains(x.Name)).Name;
        }

        public static int SolvePart2(string[] input)
        {
            throw new NotImplementedException();
        }

        public static Regex ParseRegex = new Regex(@"^(\w+) \((\d+)\)( -> (.*))?");
        public static TowerProgram Parse(string input)
        {
            var match = ParseRegex.Match(input);
            return new TowerProgram
            {
                Name = match.Groups[1].Value,
                Weight = int.Parse(match.Groups[2].Value),
                ChildProgramNames = match.Groups[4].Value.Replace(" ", "").Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray()
            }; 
        }

        public class TowerProgram
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public string[] ChildProgramNames { get; set; } = new string[0];
        }
    }
}