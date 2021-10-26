using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLib
{
    public class Vector2d
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }


        public static Vector2d operator +(Vector2d V, Vector2d W)
        {
            return new(V.X + W.X, V.Y + W.Y);
        }

        public static Vector2d operator -(Vector2d V, Vector2d W) => new(V.X - W.X, V.Y - W.Y);

        public static Vector2d operator -(Vector2d V) => new(-V.X, -V.Y);

        public static double operator *(Vector2d V, Vector2d W) => V.X * W.X + V.Y * W.Y;

        public static Vector2d operator *(Vector2d V, double s) => new(V.X * s, V.Y * s);

        public static Vector2d operator *(double s, Vector2d V) => new(V.X * s, V.Y * s);

        public static Vector2d operator /(Vector2d V, double s) => new(V.X / s, V.Y / s);

        public override string ToString() => $"({X}, {Y})";


        public double Length() => System.Math.Sqrt(X * X + Y * Y);

        public Vector2d Normalize()
        {
            var l = Length();
            X /= l;
            Y /= l;

            return this;
        }


    }
}
