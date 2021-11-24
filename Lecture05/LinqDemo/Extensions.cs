using static BaseLib.MathFunctions;

namespace LinqDemo
{
    static class Extensions
    {
        public static bool IsPrime(this int n)
        {
            var maxI = (int)sqrt(n);

            for (var i = 2; i <= maxI; i++)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

    }
}
