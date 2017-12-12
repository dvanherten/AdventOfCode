using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode
{
    public class Day12
    {
        public static Regex ParseRegex = new Regex(@"^(\d+) <-> (.*)");
        public static int SolvePart1(string[] input)
        {
            var dictionary = new Dictionary<int, int[]>();
            foreach (var programFlow in input)
            {
                var parsed = ParseRegex.Match(programFlow);
                dictionary.Add(int.Parse(parsed.Groups[1].ToString()), parsed.Groups[2].ToString().Replace(" ", "").Split(',').Select(int.Parse).ToArray());
            }
            var queue = new Queue<int>();
            var seen = new List<int> {0};
            queue.Enqueue(0);
            while (queue.Count != 0)
            {
                var programNum = queue.Dequeue();
                var ints = dictionary[programNum];
                foreach (var value in ints)
                {
                    if (!seen.Contains(value))
                    {
                        seen.Add(value);
                        queue.Enqueue(value);
                    }
                }
            }

            return seen.Count();
        }

        public static int SolvePart2(string[] input)
        {
            var dictionary = new Dictionary<int, int[]>();
            foreach (var programFlow in input)
            {
                var parsed = ParseRegex.Match(programFlow);
                dictionary.Add(int.Parse(parsed.Groups[1].ToString()), parsed.Groups[2].ToString().Replace(" ", "").Split(',').Select(int.Parse).ToArray());
            }

            var seen = new List<int> { };
            var groups = 0;
            while (seen.Count < dictionary.Count())
            {
                var queue = new Queue<int>();
                var first = dictionary.Keys.Except(seen).First();
                queue.Enqueue(first);
                while (queue.Count != 0)
                {
                    var programNum = queue.Dequeue();
                    var ints = dictionary[programNum];
                    foreach (var value in ints)
                    {
                        if (!seen.Contains(value))
                        {
                            seen.Add(value);
                            queue.Enqueue(value);
                        }
                    }
                }
                groups++;
            }

            return groups;
        }

        public static (int Answer1, int Answer2) Solve(string[] input)
        {
            

            return (0, 0);
        }

        public class ProgramFlow
        {
            
        }
    }
}