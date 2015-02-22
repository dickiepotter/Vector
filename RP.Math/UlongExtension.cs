namespace RP.Math
{
    using System.Collections.Generic;

    /// <summary>
    /// Extention methods for ULong
    /// </summary>
    /// <acknowlagement>http://blogs.msdn.com/b/ericlippert/archive/2011/02/17/looking-inside-a-double.aspx</acknowlagement>
    public static class UlongExtension
    {
        public static int Bit(this ulong x, int bit)
        {
            return (int)((x >> bit) & 0x01);
        }

        public static ulong Bits(this ulong x, int high, int low)
        {
            x <<= (63 - high);
            x >>= (low + 63 - high);
            return x;
        }

        public static IEnumerable<int> BitSeq(this ulong x, int high, int low)
        {
            for (int bit = high; bit >= low; --bit)
                yield return Bit(x, bit);
        }
    }
}