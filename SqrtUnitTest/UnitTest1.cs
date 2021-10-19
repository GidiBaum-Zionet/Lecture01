using System;
using ClassLib;
using Xunit;
using Xunit.Abstractions;

namespace SqrtUnitTest
{
    public class UnitTest1
    {
        readonly ITestOutputHelper _TestOutput;

        public UnitTest1(ITestOutputHelper testOutput)
        {
            _TestOutput = testOutput;
        }

        [Fact]
        public void Test1()
        {
            var rnd = new Random();
            var tol = 1e-4;
            double x = 0;

            for (var i = 0; i < 100; i++)
            {
                var c = rnd.NextDouble() * 10;
                var x0 = Math.Sqrt(c);

                try
                {
                    x = SqrtCalculator.Calculate(c);
                }
                catch (Exception e)
                {
                    _TestOutput.WriteLine($"SQRT({c}) = {x} OK");
                    throw;
                }

                Assert.True(Math.Abs(x - x0) < tol);
                _TestOutput.WriteLine($"SQRT({c}) = {x} OK");
            }
        }
    }
}
