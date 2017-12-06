using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using AdventOfCode.Supporting;
using Microsoft.Build.Tasks;

namespace AdventOfCode
{
    public class Day6
    {
        public static int SolvePart1(string[] input)
        {
            var answer = Solve(input);
            return answer.Value;
        }

        public static int SolvePart2(string[] input)
        {
            var firstPass = Solve(input);
            var secondPass = Solve(firstPass.BlockSequence.Split(null));
            return secondPass.Value;
        }

        private static Answer Solve(string[] input)
        {
            var array = input.Select(int.Parse).ToArray();
            var counter = 0;
            var foundBlockSequences = new List<string>();

            while (counter < 100000)
            {
                var blockSequence = string.Join(" ", array);
                if (foundBlockSequences.Contains(blockSequence))
                    return new Answer{Value = counter, BlockSequence = blockSequence};
                foundBlockSequences.Add(blockSequence);

                var blockValue = FindBlockValueWithMostBlocks(array);
                array[blockValue.Index] = 0;
                for (int i = 1; i <= blockValue.Value; i++)
                {
                    var indexToUpdate = (blockValue.Index + i) % array.Length;
                    array[indexToUpdate] = array[indexToUpdate] + 1;
                }
                counter++;
            }
            throw new Exception("Shouldn't happen");
        }

        public class Answer
        {
            public int Value { get; set; }
            public string BlockSequence { get; set; }
        }

        private static BlockValue FindBlockValueWithMostBlocks(int[] array)
        {
            var max = array.Max();
            var index = array.ToList().IndexOf(max);
            return new BlockValue
            {
                Index = index,
                Value = max
            };
        }

        public class BlockValue
        {
            public int Index;
            public int Value;
        }
    }
}