using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaseLib;

namespace LinqDemo
{
    public class AsyncDemo
    {
        public static async void Run01()
        {
            using var cts = new CancellationTokenSource(50);

            var primes = await FindPrimesAsync(cts);

            Console.WriteLine(primes.ToCsv());
        }

        public static async Task Run02()
        {
            using var cts = new CancellationTokenSource();

            _ = Task.Run(() =>
            {
                Console.WriteLine("Press enter to stop the task");
                Console.ReadKey();

                // Cancel the task
                cts.Cancel();
            });


            var primes = await FindPrimesAsync(cts);

            Console.WriteLine($"Found {primes.Count()} primes");
        }


        public static async Task<IEnumerable<int>> FindPrimesAsync(CancellationTokenSource tcs)
        {
            return await Task<IEnumerable<int>>.Factory.StartNew(() =>
            {
                var primeList = new List<int>();

                int i = 2;
                while (true)
                {
                    if (tcs.IsCancellationRequested)
                        return primeList;

                    if (i.IsPrime())
                    {
                        primeList.Add(i);
                    }

                    i++;
                }
                
            });
        }
    }
}
