namespace RP.Math
{
    using System;

    public static class DoubleExtension
    {
        /// <summary>
        /// Comparator within an absolute tolerance
        /// </summary>
        /// <param name="a">The double to compare to</param>
        /// <param name="b">The double to compare with</param>
        /// <param name="maxAbsoluteError">The tolerance for the comparison compared against the difference of the two doubles</param>
        /// <returns>Truth if the doubles are equal within a tolerance</returns>
        /// <remarks>
        /// Use this tolerant equality method if comparing against zero. The tolerance should be a small multiple of <see cref="double.Epsilon"/>.
        /// Also, check that you are not comparing floats and doubles.
        /// </remarks>
        public static bool AlmostEqualsWithAbsTolerance(this double a, double b, double maxAbsoluteError)
        {
            double diff = Math.Abs(a - b);

            if (a.Equals(b))
            {
                // shortcut, handles infinities
                return true;
            }

            return diff <= maxAbsoluteError;
        }

        /// <summary>
        /// Comparator within an absolute or relative tolerance
        /// </summary>
        /// <param name="a">The double to compare to</param>
        /// <param name="b">The double to compare with</param>
        /// <param name="maxAbsoluteError">The absolute tolerance for the comparison</param>
        /// <param name="maxRelativeError">The relative tolerance for the comparison</param>
        /// <returns>Truth if the doubles are equal within a tolerance</returns>
        /// <acknowalgement>http://www.cygnus-software.com/papers/comparingfloats/comparingfloats.htm</acknowalgement>
        /// <remarks>
        /// Quote from: http://www.cygnus-software.com/papers/comparingfloats/comparingfloats.htm
        /// If you are comparing against a non-zero number then relative epsilons or ULPs based comparisons are probably what you want. 
        /// You’ll probably want some small multiple of double.Epsilon for your relative epsilon, or some small number of ULPs. 
        /// An absolute epsilon could be used if you knew exactly what number you were comparing against.</remarks>
        public static bool AlmostEqualsWithAbsOrRelativeTolerance(
            this double a,
            double b,
            double maxAbsoluteError,
            double maxRelativeError)
        {
            // Needed if we are comparing to zero
            if (AlmostEqualsWithAbsTolerance(a, b, maxAbsoluteError))
            {
                return true;
            }

            // Check the relative tolerance
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);

            double relativeError;
            if (absB > absA)
            {
                relativeError = Math.Abs((a - b) / b);
            }
            else
            {
                relativeError = Math.Abs((a - b) / a);
            }

            return relativeError <= maxRelativeError;
        }

        /// <summary>
        /// Comparator within a numer of units in the last place that we are tolerant of (the numerb of "floats" difference that is acceptable)
        /// </summary>
        /// <param name="a">The double to compare to</param>
        /// <param name="b">The double to compare with</param>
        /// <param name="maxUlps">The Units in the Last Place (ulp)s that we are tolerant of</param>
        /// <returns>Truth if the doubles are equal within a tolerance</returns>
        /// <acknowalgement>http://www.cygnus-software.com/papers/comparingfloats/comparingfloats.htm</acknowalgement>
        /// <remarks>
        /// Quote from: http://www.cygnus-software.com/papers/comparingfloats/comparingfloats.htm
        /// If you are comparing against a non-zero number then relative epsilons or ULPs based comparisons are probably what you want. 
        /// You’ll probably want some small multiple of double.Epsilon for your relative epsilon, or some small number of ULPs. 
        /// An absolute epsilon could be used if you knew exactly what number you were comparing against.</remarks>
        public static bool AlmostEqualsWithAbsOrUlpsTolerance(
            this double a,
            double b,
            double maxAbsoluteError,
            long maxUlps)
        {
            // Needed if we are comparing to zero
            if (AlmostEqualsWithAbsTolerance(a, b, maxAbsoluteError))
            {
                return true;
            }

            // Check the ULPS tolerance
            long longA = BitConverter.DoubleToInt64Bits(a);
            longA = longA < 0 ? (long)(0x8000000000000000 - (ulong)longA) : longA;

            long longB = BitConverter.DoubleToInt64Bits(b);
            longB = longB < 0 ? (long)(0x8000000000000000 - (ulong)longB) : longB;

            long diff = Math.Abs(longA - longB);
            return diff <= maxUlps;
        }
    }
}