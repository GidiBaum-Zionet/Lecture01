using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLib;

namespace MathTest
{
    static class VectorDemo
    {
        public static void Run()
        {
            var p = new Vector2d(7, 9);
            var q = new Vector2d(3, 1);

            var w = p + q;

            Console.WriteLine(w);
        }
    }
}
