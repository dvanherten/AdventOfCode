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
    public class Day10
    {
        public static int SolvePart1(string[] input, int arraySize)
        {
            var lengths = input.Select(int.Parse).ToArray();
            var sparseHash = GetSparseHash(lengths, arraySize);
            return sparseHash[0] * sparseHash[1];
        }
        
        public static string SolvePart2(string input, int arraySize)
        {
            var lengths = GenerateLengths(input);
            var sparseHash = GetSparseHash(lengths, arraySize, 64);
            var denseHash = BuildDenseHash(sparseHash);
            var knotHash = ConvertToKnotHash(denseHash);
            return knotHash;
        }

        private static int[] GetSparseHash(int[] lengths, int arraySize, int repeat = 1)
        {
            var sparseHash = BuildInitialArray(arraySize);
            var currentPosition = 0;
            var skipSize = 0;

            for (var loop = 0; loop < repeat; loop++)
            {
                foreach (var length in lengths)
                {
                    // Lengths larger than the size of the list are invalid.
                    if (length > sparseHash.Length)
                        continue;

                    var subArray = GetSubArray(sparseHash, length, currentPosition);
                    subArray = subArray.Reverse().ToArray();
                    for (var i = 0; i < length; i++)
                    {
                        var offSetIndex = (currentPosition + i) % sparseHash.Length;
                        sparseHash[offSetIndex] = subArray[i];
                    }
                    currentPosition = (currentPosition + length + skipSize) % sparseHash.Length;
                    skipSize++;
                }
            }

            return sparseHash;
        }

        private static int[] BuildDenseHash(int[] sparseHash)
        {
            var denseHash = new int[16];

            for (var i = 0; i < 16; i++)
                denseHash[i] = sparseHash
                    .Skip(i * 16)
                    .Take(16)
                    .Aggregate((running, next) => running ^ next);
            
            return denseHash;
        }

        private static string ConvertToKnotHash(int[] denseHash)
        {
            return string.Join("", denseHash.Select(x => x.ToString("X2"))).ToLower();
        }

        public static int[] GenerateLengths(string input)
        {
            var lengths = input.Select(x => (int)x).ToArray();
            return lengths.Concat(new int[] {17, 31, 73, 47, 23}).ToArray();
        }

        private static int[] BuildInitialArray(int arraySize)
        {
            var array = new int[arraySize];
            for (int i = 0; i < array.Length; i++)
                array[i] = i;
            return array;
        }

        private static int[] GetSubArray(int[] sourceArray, int length, int startingIndex)
        {
            var array = sourceArray.Skip(startingIndex).Take(length).ToArray();
            var remainder = length - array.Length;
            if (remainder > 0)
                array = array.Concat(sourceArray.Take(remainder)).ToArray();
            return array;
        }
    }
}