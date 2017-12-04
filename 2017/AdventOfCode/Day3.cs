using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Supporting;

namespace AdventOfCode
{
    public class Day3
    {
        public static int SolvePart1(int input)
        {
            var gridSize = CalculateGridSize(input);
            var centerPoint = FindCenterPoint(gridSize);
            var startingPoint = FindPointForNumber(input);
            return CalculateManhattanDistance(startingPoint, centerPoint);
        }

        public static int SolvePart2(int input)
        {
            var gridSize = (int) Math.Sqrt(input) + 1;
            var numberOfPoints = (int) Math.Pow(gridSize, 2);
            var grid = new Dictionary<Point, int>();
            for (int i = 0; i < numberOfPoints; i++)
            {
                var x = i / gridSize;
                var y = i % gridSize;
                grid.Add(new Point(x + 1, y + 1), 0);
            }

            var centerPoint = FindCenterPoint(gridSize);
            grid[centerPoint] = 1;

            var currentNumber = 0;
            var counter = 0;
            var currentLocation = centerPoint;
            var direction = Direction.Right;
            var moveCounter = 0;
            var sideLength = 1;
            var directionChanges = 0;
            while (currentNumber < input && counter < numberOfPoints)
            {
                // move
                currentLocation = GetNextPoint(currentLocation, direction);
                moveCounter++;
                // change direction if we hit the end of a side
                if (moveCounter == sideLength)
                {
                    moveCounter = 0;

                    direction = direction.ChangeDirection();
                    directionChanges++;
                    // every 2 direction changes the size to walk increases
                    if (directionChanges == 2)
                    {
                        directionChanges = 0;
                        sideLength++;
                    }
                }

                // calculate
                var location = currentLocation;
                currentNumber = grid.Where(x => CalculateChebyshevDistance(location, x.Key) == 1).Sum(x => x.Value);
                grid[currentLocation] = currentNumber;

                counter++;
            }
            return currentNumber;
        }

        private static Point GetNextPoint(Point currentLocation, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return new Point(currentLocation.X + 1, currentLocation.Y);
                case Direction.Up:
                    return new Point(currentLocation.X, currentLocation.Y + 1);
                case Direction.Left:
                    return new Point(currentLocation.X - 1, currentLocation.Y);
                case Direction.Down:
                    return new Point(currentLocation.X, currentLocation.Y - 1);
            }
            throw new Exception("Shouldn't happen");
        }

        public static int CalculateManhattanDistance(Point starting, Point destination)
        {
            // https://en.wikipedia.org/wiki/Taxicab_geometry
            return Math.Abs(starting.X - destination.X) + Math.Abs(starting.Y - destination.Y);
        }

        public static int CalculateChebyshevDistance(Point starting, Point destination)
        {
            // https://en.wikipedia.org/wiki/Chebyshev_distance
            return Math.Max(Math.Abs(starting.X - destination.X), Math.Abs(starting.Y - destination.Y));
        }

        public static int CalculateGridSize(int number)
        {
            // Each grid is an odd square.
            var root = Math.Sqrt(number);
            var floor = (int) Math.Floor(root);
            if (root == floor && root % 2 == 1)
                return floor;
            if (floor % 2 == 1)
                floor += 2;
            else
                floor++;
            return floor;
        }

        public static Point FindPointForNumber(int number)
        {
            var gridSize = CalculateGridSize(number);
            var centerPoint = FindCenterPoint(gridSize);
            var layersFromCenter = gridSize / 2;

            // Grids go down in root sizes by 2
            var sideRoot = gridSize - 2;
            var sideNumber = number - (int) Math.Pow(sideRoot, 2);
            var numbersInOuterCircle = (int) Math.Pow(gridSize, 2) - (int) Math.Pow((gridSize - 2), 2);
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

        private static Point FindCenterPoint(int gridSize)
        {
            var center = gridSize / 2 + 1;
            return new Point(center, center);
        }
    }

    public enum Direction
    {
        Right = 0,
        Up = 1,
        Left = 2,
        Down = 3
    }

    public static class DirectionExention
    {
        public static Direction ChangeDirection(this Direction currentDirection)
        {
            int nextDirection = ((int)currentDirection + 1) % 4;
            return (Direction) nextDirection;
        }
    }
}