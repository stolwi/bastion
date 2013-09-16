using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Map
{
    public class Position3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Position3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Position3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public static implicit operator Position3(DoublePosition3 other)
        {
            return new Position3((int)other.X, (int)other.Y, (int)other.Z);
        }

        public Position3 DivideBy(int scale)
        {
            return new Position3(X / scale, Y / scale, Z);
        }
        public Position3 MultiplyBy(int scale)
        {
            return new Position3(X * scale, Y * scale, Z);
        }
        public override string ToString()
        {
            return string.Format("[{0},{1},{2}]", X, Y, Z);
        }
    }
}
