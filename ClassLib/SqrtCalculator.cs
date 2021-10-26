using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class SqrtCalculator
    {
        public static double Calculate(double c)
        {
            if (c < 0)
                throw new Exception("Negative SQRT not supported");

            var min = 1.0;
            var max = c;
            var tol = 1e-4;
            var delta = c;
            var x = c;

            while (Math.Abs(delta) > tol)
            {
                x = (max + min) / 2;
                delta = x * x - c;

                (min, max) = (delta > 0) ? (min, x) : (x, max);

                Console.WriteLine($"x={x} d={delta}");
            }

            return x;
        }
    }
}
