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
        /// <summary>
        /// Maximum precision under decimal point with long type mantissa.
        /// </summary>
        public const int Precision = 16;

        /// <summary>
        /// Mantissa of 1.
        /// </summary>
        public const long Unit = 1_0000_0000_0000_0000;

        /// <summary>
        /// Value of number 0.
        /// </summary>
        public static readonly Incremental Zero = new Incremental(0, 0);

        /// <summary>
        /// Value of number 1.
        /// </summary>
        public static readonly Incremental One = new Incremental(Unit, 0);

        #region Private utilities

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

        #endregion
    }
}