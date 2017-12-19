using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Supporting;
using Microsoft.Build.Tasks;

namespace AdventOfCode
{
    public class Day18
    {

        public static long SolvePart1(string[] input)
        {
            var instructions = BuildInstructions(input);
            var registers = instructions.Select(x => x.Register).Distinct().ToDictionary(x => x, x => 0L);
            var lastSound = 0L;
            for (var i = 0L; i < instructions.Length; i++)
            {
                var instruction = instructions[i];

                instruction.PerformOperation(registers);

                if (instruction is jgz)
                {
                    var offset = ((jgz) instruction).NextInstructionOffset;
                    if (offset.HasValue) 
                        i = i + offset.Value - 1;
                }
                else if (instruction is snd)
                {
                    var soundPlayed = ((snd)instruction).SoundPlayed;
                    if (soundPlayed.HasValue)
                        lastSound = soundPlayed.Value;
                }

                if (!instruction.KeepGoing())
                    break;
            }
            return lastSound;
        }

        private static Instruction[] BuildInstructions(string[] input)
        {
            var list = new List<Instruction>();
            foreach (var instruction in input)
            {
                list.Add(Instruction.Build(instruction));
            }
            return list.ToArray();
        }
        
        public static int SolvePart2(string[] input)
        {
            throw new NotImplementedException();
        }

        #region Operations
        public abstract class Instruction
        {
            public char Register { get; }
            public object InstructionValue { get; }

            public Instruction(char register, object instructionValue)
            {
                Register = register;
                InstructionValue = instructionValue;
            }

            public abstract void PerformOperation(IDictionary<char, long> registers);

            public virtual bool KeepGoing()
            {
                return true;
            }

            public long GetInstructionValue(IDictionary<char, long> registers)
            {
                if (InstructionValue is char c)
                    return registers[c];
                return (long)InstructionValue;
            }

            public static Instruction Build(string input)
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

                if (split[0] == "snd")
                    return new snd(register, instructionValue);
                if (split[0] == "set")
                    return new set(register, instructionValue);
                if (split[0] == "add")
                    return new add(register, instructionValue);
                if (split[0] == "mul")
                    return new mul(register, instructionValue);
                if (split[0] == "mod")
                    return new mod(register, instructionValue);
                if (split[0] == "rcv")
                    return new rcv(register, instructionValue);
                if (split[0] == "jgz")
                    return new jgz(register, instructionValue);

                throw new ArgumentException($"Invalid input: {input}");
            }
        }

        public class snd : Instruction
        {
            public snd(char register, object operationValue) : base(register, operationValue)
            {
            }

            public long? SoundPlayed { get; private set; }
            public override void PerformOperation(IDictionary<char, long> registers)
            {
                if (registers[Register] > 0)
                    SoundPlayed = registers[Register];
            }
        }

        public class set : Instruction
        {
            public set(char register, object operationValue) : base(register, operationValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                registers[Register] = GetInstructionValue(registers);
            }
        }

        public class add : Instruction
        {
            public add(char register, object operationValue) : base(register, operationValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                registers[Register] += GetInstructionValue(registers);
            }
        }

        public class mul : Instruction
        {
            public mul(char register, object operationValue) : base(register, operationValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                registers[Register] *= GetInstructionValue(registers);
            }
        }

        public class mod : Instruction
        {
            public mod(char register, object operationValue) : base(register, operationValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                registers[Register] %= GetInstructionValue(registers);
            }
        }

        public class rcv : Instruction
        {
            public rcv(char register, object operationValue) : base(register, operationValue)
            {
            }

            private bool _keepGoing = true;
            public override void PerformOperation(IDictionary<char, long> registers)
            {
                if (registers[Register] != 0)
                    _keepGoing = false;
            }

            public override bool KeepGoing()
            {
                return _keepGoing;
            }
        }

        public class jgz : Instruction
        {
            public long? NextInstructionOffset { get; private set; }
            public jgz(char register, object operationValue) : base(register, operationValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                if (registers[Register] > 0)
                    NextInstructionOffset = GetInstructionValue(registers);
                else
                    NextInstructionOffset = null;
            }
        }
        #endregion
    }
}
