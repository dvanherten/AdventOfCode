using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Supporting;
using Microsoft.Build.Tasks;

namespace AdventOfCode
{
    public class Day17
    {

        public static int SolvePart1(int input)
        {
            var list = BuildList(input, 2017);
            var index = list.IndexOf(2017);
            return list[index + 1];
        }

        public static int SolvePart2(int input)
        {
            var list = BuildList(input, 50000000);
            var index = list.IndexOf(0);
            return list[index + 1];
        }

        public static List<int> BuildList(int input, int loops)
        {
            var list = new List<int> {0};
            var currentIndex = 0;

            for (var i = 1; i <= loops; i++)
            {
                // Move
                var spacesToMove = input % i;
                var newIndex = currentIndex + spacesToMove;
                currentIndex = newIndex >= list.Count
                    ? newIndex - list.Count
                    : newIndex;

                // Insert
                currentIndex++;
                list.Insert(currentIndex, i);
            }

            return list;
        }
    }
}
