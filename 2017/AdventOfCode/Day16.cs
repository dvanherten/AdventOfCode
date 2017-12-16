using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Supporting;
using Microsoft.Build.Tasks;

namespace AdventOfCode
{
    public class Day16
    {

        public static string SolvePart1(string danceMoves, string start = "abcdefghijklmnop")
        {
            var dancers = new ArrayThatDancesWoooo(start);
            foreach (var move in danceMoves.Split(','))
                dancers.Dance(move);
            return dancers.ToString();
        }

        public static string SolvePart2(string danceMoves, int cycles, string start = "abcdefghijklmnop")
        {
            var dancers = new ArrayThatDancesWoooo(start);
            var counter = 0;
            do
            {
                foreach (var move in danceMoves.Split(','))
                    dancers.Dance(move);

                counter++;
            } while (dancers.ToString() != start && counter < cycles);

            // Completed cycles? We're done.
            if (counter == cycles)
                return dancers.ToString();

            // Back at the beginning? The answer is in the remainder.
            var cyclesLeft = cycles % counter;
            for (var i = 0; i < cyclesLeft; i++)
                foreach (var move in danceMoves.Split(','))
                    dancers.Dance(move);

            return dancers.ToString();
        }

        public class ArrayThatDancesWoooo
        {
            private List<char> _dancers;
            private readonly Regex SwapRegex = new Regex(@"[a-z](.+)\/(.+)");

            public ArrayThatDancesWoooo(string input)
            {
                _dancers = input.Select(x => x).ToList();
            }

            public void Dance(string move)
            {
                if (move[0] == 's')
                {
                    Spin(int.Parse(string.Join("", move.Skip(1))));
                }
                else if (move[0] == 'x')
                {
                    var match = SwapRegex.Match(move);
                    Exchange(int.Parse(match.Groups[1].ToString()), int.Parse(match.Groups[2].ToString()));
                }
                else if (move[0] == 'p')
                {
                    var match = SwapRegex.Match(move);
                    Partner(char.Parse(match.Groups[1].ToString()), char.Parse(match.Groups[2].ToString()));
                }
            }

            private void Spin(int size)
            {
                var splitAmount = _dancers.Count - size;
                var firstHalf = _dancers.Take(splitAmount);
                var secondHalf = _dancers.Skip(splitAmount);
                _dancers = secondHalf.Concat(firstHalf).ToList();
            }

            private void Exchange(int index1, int index2)
            {
                var temp = _dancers[index1];
                _dancers[index1] = _dancers[index2];
                _dancers[index2] = temp;
            }

            private void Partner(char value1, char value2)
            {
                var index1 = _dancers.IndexOf(value1);
                var index2 = _dancers.IndexOf(value2);
                Exchange(index1, index2);
            }

            public override string ToString()
            {
                return new string(_dancers.ToArray());
            }
        }
    }
}
