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
            var value = HandleSpinLock(input, 2017);
            return value;
        }

        public static int SolvePart2(int input)
        {
            var value = HandleSpinLockPart2(input, 50000000);
            return value;
        }

        public static int HandleSpinLock(int input, int loops)
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

            var index = list.IndexOf(2017);
            return list[index + 1];
        }

        public static int HandleSpinLockPart2(int input, int loops)
        {
            var currentIndex = 0;
            var indexOfValue = 0;
            var valueWeCareAbout = 0;

            for (var i = 1; i <= loops; i++)
            {
                // Move
                var spacesToMove = input % i;
                var newIndex = currentIndex + spacesToMove;
                currentIndex = newIndex >= i
                    ? newIndex - i
                    : newIndex;

                // Insert
                currentIndex++;
                if (currentIndex <= indexOfValue)
                    indexOfValue++;
                if (currentIndex == indexOfValue + 1)
                    valueWeCareAbout = i;
            }

            return valueWeCareAbout;
        }
    }
}
