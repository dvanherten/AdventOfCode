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
    public class Day8
    {
        public static int SolvePart1(string[] input)
        {
            return Solve(input).MaxVal;
        }

        public static int SolvePart2(string[] input)
        {
            return Solve(input).MaxMem;
        }

        public static (int MaxVal, int MaxMem) Solve(string[] input)
        {
            var registerValues = new Dictionary<string, int>();
            var maxMem = 0;
            foreach (var instruction in input)
            {
                var registerChangeInstruction = new RegisterChangeInstruction(instruction);
                var checkValue = registerValues.ContainsKey(registerChangeInstruction.NameToCheckAgainst)
                    ? registerValues[registerChangeInstruction.NameToCheckAgainst]
                    : 0;
                if (registerChangeInstruction.ExpressionSatisfied(checkValue))
                {
                    if (!registerValues.ContainsKey(registerChangeInstruction.Name))
                        registerValues.Add(registerChangeInstruction.Name, 0);

                    if (registerChangeInstruction.Increase)
                        registerValues[registerChangeInstruction.Name] += registerChangeInstruction.ChangeAmount;
                    else
                        registerValues[registerChangeInstruction.Name] -= registerChangeInstruction.ChangeAmount;

                    if (registerValues[registerChangeInstruction.Name] > maxMem)
                        maxMem = registerValues[registerChangeInstruction.Name];
                }
            }

            var maxVal = registerValues.Values.Max();
            return (maxVal, maxMem);
        }

        public class RegisterChangeInstruction
        {
            public RegisterChangeInstruction(string instruction)
            {
                var split = instruction.Split(null);
                Name = split[0];
                Increase = split[1] == "inc";
                ChangeAmount = int.Parse(split[2]);
                NameToCheckAgainst = split[4];
                ComparisonOperator = split[5];
                ComparisonValue = int.Parse(split[6]);
            }
            
            public string Name { get; set; }
            public bool Increase { get; set; }
            public int ChangeAmount { get; set; }
            public string NameToCheckAgainst { get; set; }
            public string ComparisonOperator { get; set; }
            public int ComparisonValue { get; set; }

            public bool ExpressionSatisfied(int valueToCheck)
            {
                var providedValueExpression = Expression.Constant(valueToCheck, typeof(int));
                var comparisonValueExpression = Expression.Constant(ComparisonValue, typeof(int));
                Expression binaryExpression = null;
                if (ComparisonOperator == "==")
                    binaryExpression = Expression.Equal(providedValueExpression, comparisonValueExpression);
                else if (ComparisonOperator == "!=")
                    binaryExpression = Expression.NotEqual(providedValueExpression, comparisonValueExpression);
                else if (ComparisonOperator == ">")
                    binaryExpression = Expression.GreaterThan(providedValueExpression, comparisonValueExpression);
                else if (ComparisonOperator == ">=")
                    binaryExpression = Expression.GreaterThanOrEqual(providedValueExpression, comparisonValueExpression);
                else if (ComparisonOperator == "<")
                    binaryExpression = Expression.LessThan(providedValueExpression, comparisonValueExpression);
                else if (ComparisonOperator == "<=")
                    binaryExpression = Expression.LessThanOrEqual(providedValueExpression, comparisonValueExpression);
                else
                {
                    throw new Exception($"Missing Expression for {ComparisonOperator}");
                }

                return Expression.Lambda<Func<bool>>(binaryExpression).Compile()();
            }
        }
    }
}