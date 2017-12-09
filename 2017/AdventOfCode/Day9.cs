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
            var answer = WalkStream(input);
            return answer.Score;
        }

        public static int SolvePart2(string input)
        {
            var answer = WalkStream(input);
            return answer.GarbageCount;
        }

        private static (int Score, int GarbageCount) WalkStream(string input)
        {
            var score = 0;
            var currentScoreValue = 0;
            var inGarbage = false;
            var garbageCount = 0;

            for (var i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '!':
                        i++; // Skip next input.
                        break;
                    case '<':
                        if (inGarbage)
                            garbageCount++;
                        else
                            inGarbage = true;
                        break;
                    case '>':
                        inGarbage = false;
                        break;
                    case '{':
                        if (inGarbage)
                            garbageCount++;
                        else
                            currentScoreValue++;
                        break;
                    case '}':
                        if (inGarbage)
                            garbageCount++;
                        else
                        { 
                            score += currentScoreValue;
                            currentScoreValue--;
                        }
                        break;
                    default:
                        if (inGarbage)
                            garbageCount++;
                        break;
                }
            }

            return (score, garbageCount);
        }
    }
}