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
        private static readonly long[] PowersOf10 = new long[MaxPowersOf10Range];

        static Incremental()
        {
            long value = 1;

            for (int i = 0; i < MaxPowersOf10Range; ++i)
            {
                PowersOf10[i] = value;
                value *= 10;
            }
        }

        private static long MultiplyPow10(long value, int pow)
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

            return 0;
        }

        /// <summary>
        /// Internal common log for normalization.
        /// </summary>
        private static int Log10Int(long value)
        {
            int result = 0;
            int eval = 16;

            while (eval > 0)
            {
                if (value >= PowersOf10[eval])
                {
                    result += eval;
                    value = MultiplyPow10(value, -eval);
                }

                eval /= 2;
            }

            return result;
        }

        private static decimal ToDecimalNormalized(long value, byte scale = Precision)
        {
            bool isNegative = value < 0;
            value = isNegative ? -value : value;

            int lower = (int)(value & 0xFFFF_FFFF);
            int upper = (int)((value >> 32) & 0x7FFF_FFFF);

            return new decimal(lower, upper, 0, isNegative, scale);
        }


        #endregion
    }
}