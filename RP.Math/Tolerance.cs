namespace RP.Math.Tolerance
{
    /// <summary>
    /// The Tolerance interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface ITolerance<T> where T : struct 
    {
        /// <summary>
        /// The is within.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsWithin(T value, T target);
    }

    namespace RP.Math.Tolerance.Double
    {
        using System;
        using System.Collections.Generic;

        public class UlpsTolerance : ITolerance<double>
        {
            public int Ulps { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public UlpsTolerance(int ulps)
            {
                this.Ulps = ulps;
            }

            public bool IsWithin(double value, double target)
            {
                throw new NotImplementedException();
            }
        }

        public class PercentageTolerance : ITolerance<double>
        {
            public double Percentage { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public PercentageTolerance(double percentage)
            {
                this.Percentage = percentage;
            }

            public bool IsWithin(double value, double target)
            {
                throw new NotImplementedException();
            }
        }

        public class AbsoluteTolerance : ITolerance<double>
        {
            public double Absolute { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public AbsoluteTolerance(double absolute)
            {
                this.Absolute = absolute;
            }

            public bool IsWithin(double value, double target)
            {
                throw new NotImplementedException();
            }
        }

        public class RelativeTolerance : ITolerance<double>
        {
            public double Relative { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public RelativeTolerance(double relative)
            {
                this.Relative = relative;
            }

            public bool IsWithin(double value, double target)
            {
                throw new NotImplementedException();
            }
        }

        public class Range : ITolerance<double>
        {
            public double Min { get; private set; }

            public double Max { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public Range(double min, double max)
            {
                this.Min = Math.Min(min, max);
                this.Max = Math.Max(min, max);
            }

            public bool IsWithin(double value, double target)
            {
                throw new NotImplementedException();
            }
        }

        public class AbsoluteAndUlpsTolerance : ITolerance<double>
        {
            public UlpsTolerance Ulps { get; private set; }

            public AbsoluteTolerance Absolute { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public AbsoluteAndUlpsTolerance(UlpsTolerance ulps, AbsoluteTolerance absolute)
            {
                this.Ulps = ulps;
                this.Absolute = absolute;
            }

            public bool IsWithin(double value, double target)
            {
                throw new NotImplementedException();
            }
        }

        public class AbsoluteAndRelativeTolerance : ITolerance<double>
        {
            public AbsoluteTolerance Absolute { get; private set; }

            public RelativeTolerance Relative { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public AbsoluteAndRelativeTolerance(AbsoluteTolerance absolute, RelativeTolerance relative)
            {
                this.Absolute = absolute;
                this.Relative = relative;
            }

            public bool IsWithin(double value, double target)
            {
                throw new NotImplementedException();
            }
        }

        public class TolerantEqualityComparer : IEqualityComparer<double>
        {
            public ITolerance<double> Tolerance { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public TolerantEqualityComparer(ITolerance<double> tolerance)
            {
                this.Tolerance = tolerance;
            }

            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <returns>
            /// true if the specified objects are equal; otherwise, false.
            /// </returns>
            /// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
            public bool Equals(double x, double y)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Returns a hash code for the specified object.
            /// </summary>
            /// <returns>
            /// A hash code for the specified object.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
            public int GetHashCode(double obj)
            {
                throw new NotImplementedException();
            }
        }

        public class TolerantComparer : IComparer<double>
        {
            public ITolerance<double> Tolerance { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public TolerantComparer(ITolerance<double> tolerance)
            {
                this.Tolerance = tolerance;
            }

            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
            public int Compare(double x, double y)
            {
                throw new NotImplementedException();
            }
        }
    }

}
