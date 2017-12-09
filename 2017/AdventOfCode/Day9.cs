using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day9
    {
        public static int SolvePart1(string input)
        {
            var score = WalkStream(input);
            return score;
        }

        public static int SolvePart2(string input)
        {
            throw new NotImplementedException();
        }

        private static int WalkStream(string input)
        {
            var score = 0;
            var currentScoreValue = 0;
            var inGarbage = false;

            for (var i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '!':
                        i++; // Skip next input.
                        break;
                    case '<':
                        inGarbage = true;
                        break;
                    case '>':
                        inGarbage = false;
                        break;
                    case '{':
                        if (!inGarbage)
                            currentScoreValue++;
                        break;
                    case '}':
                        if (!inGarbage)
                        {
                            score += currentScoreValue;
                            currentScoreValue--;
                        }
                        break;
                }
            }

            return score;
        }
    }
}