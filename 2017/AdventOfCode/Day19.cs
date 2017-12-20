using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AdventOfCode.Supporting;
using Microsoft.Build.Tasks;

namespace AdventOfCode
{
    public class Day19
    {
        public static string SolvePart1(string[] input)
        {
            var xSize = input.First().Length;
            var ySize = input.Length;

            var point = new Point(input.First().IndexOf('|'), 0);
            var lettersFound = "";

            var direction = Direction.Down;

            while (true)
            {
                var currentChar = input[point.Y][point.X];
                
                if (Char.IsLetter(currentChar))
                    lettersFound += currentChar;
                else if (currentChar == '+')
                {
                    if (direction == Direction.Up || direction == Direction.Down)
                        direction = input[point.Y][point.X + 1] == '-' || Char.IsLetter(input[point.Y][point.X + 1]) ? Direction.Right : Direction.Left;
                    else
                        direction = input[point.Y + 1][point.X] == '|' || Char.IsLetter(input[point.Y + 1][point.X]) ? Direction.Down : Direction.Up;
                }
                else if (currentChar == ' ')
                    break;

                point = point.GetNextPoint(direction);
                if (point.X < 0 || point.X >= xSize || point.Y < 0 || point.Y >= ySize)
                    break;
            }

            return lettersFound;
        }

        public static int SolvePart2(string[] input)
        {
            var xSize = input.First().Length;
            var ySize = input.Length;

            var point = new Point(input.First().IndexOf('|'), 0);
            var steps = 0;

            var direction = Direction.Down;

            while (true)
            {
                var currentChar = input[point.Y][point.X];

                if (currentChar == '+')
                {
                    if (direction == Direction.Up || direction == Direction.Down)
                        direction = input[point.Y][point.X + 1] == '-' || Char.IsLetter(input[point.Y][point.X + 1]) ? Direction.Right : Direction.Left;
                    else
                        direction = input[point.Y + 1][point.X] == '|' || Char.IsLetter(input[point.Y + 1][point.X]) ? Direction.Down : Direction.Up;
                }
                else if (currentChar == ' ')
                    break;

                steps++;
                point = point.GetNextPoint(direction);
                if (point.X < 0 || point.X >= xSize || point.Y < 0 || point.Y >= ySize)
                    break;
            }

            return steps;
        }
    }
}
