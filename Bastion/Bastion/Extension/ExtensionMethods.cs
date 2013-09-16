using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bastion.ExtensionMethods
{
    public static class Extensions
    {
        public static Point Scale(this Point p, int scale)
        {
            return new Point(p.X * scale, p.Y * scale);
        }

        public static Point Translate(this Point p, int offset)
        {
            return new Point(p.X + offset, p.Y + offset);
        }

        public static Vector2 GetVector(this Point p)
        {
            return new Vector2(p.X, p.Y);
        }
    }

}
