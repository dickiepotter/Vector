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
            Console.WriteLine("Test Utility for RP.Math.Vector3");
            Console.WriteLine();
            Console.WriteLine(@"This utility will test various vector operations on RP.Math.Vector3 and a number of third party libraries.");
            Console.WriteLine(@"The result is time taken for {0} iterations of the operation in ticks.", iterations);
            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();

            var results = RunTests();

            foreach (var kvp in results)
            {
                Console.WriteLine(kvp.Value + " => " + kvp.Key);
            }

            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine(" Done... Press Any Key to Exit");
            Console.ReadKey();
        }

        #region Helper Methods

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

        #endregion

        #region Run the tests

        private static Dictionary<string, long> RunTests()
        {
            var results = new Dictionary<string, long>();

            RunVector3Tests(results);
            RunMathNetTests(results);
            RunSharpDxTests(results);
            RunSevenTests(results);
            RunExocortexTests(results);

            return results;
        }

        /// <summary>
        /// Run tests for SharpDx
        /// </summary>
        /// <seealso cref="http://sharpdx.org/"/>
        /// <param name="results">The collection of results to add to</param>
        /// <remarks>Included using Nuget</remarks>
        private static void RunSharpDxTests(Dictionary<string, long> results)
        {
            var libraryName = "SharpDX";

            var a = new SharpDX.Vector3(2, 4, 6);
            var b = new SharpDX.Vector3(5, 7, 10);

            results.Add(
                "Cross Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = SharpDX.Vector3.Cross(a, b);
                        }));

            results.Add(
                "Dot Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = SharpDX.Vector3.Dot(a, b);
                        }));

            results.Add(
                "Normalize => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            // Mutable
                            var vector = new SharpDX.Vector3(2, 4, 6);
                            vector.Normalize();
                        }));

            results.Add(
                "Add => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a + b;
                        }));

            results.Add(
                "Subtract => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a - b;
                        }));
        }

        /// <summary>
        /// Run tests for Math.Net
        /// </summary>
        /// <seealso cref="http://www.mathdotnet.com/"/>
        /// <param name="results">The collection of results to add to</param>
        /// <remarks>Included using Nuget</remarks>
        private static void RunMathNetTests(Dictionary<string, long> results)
        {
            var libraryName = "MathNet";

            var a = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] { 2, 4, 6 });
            var b = new MathNet.Numerics.LinearAlgebra.Double.DenseVector(new double[] { 5, 7, 10 });

            results.Add(
                "Cross Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a.OuterProduct(b); // Not sure if this is cross product or not
                        }));

            results.Add(
                "Dot Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a.DotProduct(b);
                        }));

            results.Add(
                "Normalize => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a.Normalize(0);
                        }));

            results.Add(
                "Add => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a + b;
                        }));

            results.Add(
                "Subtract => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a - b;
                        }));
        }

        /// <summary>
        /// Run tests for Seven Framework (From comments on <seealso cref="http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C"/>
        /// </summary>
        /// <seealso cref="https://github.com/53V3N1X/SevenFramework"/>
        /// <param name="results">The collection of results to add to</param>
        /// <remarks>Included as a DLL</remarks>
        private static void RunSevenTests(Dictionary<string, long> results)
        {
            var libraryName = "Seven";

            var a = new Seven.Mathematics.Vector<double>(2d, 4d, 6d);
            var b = new Seven.Mathematics.Vector<double>(5d, 7d, 10d);

            results.Add(
                "Cross Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = a.CrossProduct(b);
                    }));

            results.Add(
                "Dot Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = a.DotProduct(b);
                    }));

            results.Add(
                "Normalize => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = a.Normalize();
                    }));

            results.Add(
                "Add => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = a + b;
                    }));

            results.Add(
                "Subtract => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = a - b;
                    }));
        }

        /// <summary>
        /// Run tests for ExoCortex
        /// </summary>
        /// <seealso cref="http://www.codeproject.com/Articles/1728/ExoEngine-a-C-D-engine"/>
        /// <param name="results">The collection of results to add to</param>
        /// <remarks>
        /// Included as a DLL.
        /// The vector implementation in this library only comes as a container of float data types (the test may not be strictly fair)
        /// </remarks>
        private static void RunExocortexTests(Dictionary<string, long> results)
        {
            var libraryName = "Exocortex";

            var a = new Exocortex.Geometry3D.Vector3D(2f, 4f, 6f);
            var b = new Exocortex.Geometry3D.Vector3D(5f, 7f, 10f);

            results.Add(
                "Cross Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = Exocortex.Geometry3D.Vector3D.CrossProduct(a, b);
                    }));

            results.Add(
                "Dot Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = Exocortex.Geometry3D.Vector3D.DotProduct(a, b);
                    }));

            results.Add(
                "Normalize => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        // Mutable
                        var vector = new Exocortex.Geometry3D.Vector3D(2f, 4f, 6f);
                        vector.Normalize();
                    }));

            results.Add(
                "Add => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = a + b;
                    }));

            results.Add(
                "Subtract => " + libraryName,
                Measure(
                    iterations,
                    () =>
                    {
                        var result = a - b;
                    }));
        }

        /// <summary>
        /// Run tests for this Vector3 project
        /// </summary>
        /// <param name="results">The collection of results to add to</param>
        private static void RunVector3Tests(Dictionary<string, long> results)
        {
            var libraryName = "RP.Math.Vector3";

            var a = new Vector3(2, 4, 6);
            var b = new Vector3(5, 7, 10);

            results.Add(
                "Cross Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a.CrossProduct(b);
                        }));

            results.Add(
                "Dot Product => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a.DotProduct(b);
                        }));

            results.Add(
                "Normalize => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a.Normalize();
                        }));

            results.Add(
                "Add => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a + b;
                        }));

            results.Add(
                "Subtract => " + libraryName,
                Measure(
                    iterations,
                    () =>
                        {
                            var result = a - b;
                        }));
        }

        #endregion
    }
}
