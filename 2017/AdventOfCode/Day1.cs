using System;

namespace AdventOfCode
{
    public static class Day1
    {
        public static int SolvePart1(string input)
        {
            var extendedInput = input + input[0];
            var sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (extendedInput[i] == extendedInput[i + 1])
                    sum += int.Parse(extendedInput[i].ToString());
            }
            return sum;
        }

        public static int SolvePart2(string input)
        {
            var halfWayPoint = input.Length / 2;
            var extendedInput = input + input.Substring(0, halfWayPoint);
            var sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (extendedInput[i] == extendedInput[i + halfWayPoint])
                    sum += int.Parse(extendedInput[i].ToString());
            }
            return sum;
        }
    }
}
