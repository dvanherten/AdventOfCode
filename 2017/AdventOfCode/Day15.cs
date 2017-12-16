using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AdventOfCode.Supporting;

namespace AdventOfCode
{
    public class Day15
    {
        public const int FactorA = 16807;
        public const int FactorB = 48271;

        public static int SolvePart1(int seedA, int seedB, int cycles)
        {
            var a = new Generator(seedA, FactorA);
            var b = new Generator(seedB, FactorB);

            var likePairs = 0;
            for (var i = 0; i < cycles; i++)
            {
                a.GenerateNewValue();
                b.GenerateNewValue();
                if (a.FirstSixteenBits == b.FirstSixteenBits)
                    likePairs++;
            }

            return likePairs;
        }

        public static int SolvePart2(int seedA, int seedB, int cycles)
        {
            var a = new Generator(seedA, FactorA, x => x % 4 == 0);
            var b = new Generator(seedB, FactorB, x => x % 8 == 0);

            var likePairs = 0;
            for (var i = 0; i < cycles; i++)
            {
                a.GenerateNewValue();
                b.GenerateNewValue();
                if (a.FirstSixteenBits == b.FirstSixteenBits)
                    likePairs++;
            }

            return likePairs;
        }

        public class Generator
        {
            private readonly Predicate<long> _condition;
            public const int Divisor = 2147483647;
            
            public int Factor { get; }
            public long Value { get; private set; }
            public short FirstSixteenBits => (short)(Value & 0xffff);

            public Generator(int seed, int factor, Predicate<long> condition = null)
            {
                if (condition == null)
                    _condition = x => true;
                else
                    _condition = condition;
                Value = seed;
                Factor = factor;
            }

            public void GenerateNewValue()
            {
                do
                {
                    Value = (Value * Factor) % Divisor;
                } while (!_condition(Value));
            }
        }
    }
}
