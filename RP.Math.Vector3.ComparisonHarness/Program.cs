namespace RP.Math.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    class Program
    {
        private const int iterations = 1000;

        static void Main(string[] args)
        {
            var results = RunTests();

            foreach (var kvp in results)
            {
                Console.WriteLine(kvp.Value + " => " + kvp.Key);
            }

            Console.ReadKey();
        }

        private static long Measure(int iterations, Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < iterations; i++)
            {
                action();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }

        private static Dictionary<string, long> RunTests()
        {
            var results = new Dictionary<string, long>();

            #region RP.Math.Vector3

            results.Add(
                "Cross Product => RP.Math.Vector3",
                Measure(
                    iterations,
                    () =>
                        {
                            var a = new Vector3(2, 4, 6);
                            var b = new Vector3(5, 7, 10);
                            var result = a.CrossProduct(b);
                        }));

            results.Add(
                "Dot Product => RP.Math.Vector3",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new Vector3(2, 4, 6);
                        var b = new Vector3(5, 7, 10);
                        var result = a.DotProduct(b);
                    }));

            results.Add(
                "Normalize => RP.Math.Vector3",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new Vector3(2, 4, 6);
                        var b = new Vector3(5, 7, 10);
                        var result = a.Normalize();
                    }));

            results.Add(
                "Add => RP.Math.Vector3",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new Vector3(2, 4, 6);
                        var b = new Vector3(5, 7, 10);
                        var result = a + b;
                    }));

            results.Add(
               "Subtract => RP.Math.Vector3",
               Measure(
                   iterations,
                   () =>
                   {
                       var a = new Vector3(2, 4, 6);
                       var b = new Vector3(5, 7, 10);
                       var result = a - b;
                   }));

            #endregion

            #region Math.Net

            results.Add(
                "Cross Product => MathNet",
                Measure(
                    iterations,
                    () =>
                        {
                            var a = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] { 2, 4, 6 });
                            var b = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] { 5, 7, 10 });
                            var result = a.OuterProduct(b); // Not sure if this is cross product or not
                        }));

            results.Add(
                "Dot Product => MathNet",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] { 2, 4, 6 });
                        var b = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] { 5, 7, 10 });
                        var result = a.DotProduct(b);
                    }));

            results.Add(
                "Normalize => MathNet",
                Measure(
                    iterations,
                    () =>
                        {
                            var a = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] { 2, 4, 6 });
                            var b = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] { 5, 7, 10 });
                            var result = a.Normalize(0);
                    }));

            results.Add(
                "Add => MathNet",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] {2, 4, 6});
                        var b = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] {5, 7, 10});
                        var result = a + b;
                    }));

            results.Add(
               "Subtract => MathNet",
               Measure(
                   iterations,
                   () =>
                   {
                       var a = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] {2, 4, 6});
                       var b = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] {5, 7, 10});
                       var result = a - b;
                   }));

            #endregion

            #region SharpDX

            results.Add(
                "Cross Product => SharpDX",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new SharpDX.Vector3(2, 4, 6);
                        var b = new SharpDX.Vector3(5, 7, 10);
                        var result = SharpDX.Vector3.Cross(a, b);
                    }));

            results.Add(
                "Dot Product => SharpDX",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new SharpDX.Vector3(2, 4, 6);
                        var b = new SharpDX.Vector3(5, 7, 10);
                        var result = SharpDX.Vector3.Dot(a, b);
                    }));

            results.Add(
                "Normalize => SharpDX",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new SharpDX.Vector3(2, 4, 6);
                        var b = new SharpDX.Vector3(5, 7, 10);
                        a.Normalize();
                    }));

            results.Add(
                "Add => SharpDX",
                Measure(
                    iterations,
                    () =>
                    {
                        var a = new SharpDX.Vector3(2, 4, 6);
                        var b = new SharpDX.Vector3(5, 7, 10);
                        var result = a + b;
                    }));

            results.Add(
               "Subtract => SharpDX",
               Measure(
                   iterations,
                   () =>
                   {
                       var a = new SharpDX.Vector3(2, 4, 6);
                       var b = new SharpDX.Vector3(5, 7, 10);
                       var result = a - b;
                   }));

            #endregion

            return results;
        }
    }
}
