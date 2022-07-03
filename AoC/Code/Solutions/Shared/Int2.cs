using System;
using System.Diagnostics.CodeAnalysis;

namespace AoC.Code.Solutions.Shared
{
    public struct Int2 : IEquatable<Int2>
    {
        public int x { get; }
        public int y { get; }

        public static Int2 Zero => new(0, 0);
        public static Int2 One => new(1, 1);
        public static Int2 Up => new(0, 1);
        public static Int2 Down => new(0, -1);
        public static Int2 Left => new(-1, 0);
        public static Int2 Right => new(1, 0);

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Int2 operator*(int scalar, Int2 int2)
        {
            return new(scalar * int2.x, scalar * int2.y);
        }

        public static Int2 operator+(Int2 a, Int2 b)
        {
            return new(a.x + b.x, a.y + b.y);
        }

        public override string ToString()
        {
            return $"({x},{y})";
        }

        public bool Equals(Int2 other)
        {
            if (x == other.x && y == other.y) return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is Int2 && Equals((Int2)obj);
        }

        public static bool operator ==(Int2 left, Int2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Int2 left, Int2 right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (x + 101) * y + 173;
        }
    }

    public static class Int2Extensions
    {
        public static int ManhattanDistance(this Int2 val) => Math.Abs(val.x) + Math.Abs(val.y);
        public static int SqDistance(this Int2 val) => val.x * val.x + val.y * val.y;
        public static double Distance(this Int2 val) => Math.Sqrt(val.SqDistance());
    }
}
