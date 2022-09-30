// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Globalization;

namespace Cathei.Mathematics
{
    public readonly partial struct Incremental
    {
        #region Internal utilities

        /// <summary>
        /// Maximum powers of 10.
        /// It will be inaccurate in long type when make this number higher.
        /// </summary>
        private const int MaxPowersOf10Range = 18;

        /// <summary>
        /// Lookup table for power of 10s.
        /// </summary>
        private static readonly long[] PowersOf10 = new long[MaxPowersOf10Range + 1];

        static Incremental()
        {
            long value = 1;

            for (int i = 0; i <= MaxPowersOf10Range; ++i)
            {
                PowersOf10[i] = value;
                value *= 10;
            }
        }

        /// <summary>
        /// Fast multiply or divide by pow 10.
        /// The constant division will be optimized by compiler.
        /// </summary>
        private static long MultiplyPow10(long value, long pow)
        {
            switch (pow)
            {
                case 0: return value;
                case 1: return value * 10;
                case 2: return value * 100;
                case 3: return value * 1_000;
                case 4: return value * 10_000;
                case 5: return value * 100_000;
                case 6: return value * 1_000_000;
                case 7: return value * 10_000_000;
                case 8: return value * 100_000_000;
                case 9: return value * 1_000_000_000;
                case 10: return value * 10_000_000_000;
                case 11: return value * 100_000_000_000;
                case 12: return value * 1_000_000_000_000;
                case 13: return value * 10_000_000_000_000;
                case 14: return value * 100_000_000_000_000;
                case 15: return value * 1_000_000_000_000_000;
                case 16: return value * 10_000_000_000_000_000;
                case 17: return value * 100_000_000_000_000_000;
                case 18: return value * 1_000_000_000_000_000_000;
                case -1: return value / 10;
                case -2: return value / 100;
                case -3: return value / 1_000;
                case -4: return value / 10_000;
                case -5: return value / 100_000;
                case -6: return value / 1_000_000;
                case -7: return value / 10_000_000;
                case -8: return value / 100_000_000;
                case -9: return value / 1_000_000_000;
                case -10: return value / 10_000_000_000;
                case -11: return value / 100_000_000_000;
                case -12: return value / 1_000_000_000_000;
                case -13: return value / 10_000_000_000_000;
                case -14: return value / 100_000_000_000_000;
                case -15: return value / 1_000_000_000_000_000;
                case -16: return value / 10_000_000_000_000_000;
                case -17: return value / 100_000_000_000_000_000;
                case -18: return value / 1_000_000_000_000_000_000;
            }

            throw new OverflowException();
        }

        /// <summary>
        /// Internal common log for normalization.
        /// </summary>
        private static int Log10Int(long value)
        {
            int result = 0;

            if (value >= 1_0000_0000_0000_0000L)
            {
                result += 16;
                value /= 1_0000_0000_0000_0000L;
            }
            else // maximum log 10 for long type is 18 
            {
                if (value >= 1_0000_0000L)
                {
                    result += 8;
                    value /= 1_0000_0000L;
                }

                if (value >= 10000)
                {
                    result += 4;
                    value /= 10000;
                }
            }

            if (value >= 100)
            {
                result += 2;
                value /= 100;
            }

            if (value >= 10)
                result += 1;
            
            return result;
        }

        private static decimal ToDecimalNormalized(long value, byte scale = Precision)
        {
            bool isNegative = value < 0;

            if (isNegative)
                value = -value;

            int lower = (int)(value & 0xFFFF_FFFF);
            int upper = (int)((value >> 32) & 0x7FFF_FFFF);

            return new decimal(lower, upper, 0, isNegative, scale);
        }
        
        private readonly struct AlreadyNormalized { }

        #endregion
    }
}