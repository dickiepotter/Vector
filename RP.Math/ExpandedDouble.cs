namespace RP.Math
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Representation of a double expanded into its component parts
    /// </summary>
    /// <acknowlagement>http://blogs.msdn.com/b/ericlippert/archive/2011/02/17/looking-inside-a-double.aspx</acknowlagement>
    public struct ExpandedDouble
    {
        private ulong bits;

        public ExpandedDouble(double d)
        {
            this.bits = (ulong)BitConverter.DoubleToInt64Bits(d);
        }

        public int Sign
        {
            get
            {
                return this.bits.Bit(63);
            }
        }

        public bool IsNegative
        {
            get
            {
                return this.Sign != 0;
            }
        }

        public int Exponent
        {
            get
            {
                return (int)this.bits.Bits(62, 52);
            }
        }

        public IEnumerable<int> ExponentBits
        {
            get
            {
                return this.bits.BitSeq(62, 52);
            }
        }

        public ulong Mantissa
        {
            get
            {
                return this.bits.Bits(51, 0);
            }
        }

        public IEnumerable<int> MantissaBits
        {
            get
            {
                return this.bits.BitSeq(51, 0);
            }
        }

        public static implicit operator ExpandedDouble(double d)
        {
            return new ExpandedDouble(d);
        }
    }
}