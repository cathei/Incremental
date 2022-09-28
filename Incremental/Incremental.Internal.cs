// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Globalization;

namespace Cathei.Mathematics
{
    /// <summary>
    /// 16-byte deterministic floating point decimal type.
    /// </summary>
    public readonly partial struct Incremental
    {
        #region Internal utilities

        /// <summary>
        /// Maximum powers of 10.
        /// It will be inaccurate in long type when make this number higher.
        /// </summary>
        private const int MaxPowersOf10Range = 20;

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
                    value /= PowersOf10[eval];
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