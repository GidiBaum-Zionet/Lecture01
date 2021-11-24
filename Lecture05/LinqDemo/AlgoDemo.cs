using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLib;

namespace LinqDemo
{
    public class AlgoDemo
    {
        public static void Run01()
        {
            Console.WriteLine("AlgoDemo::Run");

            int n = 7;

            var ok = n.IsPrime();

            Console.WriteLine($"{n} is prime: {ok}");
        }

        public static void Run02()
        {
            var timer = new Stopwatch();
            timer.Start();

            var primes = FindPrimes(1_000_000);

            timer.Stop();

            Console.WriteLine($"Found {primes.Count()} in {timer.ElapsedMilliseconds} ms");

            //Console.WriteLine(primes.ToCsv());
        }

        public static void Run03()
        {
            var arr = new List<string>();

            for (var i = 0; i < 12; i++)
            {
                arr.Add($"i={i}");
            }

            var x = arr[2];
        }

        public static void Run04()
        {
            var primes = PrimeGenerator(1_000_000);

            // int counter = 0;
            // foreach (var p in primes)
            // {
            //     Console.WriteLine($"{counter++}: {p}");
            // }

            Console.WriteLine($"Found {primes.Count()}");

            //Console.WriteLine(primes.ToCsv());
        }


        static IEnumerable<int> FindPrimes(int maxN)
        {
            var list = new List<int>();

            for (var i = 2; i <= maxN; i++)
            {
                if (i.IsPrime())
                    list.Add(i);
            }

            return list;
        }

        static IEnumerable<int> PrimeGenerator(int maxN)
        {
            for (var i = 2; i <= maxN; i++)
            {
                if (i.IsPrime())
                    yield return i;
            }
        }

    }
}
