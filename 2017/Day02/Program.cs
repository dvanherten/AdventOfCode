using System;
using System.IO;
using System.Linq;

namespace Day02
{
    class Program
    {
        static string[] _testInputPart1 = { "5 1 9 5", "7 5 3", "2 4 6 8" };
        static string[] _testInputPart2 = { "5 9 2 8", "9 4 7 3", "3 8 6 5" };

        static void Main(string[] args)
        {
            Console.WriteLine("Day 2 - Part 1");
            Console.WriteLine("--------------");
            Console.WriteLine(SolvePart1(_testInputPart1));
            Console.WriteLine(SolvePart1(File.ReadAllLines("input.txt")));

            Console.WriteLine();
            Console.WriteLine("Day 2 - Part 2");
            Console.WriteLine("--------------");
            Console.WriteLine(SolvePart2(_testInputPart2));
            Console.WriteLine(SolvePart2(File.ReadAllLines("input.txt")));

        }

        static int SolvePart1(string[] input)
        {
            return input.Sum(x =>
            {
                var split = x.Split(null).Select(int.Parse).ToArray();
                return split.Max() - split.Min();
            });
        }

        static int SolvePart2(string[] input)
        {
            return input.Sum(x =>
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
            });
        }
    }
}
