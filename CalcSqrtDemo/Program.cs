using System;

namespace CalcSqrtDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Give me a number");
            }

            if (!double.TryParse(args[0], out var c))
            {
                Console.WriteLine("Enter a correct number");
            }

            Console.WriteLine($"Calc SQRT({c})");

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

            Console.WriteLine($"SQRT = {x}");

            Console.WriteLine("Hello World!");
        }
    }
}
