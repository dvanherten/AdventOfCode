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
            var programs = input.Select(Parse).ToArray();
            
            // I know I should probably build a graph here, but yolo.

            var programsToProcess = programs.ToArray();
            while (true)
            {
                var skippedPrograms = new List<TowerProgram>();

                foreach (var program in programsToProcess)
                {
                    if (!program.CalculateWeight(programs))
                    {
                        skippedPrograms.Add(program);
                    }
                }

                if (programs.All(x => x.WeightTotal != -1))
                {
                    break;
                }

                programsToProcess = skippedPrograms.ToArray();
            }

            var unbalancedProgram = programs.Where(x => x.IsUnbalanced()).Single(x => x.ChildPrograms.All(y => !y.IsUnbalanced()));
            var childWeightGroupings = unbalancedProgram.ChildPrograms.GroupBy(x => x.WeightTotal).ToArray();
            var problemProgram = childWeightGroupings.Where(x => x.Count() == 1).SelectMany(x => x).Single();
            var expectedWeight = childWeightGroupings.Where(x => x.Count() > 1).SelectMany(x => x).First().WeightTotal;
            var diff = problemProgram.WeightTotal - expectedWeight;
            return problemProgram.Weight - diff;
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
            public TowerProgram[] ChildPrograms { get; set; }

            public int WeightTotal { get; private set; } = -1;
            public bool CalculateWeight(TowerProgram[] programs)
            {
                if (ChildPrograms == null)
                    ChildPrograms = programs.Where(x => ChildProgramNames.Contains(x.Name)).ToArray();

                if (ChildPrograms.Any(x => x.WeightTotal == -1))
                    return false;

                WeightTotal = ChildPrograms.Sum(x => x.WeightTotal) + Weight;
                return true;
            }

            public bool IsUnbalanced()
            {
                return ChildPrograms.Select(x => x.WeightTotal).Distinct().Count() > 1;
            }
        }
    }
}