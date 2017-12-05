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
    public class Day5
    {
        public static int SolvePart1(string[] input)
        {
            var array = input.Select(int.Parse).ToArray();
            var counter = 0;
            var index = 0;
            try
            {
                while (true)
                {
                    var newIndex = index + array[index];
                    array[index] = array[index] + 1;
                    index = newIndex;
                    counter++;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            return counter;
        }

        public static int SolvePart2(string[] input)
        {
            var array = input.Select(int.Parse).ToArray();
            var counter = 0;
            var index = 0;
            try
            {
                while (true)
                {
                    var newIndex = index + array[index];
                    array[index] = array[index] >= 3 ? array[index] - 1 : array[index] + 1;
                    index = newIndex;
                    counter++;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            return counter;
        }
    }
}