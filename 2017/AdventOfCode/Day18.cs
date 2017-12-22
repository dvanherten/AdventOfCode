using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var instructions = BuildInstructionsPart1(input);
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
                else if (instruction is sound)
                {
                    var soundPlayed = ((sound)instruction).SoundPlayed;
                    if (soundPlayed.HasValue)
                        lastSound = soundPlayed.Value;
                }

                if (!instruction.KeepGoing())
                    break;
            }
            return lastSound;
        }

        public static long SolvePart2(string[] input)
        {
            var program0Queue = new Queue<long>();
            var program1Queue = new Queue<long>();
            var instructions0 = BuildInstructionsPart2(input, 0, program0Queue, program1Queue);
            var instructions1 = BuildInstructionsPart2(input, 1, program1Queue, program0Queue);
            var registers0 = instructions0.Select(x => x.Register).Where(x => !int.TryParse(x.ToString(), out var y)).Distinct().ToDictionary(x => x, x => 0L);
            var registers1 = instructions1.Select(x => x.Register).Where(x => !int.TryParse(x.ToString(), out var y)).Distinct().ToDictionary(x => x, x => x == 'p' ? 1L : 0L);
            var index0 = 0L;
            var index1 = 0L;
            var program1SendCount = 0;
            var program0Running = true;
            while(true)
            {
                var instruction0 = instructions0[index0];
                var instruction1 = instructions1[index1];

                if (program0Running)
                {
                    instruction0.PerformOperation(registers0);

                    if (instruction0 is jgz)
                    {
                        var offset = ((jgz) instruction0).NextInstructionOffset;
                        if (offset.HasValue)
                            index0 = index0 + offset.Value - 1;
                    }

                    if (instruction0.KeepGoing())
                        index0++;
                    else
                        program0Running = false;
                }
                else
                {
                    instruction1.PerformOperation(registers1);

                    if (instruction1 is jgz)
                    {
                        var offset = ((jgz) instruction1).NextInstructionOffset;
                        if (offset.HasValue)
                            index1 = index1 + offset.Value - 1;
                    }
                    if (instruction1 is send)
                        program1SendCount++;


                    if (instruction1.KeepGoing())
                        index1++;
                    else
                        program0Running = true;
                }

                if (!instruction0.KeepGoing() && program0Queue.Count == 0 && 
                    !instruction1.KeepGoing() && program1Queue.Count == 0)
                    break;

            }
            return program1SendCount;
        }

        private static Instruction[] BuildInstructionsPart1(string[] input)
        {
            var list = new List<Instruction>();
            foreach (var instruction in input)
            {
                list.Add(Instruction.BuildPart1(instruction));
            }
            return list.ToArray();
        }

        private static Instruction[] BuildInstructionsPart2(string[] input, int programId, Queue<long> sendQueue, Queue<long> receiveQueue)
        {
            var list = new List<Instruction>();
            foreach (var instruction in input)
            {
                list.Add(Instruction.BuildPart2(instruction, programId, sendQueue, receiveQueue));
            }
            return list.ToArray();
        }

        #region Operations
        public abstract class Instruction
        {
            public char Register { get; }
            public object InstructionValue { get; }
            public int ProgramId { get; }

            public Instruction(int programId, char register, object instructionValue)
            {
                ProgramId = programId;
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

            public static Instruction BuildPart1(string input)
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
                    return new set(0, register, instructionValue);
                if (split[0] == "add")
                    return new add(0, register, instructionValue);
                if (split[0] == "mul")
                    return new mul(0, register, instructionValue);
                if (split[0] == "mod")
                    return new mod(0, register, instructionValue);
                if (split[0] == "snd")
                    return new sound(0, register, instructionValue);
                if (split[0] == "rcv")
                    return new recover(0, register, instructionValue);
                if (split[0] == "jgz")
                    return new jgz(0, register, instructionValue);

                throw new ArgumentException($"Invalid input: {input}");
            }

            public static Instruction BuildPart2(string input, int programId, Queue<long> sendQueue, Queue<long> receiveQueue)
            {
                var split = input.Split(null);
                var register = split[1].First();
                object instructionValue;
                if (split[0] == "snd" && split.Length == 2)
                {
                    if (long.TryParse(split[1], out var longValue))
                        instructionValue = longValue;
                    else
                        instructionValue = split[1].First();
                }
                else if (split.Length == 2)
                    instructionValue = null;
                else if (long.TryParse(split[2], out var longValue))
                    instructionValue = longValue;
                else
                    instructionValue = split[2].First();

                if (split[0] == "set")
                    return new set(programId, register, instructionValue);
                if (split[0] == "add")
                    return new add(programId, register, instructionValue);
                if (split[0] == "mul")
                    return new mul(programId, register, instructionValue);
                if (split[0] == "mod")
                    return new mod(programId, register, instructionValue);
                if (split[0] == "snd")
                    return new send(programId, register, instructionValue, sendQueue);
                if (split[0] == "rcv")
                    return new receive(programId, register, receiveQueue);
                if (split[0] == "jgz")
                    return new jgz(programId, register, instructionValue);

                throw new ArgumentException($"Invalid input: {input}");
            }
        }

        public class set : Instruction
        {
            public set(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                Debug.WriteLine($"[{ProgramId}] Set {GetInstructionValue(registers)} to {Register}");
                registers[Register] = GetInstructionValue(registers);
                
            }
        }

        public class add : Instruction
        {
            public add(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                registers[Register] += GetInstructionValue(registers);
                Debug.WriteLine($"[{ProgramId}] Add {GetInstructionValue(registers)} to {Register} [{registers[Register]}]");
            }
        }

        public class mul : Instruction
        {
            public mul(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                registers[Register] *= GetInstructionValue(registers);
                Debug.WriteLine($"[{ProgramId}] Multiply {GetInstructionValue(registers)} to {Register} [{registers[Register]}]");
            }
        }

        public class mod : Instruction
        {
            public mod(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                registers[Register] %= GetInstructionValue(registers);
                Debug.WriteLine($"[{ProgramId}] Mod {GetInstructionValue(registers)} to {Register} [{registers[Register]}]");
            }
        }

        public class sound : Instruction
        {
            public sound(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
            {
            }

            public long? SoundPlayed { get; private set; }
            public override void PerformOperation(IDictionary<char, long> registers)
            {
                if (registers[Register] > 0)
                    SoundPlayed = registers[Register];
            }
        }

        public class recover : Instruction
        {
            public recover(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
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

        public class send : Instruction
        {
            public Queue<long> Queue { get; }

            public send(int programId, char register, object instructionValue, Queue<long> queue) : base(programId, register, instructionValue)
            {
                Queue = queue;
            }
            
            public override void PerformOperation(IDictionary<char, long> registers)
            {
                Debug.WriteLine($"[{ProgramId}] Sending {GetInstructionValue(registers)} from {InstructionValue} to queue");
                Queue.Enqueue(GetInstructionValue(registers));
            }
        }

        public class receive : Instruction
        {
            public Queue<long> Queue { get; }

            public receive(int programId, char register, Queue<long> queue) : base(programId, register, null)
            {
                Queue = queue;
            }

            private bool _keepGoing = true;
            public override void PerformOperation(IDictionary<char, long> registers)
            {
                if (Queue.Count == 0)
                {
                    Debug.WriteLine($"[{ProgramId}] Queue empty. Waiting...");
                    _keepGoing = false;
                    return;
                }
                var queueValue = Queue.Dequeue();
                registers[Register] = queueValue;
                Debug.WriteLine($"[{ProgramId}] Receiving {queueValue} into {Register}");
                _keepGoing = true;
            }

            public override bool KeepGoing()
            {
                return _keepGoing;
            }
        }

        public class jgz : Instruction
        {
            public long? NextInstructionOffset { get; private set; }
            public jgz(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
            {
            }

            public override void PerformOperation(IDictionary<char, long> registers)
            {
                // Assume if Register doesn't have the key that it is a number that will qualify (greater than 0)
                if (!registers.ContainsKey(Register) || registers[Register] > 0)
                    NextInstructionOffset = GetInstructionValue(registers);
                else
                    NextInstructionOffset = null;
                Debug.WriteLine($"[{ProgramId}] Jumping {NextInstructionOffset}");
            }
        }
        #endregion
    }
}
