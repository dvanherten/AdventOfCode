using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode.Supporting
{
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

    public class sub : Instruction
    {
        public sub(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
        {
        }

        public override void PerformOperation(IDictionary<char, long> registers)
        {
            registers[Register] -= GetInstructionValue(registers);
            Debug.WriteLine($"[{ProgramId}] Sub {GetInstructionValue(registers)} to {Register} [{registers[Register]}]");
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

    public class jnz : Instruction
    {
        public long? NextInstructionOffset { get; private set; }
        public jnz(int programId, char register, object instructionValue) : base(programId, register, instructionValue)
        {
        }

        public override void PerformOperation(IDictionary<char, long> registers)
        {
            // Assume if Register doesn't have the key that it is a number that will qualify (not 0)
            if (!registers.ContainsKey(Register) || registers[Register] != 0)
                NextInstructionOffset = GetInstructionValue(registers);
            else
                NextInstructionOffset = null;
            Debug.WriteLine($"[{ProgramId}] Jumping {NextInstructionOffset}");
        }
    }
    #endregion
}
