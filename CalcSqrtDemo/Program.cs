using System;
using ClassLib;

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

            var x = SqrtCalculator.Calculate(c);

            Console.WriteLine($"SQRT = {x}");

            Console.WriteLine("Hello World!");
        }
    }
}
