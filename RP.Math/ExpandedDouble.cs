namespace RP.Math
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices.WindowsRuntime;

    /// <summary>
    /// Representation of a double expanded into its component parts.
    /// </summary>
    /// <remarks>Double structure is 1bit: Sign, 52bits mantissa, 11bits exponent</remarks>
    /// <acknowlagement>http://blogs.msdn.com/b/ericlippert/archive/2011/02/17/looking-inside-a-double.aspx</acknowlagement>
    /// <acknowlagement>http://csharpindepth.com/articles/general/FloatingPoint.aspx</acknowlagement>
    public struct ExpandedDouble
    {
        private ulong bits;

        #region Constants

        public const int ExponentBias = 1026;

        /// <summary>
        /// The exponent value as stored for Infinite and NaN values
        /// </summary>
        public const int ReservedValueExponent = 2047;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="d"></param>
        public ExpandedDouble(double d)
        {
            // Bit converter will also sort out the edian-ness for the current hardware platform.
            this.bits = (ulong)BitConverter.DoubleToInt64Bits(d);
        }

        #endregion

        #region Decisions

        /// <summary>
        /// Is the sign negative?
        /// </summary>
        /// <remarks>This will be true for NaN although NaN is not usualy considered to have a sign it does in the underlying representation.</remarks>
        public bool IsNegative
        {
            get
            {
                return this.Sign != 0;
            }
        }

        /// <summary>
        /// Is this double in normalised form where the leading bit of the mantissa is assumed to be one.
        /// </summary>
        public bool IsNormalised {
            get
            {
                return ExponentAsStored != 0;
            }
        }

        /// <summary>
        /// Is this double subnormal or denormalised (true for very small numbers) and the leading bit of the mantissa is present.
        /// </summary>
        public bool IsSubnormal
        {
            get
            {
                return ExponentAsStored == 0;
            }
        }

        /// <summary>
        /// Is this double the reserved value for Not a Number (NaN).
        /// </summary>
        public bool IsNaN
        {
            get
            {
                return ExponentAsStored == ReservedValueExponent && MantissaAsStored != 0;
            }
        }

        /// <summary>
        /// Is this double the reserved value for a signalling NaN (SNaN),
        /// signifing that the result of a mathematical operation is undefined.
        /// </summary>
        public bool IsSignallingNaN
        {
            get
            {
                return ExponentAsStored == ReservedValueExponent && MantissaAsStored == 1;
            }
        }

        /// <summary>
        /// Is this double the reserved value for a quiet NaN (QNaN),
        /// signify that the result of a mathematical operation was invalid not mearly inderterminate.
        /// </summary>
        public bool IsQuietNaN
        {
            get
            {
                return ExponentAsStored == ReservedValueExponent && MantissaAsStored > 1;
            }
        }

        /// <summary>
        /// Is this double the reserved value for positive or negative infinity.
        /// </summary>
        public bool IsInifinite
        {
            get
            {
                return ExponentAsStored == ReservedValueExponent && MantissaAsStored == 0;
            }
        }

        /// <summary>
        /// Is this double the reserved value for positive infinity.
        /// </summary>
        public bool IsPositiveInfinity
        {
            get
            {
                return IsInifinite && !IsNegative;
            }
        }

        /// <summary>
        /// Is this double the reserved value for negative infinity.
        /// </summary>
        public bool IsNegativeInfinity
        {
            get
            {
                return IsInifinite && IsNegative;
            }
        }

        #endregion

        #region Component parts

        /// <summary>
        /// The sign of the double, 0 for positive, one for negative;
        /// </summary>
        public int Sign
        {
            get
            {
                // 11bits exponent + 52bits mantissa
                return this.bits.Bit(63);
            }
        }

        /// <summary>
        /// The binary (base 2) exponent of the double (after the bias has been removed).
        /// </summary>
        public int Exponent
        {
            get
            {
                if (IsInifinite || IsNaN)
                {
                    throw new InvalidOperationException(
                        "This double is a reserved value, the exponent is therefore meaningless after the bias has been removed");
                }

                // Bias is to allow for reserved values, so remove it
                return this.ExponentAsStored - ExponentBias;
            }
        }

        /// <summary>
        /// The exponent as stored before the bias for reserved values has been removed
        /// </summary>
        public int ExponentAsStored
        {
            get
            {
                return (int)this.bits.Bits(62, 52);
            }
        }

        /// <summary>
        /// The exponent as stored before the bias for reserved values has been removed as a collection of bits
        /// </summary>
        public IEnumerable<int> ExponentBitsAsStored
        {
            get
            {
                return this.bits.BitSeq(62, 52);
            }
        }

        /// <summary>
        ///  The unsigned mantissa of the double after implicit values are added.
        /// </summary>
        public ulong Mantissa
        {
            get
            {
                ulong result = this.MantissaAsStored;

                // If the value is normalised then add the implicit leading one
                if (this.IsNormalised)
                {
                    result += 1; // todo: this may be in reverse so we need to do, mantissa | (1L<<52), test it!
                }

                return result;
            }
        }

        /// <summary>
        ///  The unsigned mantissa of the double as stored.
        /// </summary>
        public ulong MantissaAsStored
        {
            get
            {
                return this.bits.Bits(51, 0);
            }
        }

        /// <summary>
        ///  The unsigned mantissa of the double as stored as a collection of bits.
        /// </summary>
        public IEnumerable<int> MantissaBitsAsStored
        {
            get
            {
                return this.bits.BitSeq(51, 0);
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Convert a <see cref="System.double"/> into an <see cref="ExpandedDouble"/>
        /// </summary>
        /// <param name="d">The <see cref="System.Double"/> to convert.</param>
        public static explicit operator ExpandedDouble(double d)
        {
            return new ExpandedDouble(d);
        }

        #endregion
    }
}