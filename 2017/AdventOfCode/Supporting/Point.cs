using System;
using System.Diagnostics;

namespace AdventOfCode.Supporting
{
    [DebuggerDisplay("X = {X}, Y = {Y}")]
    public struct Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        /// <summary>
        /// Assumes 0,0 is top left corner.
        /// </summary>
        public Point GetNextPoint(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return new Point(X + 1, Y);
                case Direction.Up:
                    return new Point(X, Y - 1);
                case Direction.Left:
                    return new Point(X - 1, Y);
                case Direction.Down:
                    return new Point(X, Y + 1);
            }
            throw new Exception("Shouldn't happen");
        }
    }
}