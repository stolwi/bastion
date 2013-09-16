using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Map
{
    public class DoublePosition3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public DoublePosition3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Position3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public static implicit operator DoublePosition3(Position3 other)
        {
            return new DoublePosition3(other.X, other.Y, other.Z);
        }

        public void Move(double x, double y, double z)
        {
            X += x;
            Y += y;
            Z += z;
        }
    }
}
