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
        public static bool EqualWithinTolerance(this double a, double b, double maxAbsoluteError)
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
        public static bool EqualWithinTolerance(this double a, double b, double maxAbsoluteError, double maxRelativeError)
        {
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);

            if (EqualWithinTolerance(a, b, maxAbsoluteError) )
            {
                return true;
            }

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
        /// Comparator within a numer of significan 
        /// </summary>
        /// <param name="a">The double to compare to</param>
        /// <param name="b">The double to compare with</param>
        /// <param name="maxUlps">The Units in the Last Place (ulp)s that we </param>
        /// <returns>Truth if the doubles are equal within a tolerance</returns>
        /// <acknowalgement>http://www.cygnus-software.com/papers/comparingfloats/comparingfloats.htm</acknowalgement>
        public static bool EqualToUlps(this double a, double b, long maxUlps)
        {
            if (a.Equals(b))
            {
                // shortcut, handles infinities
                return true;
            }

            long aAsBits = a.To2Compliment();
            long bAsBits = b.To2Compliment();
            long diff = Math.Abs(aAsBits - bAsBits);
            return diff <= maxUlps;
        }

        /// <summary>
        /// Get the twos complement long representation of a double.
        /// </summary>
        /// <param name="value">The double to convert</param>
        /// <returns>The twos complement long representation of a double</returns>
        private static long To2Compliment(this double value)
        {
            long valueAsLong = BitConverter.DoubleToInt64Bits(value);

            return valueAsLong < 0
                ? (long)(0x8000000000000000 - (ulong)valueAsLong)
                : valueAsLong;
        }
    }
}