﻿// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Globalization;

namespace Cathei.Mathematics
{
    public readonly partial struct Incremental
    {
        #region Internal utilities

        /// <summary>
        /// Maximum powers of 10.
        /// It will be inaccurate in ulong type when make this number higher.
        /// </summary>
        private const int MaxPowersOf10Range = 19;

        /// <summary>
        /// Lookup table for power of 10s.
        /// </summary>
        private static readonly ulong[] PowersOf10 = new ulong[MaxPowersOf10Range + 1];

        /// <summary>
        /// Pre-calculated 9,007,199,254,740,992 / 10,000,000,000,000,000 (Unit) value under decimal point.
        /// Used for multiplication.
        /// https://stackoverflow.com/questions/41183935/why-does-gcc-use-multiplication-by-a-strange-number-in-implementing-integer-divi
        /// </summary>
        private const ulong InverseUnitShift53 = 0xE695_94BE_C44D_E15B;

        private bool IsNegative => Mantissa < 0;

        static Incremental()
        {
            ulong value = 1;

            for (int i = 0; i <= MaxPowersOf10Range; ++i)
            {
                PowersOf10[i] = value;
                value *= 10;
            }
        }

        private static long MultiplyPow10(long value, int pow)
        {
            if (value >= 0)
                return (long)MultiplyPow10((ulong)value, pow);
            return -(long)MultiplyPow10((ulong)-value, pow);
        }

        /// <summary>
        /// Fast multiply or divide by pow 10.
        /// The constant division will be optimized by compiler.
        /// </summary>
        private static ulong MultiplyPow10(ulong value, int pow)
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
                case 19: return value * 10_000_000_000_000_000_000;
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
                case -19: return value / 10_000_000_000_000_000_000;
            }

            throw new OverflowException();
        }

        /// <summary>
        /// Internal common log for normalization.
        /// </summary>
        private static int Log10Int(ulong value)
        {
            int result = 0;

            if (value >= 1_0000_0000_0000_0000L)
            {
                result += 16;
                value /= 1_0000_0000_0000_0000L;
            }
            else // maximum log 10 for long type is 19
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

        /// <summary>
        /// Multiply two uint 32 to ulong
        /// </summary>
        private static ulong MultiplyUInt32(uint a, uint b)
        {
            return (ulong)a * (ulong)b;
        }

        /// <summary>
        /// Multiply two ulong and takes upper 64 bits
        /// Formula = (a * b) = ((aUpper + aLower) * (bUpper + bLower)) =
        /// ((aUpper * bUpper) + (aUpper * bLower) + (aLower * bUpper) + (aLower * bLower)).
        /// https://stackoverflow.com/questions/28868367/getting-the-high-part-of-64-bit-integer-multiplication
        /// </summary>
        private static ulong MultiplyUInt64(ulong a, ulong b)
        {
            uint aUpper = (uint)(a >> 32);
            uint aLower = (uint)a;

            uint bUpper = (uint)(b >> 32);
            uint bLower = (uint)b;

            ulong hi = MultiplyUInt32(aUpper, bUpper);
            ulong mid1 = MultiplyUInt32(aUpper, bLower);
            ulong mid2 = MultiplyUInt32(aLower, bUpper);

            // we could calculate carry bits
            // but Incremental have some unused bits as padding, so should be fine
            return hi + (mid1 >> 32) + (mid2 >> 32);

            // ulong lo = MultiplyUInt32(aLower, bLower);
            // ulong carry = ((ulong)(uint)mid1 + (ulong)(uint)mid2 + (lo >> 32)) >> 32;
            // return hi + (mid1 >> 32) + (mid2 >> 32) + carry;
        }

        /// <summary>
        /// Used for division.
        /// </summary>
        private static int FindDividendScale(ulong value)
        {
            int log10 = Log10Int(value);
            return 2 + Precision - log10;
        }

        /// <summary>
        /// Divide two normalized ulong and returns result mantissa.
        /// This method produces value that has one less exponent of [Unit, Unit * 100) range.
        /// https://stackoverflow.com/questions/71440466/how-can-i-quickly-and-accurately-multiply-a-64-bit-integer-by-a-64-bit-fraction
        /// </summary>
        private static ulong DivideUInt64(ulong a, ulong b)
        {
            ulong result = 0;

            // produce one more digit so mantissa would be shifted
            int exponent = Precision + 1;

            // it is safe to multiply 100 at first loop
            int diff = 2;

            // first shift divisor to rightmost place
            ulong bTenth = b / 10;

            while (b == bTenth * 10)
            {
                b = bTenth;
                bTenth = b / 10;
                exponent--;
            }

            // do partial division iteration
            while (true)
            {
                // shift dividend to leftmost place
                a *= PowersOf10[diff];
                exponent -= diff;

                if (a <= ulong.MaxValue / 10)
                {
                    // you've got to scale one more, lucky!
                    a *= 10;
                    exponent--;
                }

                // there is no Math.DivRem for UInt64?
                ulong q = a / b;
                a -= q * b;

                // write to result
                result += MultiplyPow10(q, exponent);

                if (a == 0 || exponent <= 0)
                    break;

                diff = FindDividendScale(a);
            }

            return result;
        }

        private static decimal ToDecimalNormalized(long value, byte scale = Precision)
        {
            bool isNegative = value < 0;

            if (isNegative)
                value = -value;

            int lower = (int)value;
            int upper = (int)(value >> 32);

            return new decimal(lower, upper, 0, isNegative, scale);
        }

        private readonly struct AlreadyNormalized { }

        #endregion
    }
}