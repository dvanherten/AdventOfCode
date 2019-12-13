using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class IntCodeComputer
    {
        private readonly Dictionary<int, OpCode> _activeOpCodes = new Dictionary<int, OpCode>();

        public IntCodeComputer()
        {
            Register<OpCode1>();
            Register<OpCode2>();
            Register<OpCode3>();
            Register<OpCode5>();
            Register<OpCode6>();
            Register<OpCode7>();
            Register<OpCode8>();
            Register<OpCode99>();
        }

        public void Register<T>() where T : OpCode, new()
        {
            var opCode = new T();
            _activeOpCodes.Add(opCode.Code, opCode);
        }

        public void Register(OpCode opCode)
        {
            _activeOpCodes.Add(opCode.Code, opCode);
        }

        public int[] RunProgram(int[] program)
        {
            var index = 0;
            while (true)
            {
                var opCodeValue = program[index] % 100;
                var opCode = _activeOpCodes[opCodeValue];
                if (opCode == null)
                    throw new Exception("Invalid program");
                index = opCode.Process(ref program, index);
                if (index == -1)
                    break;
            }

            return program;
        }
    }

    public abstract class OpCode
    {
        public abstract int Code { get; }
        public abstract int Process(ref int[] program, int index);

        protected int GetParameterLocation(int[] program, int index, ParameterMode mode)
        {
            switch (mode)
            {
                case ParameterMode.Position:
                    return program[index];
                case ParameterMode.Immediate:
                    return index;
            }

            throw new Exception("whoops!");
        }
    }

    public class OpCode1: OpCode
    {
        public override int Code => 1;
        public override int Process(ref int[] program, int index)
        {
            var fullOpCode = program[index];
            var firstParamMode = Math.Abs(fullOpCode / 100 % 10);
            var firstParamLocation = GetParameterLocation(program, index + 1, (ParameterMode)firstParamMode);
            var secondParamMode = Math.Abs(fullOpCode / 1000 % 10);
            var secondParamLocation = GetParameterLocation(program, index + 2, (ParameterMode)secondParamMode);
            var thirdParamMode = Math.Abs(fullOpCode / 10000 % 10);
            var thirdParamLocation = GetParameterLocation(program, index + 3, (ParameterMode)thirdParamMode);
            program[thirdParamLocation] = program[firstParamLocation] + program[secondParamLocation];
            return index + 4;
        }
    }

    public class OpCode2 : OpCode
    {
        public override int Code => 2;
        public override int Process(ref int[] program, int index)
        {
            var fullOpCode = program[index];
            var paramMode = Math.Abs(fullOpCode / 100 % 10);
            var firstParamLocation = GetParameterLocation(program, index + 1, (ParameterMode)paramMode);
            paramMode = Math.Abs(fullOpCode / 1000 % 10);
            var secondParamLocation = GetParameterLocation(program, index + 2, (ParameterMode)paramMode);
            paramMode = Math.Abs(fullOpCode / 10000 % 10);
            var thirdParamLocation = GetParameterLocation(program, index + 3, (ParameterMode)paramMode);
            program[thirdParamLocation] = program[firstParamLocation] * program[secondParamLocation];
            return index + 4;
        }
    }

    public class OpCode3 : OpCode
    {
        public static int Input { get; set; }
        public override int Code => 3;
        public override int Process(ref int[] program, int index)
        {
            var fullOpCode = program[index];
            var paramMode = Math.Abs(fullOpCode / 100 % 10);
            var firstParamLocation = GetParameterLocation(program, index + 1, (ParameterMode)paramMode);
            program[firstParamLocation] = Input;
            return index + 2;
        }
    }

    public class OpCode4 : OpCode
    {
        public Action<int> LogOutput;
        public override int Code => 4;
        public override int Process(ref int[] program, int index)
        {
            var fullOpCode = program[index];
            var paramMode = Math.Abs(fullOpCode / 100 % 10);
            var firstParamLocation = GetParameterLocation(program, index + 1, (ParameterMode)paramMode); 
            LogOutput?.Invoke(program[firstParamLocation]);
            return index + 2;
        }
    }

    public class OpCode5 : OpCode
    {
        public override int Code => 5;
        public override int Process(ref int[] program, int index)
        {
            var fullOpCode = program[index];
            var paramMode = Math.Abs(fullOpCode / 100 % 10);
            var firstParamLocation = GetParameterLocation(program, index + 1, (ParameterMode)paramMode);
            paramMode = Math.Abs(fullOpCode / 1000 % 10);
            var secondParamLocation = GetParameterLocation(program, index + 2, (ParameterMode)paramMode);
            return program[firstParamLocation] != 0 ? program[secondParamLocation] : index + 3;
        }
    }

    public class OpCode6 : OpCode
    {
        public override int Code => 6;
        public override int Process(ref int[] program, int index)
        {
            var fullOpCode = program[index];
            var paramMode = Math.Abs(fullOpCode / 100 % 10);
            var firstParamLocation = GetParameterLocation(program, index + 1, (ParameterMode)paramMode);
            paramMode = Math.Abs(fullOpCode / 1000 % 10);
            var secondParamLocation = GetParameterLocation(program, index + 2, (ParameterMode)paramMode);
            return program[firstParamLocation] == 0 ? program[secondParamLocation] : index + 3;
        }
    }

    public class OpCode7 : OpCode
    {
        public override int Code => 7;

        public override int Process(ref int[] program, int index)
        {
            var fullOpCode = program[index];
            var firstParamMode = Math.Abs(fullOpCode / 100 % 10);
            var firstParamLocation = GetParameterLocation(program, index + 1, (ParameterMode)firstParamMode);
            var secondParamMode = Math.Abs(fullOpCode / 1000 % 10);
            var secondParamLocation = GetParameterLocation(program, index + 2, (ParameterMode)secondParamMode);
            var thirdParamMode = Math.Abs(fullOpCode / 10000 % 10);
            var thirdParamLocation = GetParameterLocation(program, index + 3, (ParameterMode)thirdParamMode);
            program[thirdParamLocation] = program[firstParamLocation] < program[secondParamLocation] ? 1 : 0;
            return index + 4;
        }
    }

    public class OpCode8 : OpCode
    {
        public override int Code => 8;

        public override int Process(ref int[] program, int index)
        {
            var fullOpCode = program[index];
            var firstParamMode = Math.Abs(fullOpCode / 100 % 10);
            var firstParamLocation = GetParameterLocation(program, index + 1, (ParameterMode)firstParamMode);
            var secondParamMode = Math.Abs(fullOpCode / 1000 % 10);
            var secondParamLocation = GetParameterLocation(program, index + 2, (ParameterMode)secondParamMode);
            var thirdParamMode = Math.Abs(fullOpCode / 10000 % 10);
            var thirdParamLocation = GetParameterLocation(program, index + 3, (ParameterMode)thirdParamMode);
            program[thirdParamLocation] = program[firstParamLocation] == program[secondParamLocation] ? 1 : 0;
            return index + 4;
        }
    }

    public class OpCode99 : OpCode
    {
        public override int Code => 99;
        public override int Process(ref int[] program, int index)
        {
            return -1;
        }
    }

    public enum ParameterMode
    {
        Position = 0,
        Immediate = 1
    }
}
