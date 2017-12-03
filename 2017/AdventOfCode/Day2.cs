using System;
using System.Linq;

namespace AdventOfCode
{
    public static class Day2
    {
        public static int Solve(string[] input, Func<string, int> lineLogic)
        {
            return input.Sum(lineLogic);
        }

        public static Func<string, int> Part1LineLogic = x =>
        {
            var split = x.Split(null).Select(int.Parse).ToArray();
            return split.Max() - split.Min();
        };

        public static Func<string, int> Part2LineLogic = x =>
        {
            var ordered = x.Split(null)
                .Select(int.Parse)
                .OrderByDescending(y => y)
                .ToArray();

            for (var i = 0; i < ordered.Length; i++)
            {
                for (var j = ordered.Length - 1; j > i; j--)
                {
                    if (ordered[i] % ordered[j] == 0)
                    {
                        return ordered[i] / ordered[j];
                    }
                }
            }
            throw new Exception("Something went horribly horribly wrong.");
        };
    }
}
