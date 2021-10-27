using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib
{
    public static class MathFunctions
    {
        public static double sqrt(double x) => System.Math.Sqrt(x);
        public static Vector2d vec2(double x, double y) => new Vector2d(x, y);
    }
}
