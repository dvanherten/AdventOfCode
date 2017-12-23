using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Supporting;

namespace AdventOfCode
{
    public class Day23
    {
        public static long SolvePart1(string[] input)
        {
            var instructions = input.Select(x => BuildPart1(x)).ToArray();
            var registers = instructions.Select(x => x.Register).Where(x => !int.TryParse(x.ToString(), out var y)).Distinct().ToDictionary(x => x, x => 0L);
            var mulCalled = 0L;
            for (var i = 0L; i < instructions.Length; i++)
            {
                var instruction = instructions[i];

                instruction.PerformOperation(registers);

                if (instruction is jnz)
                {
                    var offset = ((jnz)instruction).NextInstructionOffset;
                    if (offset.HasValue)
                        i = i + offset.Value - 1;
                }
                else if (instruction is mul)
                {
                    mulCalled++;
                }

                if (!instruction.KeepGoing())
                    break;
            }
            return mulCalled;
        }

        public static int SolvePart2(string[] input)
        {
            throw new NotImplementedException();
        }


        public static Instruction BuildPart1(string input, int programId = 0)
        {
            var split = input.Split(null);
            var register = split[1].First();
            object instructionValue;
            if (split.Length == 2)
                instructionValue = null;
            else if (long.TryParse(split[2], out var longValue))
                instructionValue = longValue;
            else
                instructionValue = split[2].First();

            if (split[0] == "set")
                return new set(programId, register, instructionValue);
            if (split[0] == "sub")
                return new sub(programId, register, instructionValue);
            if (split[0] == "mul")
                return new mul(programId, register, instructionValue);
            if (split[0] == "jnz")
                return new jnz(programId, register, instructionValue);

            throw new ArgumentException($"Invalid input: {input}");
        }
    }
}
