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
        private static readonly Func<long, long>[] MultiplyPow10Table = new Func<long, long>[MaxPowersOf10Range * 2 + 1];

        static Incremental()
        {
            long value = 1;

            for (int i = 0; i <= MaxPowersOf10Range; ++i)
            {
                PowersOf10[i] = value;
                value *= 10;
            }
            
            MultiplyPow10Table[MaxPowersOf10Range + 0] = x => x;
            MultiplyPow10Table[MaxPowersOf10Range + 1] = x => x * 10;
            MultiplyPow10Table[MaxPowersOf10Range + 2] = x => x * 100;
            MultiplyPow10Table[MaxPowersOf10Range + 3] = x => x * 1_000;
            MultiplyPow10Table[MaxPowersOf10Range + 4] = x => x * 10_000;
            MultiplyPow10Table[MaxPowersOf10Range + 5] = x => x * 100_000;
            MultiplyPow10Table[MaxPowersOf10Range + 6] = x => x * 1_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 7] = x => x * 10_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 8] = x => x * 100_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 9] = x => x * 1_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 10] = x => x * 10_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 11] = x => x * 100_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 12] = x => x * 1_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 13] = x => x * 10_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 14] = x => x * 100_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 15] = x => x * 1_000_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 16] = x => x * 10_000_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 17] = x => x * 100_000_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + 18] = x => x * 1_000_000_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -1] = x => x / 10;
            MultiplyPow10Table[MaxPowersOf10Range + -2] = x => x / 100;
            MultiplyPow10Table[MaxPowersOf10Range + -3] = x => x / 1_000;
            MultiplyPow10Table[MaxPowersOf10Range + -4] = x => x / 10_000;
            MultiplyPow10Table[MaxPowersOf10Range + -5] = x => x / 100_000;
            MultiplyPow10Table[MaxPowersOf10Range + -6] = x => x / 1_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -7] = x => x / 10_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -8] = x => x / 100_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -9] = x => x / 1_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -10] = x => x / 10_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -11] = x => x / 100_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -12] = x => x / 1_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -13] = x => x / 10_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -14] = x => x / 100_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -15] = x => x / 1_000_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -16] = x => x / 10_000_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -17] = x => x / 100_000_000_000_000_000;
            MultiplyPow10Table[MaxPowersOf10Range + -18] = x => x / 1_000_000_000_000_000_000;
        }

        private static long MultiplyPow10(long value, int pow)
        {
            return MultiplyPow10Table[MaxPowersOf10Range + pow](value);
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