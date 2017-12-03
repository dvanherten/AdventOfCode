using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public class Day3
    {
        public static int SolvePart1(int input)
        {
            var centerPoint = FindCenterPoint(input);
            var startingPoint = FindPointForNumber(input);
            return CalculateManhattanDistance(startingPoint, centerPoint);
        }

        public static int SolvePart2(int input)
        {
            throw new NotImplementedException();
        }

        public static int CalculateManhattanDistance(Point starting, Point destination)
        {
            // https://en.wikipedia.org/wiki/Taxicab_geometry
            return Math.Abs(starting.X - destination.X) + Math.Abs(starting.Y - destination.Y);
        }

        public static int CalculateGridSize(int number)
        {
            // Each grid is an odd square.
            var root = Math.Sqrt(number);
            var floor = (int)Math.Floor(root);
            if (root == floor && root % 2 == 1)
                return floor;
            if (floor % 2 == 1)
                floor += 2;
            else
                floor++;
            return floor;
        }

        public static Point FindCenterPoint(int number)
        {
            var gridSize = CalculateGridSize(number);
            var center = gridSize / 2 + 1;
            return new Point(center, center);
        }

        public static Point FindPointForNumber(int number)
        {
            var gridSize = CalculateGridSize(number);
            var centerPoint = FindCenterPoint(number);
            var layersFromCenter = gridSize / 2;

            // Grids go down in root sizes by 2
            var sideRoot = gridSize - 2;
            var sideNumber = number - (int)Math.Pow(sideRoot, 2);
            var numbersInOuterCircle = (int)Math.Pow(gridSize, 2) - (int)Math.Pow((gridSize - 2), 2);
            var numbersPerSide = numbersInOuterCircle / 4;
            var side = sideNumber / Math.Max(numbersPerSide, 1);
            var locationModifier = sideNumber % Math.Max(numbersPerSide, 1);

            if (side == 0 || side == 4)
                return new Point(centerPoint.X + layersFromCenter, 1 + locationModifier);
            if (side == 1)
                return new Point(gridSize - locationModifier, centerPoint.Y + layersFromCenter);
            if (side == 2)
                return new Point(centerPoint.X - layersFromCenter, gridSize - locationModifier);
            if (side == 3)
                return new Point(1 + locationModifier, centerPoint.Y - layersFromCenter);
            
            throw new Exception("Shouldn't happen");
        }
    }
}
