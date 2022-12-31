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

        static Incremental()
        {
            ulong value = 1;

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
        /// Binary search CLZ, Used for division.
        /// </summary>
        private static int CountLeadingZeros(ulong value)
        {
            int result = 0;

            if ((value & 0xFFFF_FFFF_0000_0000) == 0)
            {
                result += 32;
                value <<= 32;
            }

            if ((value & 0xFFFF_0000_0000_0000) == 0)
            {
                result += 16;
                value <<= 16;
            }

            if ((value & 0xFF00_0000_0000_0000) == 0)
            {
                result += 8;
                value <<= 8;
            }

            if ((value & 0xF000_0000_0000_0000) == 0)
            {
                result += 4;
                value <<= 4;
            }

            if ((value & 0xC000_0000_0000_0000) == 0)
            {
                result += 2;
                value <<= 2;
            }

            if ((value & 0x8000_0000_0000_0000) == 0)
                result++;

            return result;
        }

        /// <summary>
        /// Binary search CTZ, Used for division
        /// </summary>
        private static int CountTrailingZeros(ulong value)
        {
            int result = 0;

            if ((value & 0xFFFF_FFFF) == 0)
            {
                result += 32;
                value >>= 32;
            }

            if ((value & 0xFFFF) == 0)
            {
                result += 16;
                value >>= 16;
            }

            if ((value & 0x00FF) == 0)
            {
                result += 8;
                value >>= 8;
            }

            if ((value & 0x000F) == 0)
            {
                result += 4;
                value >>= 4;
            }

            if ((value & 0x0003) == 0)
            {
                result += 2;
                value >>= 2;
            }

            if ((value & 0x0001) == 0)
                result++;

            return result;
        }

        /// <summary>
        /// Divide two normalized ulong and returns result mantissa.
        /// This method produces value that has one less exponent of [Unit, Unit * 100) range.
        /// https://stackoverflow.com/questions/71440466/how-can-i-quickly-and-accurately-multiply-a-64-bit-integer-by-a-64-bit-fraction
        /// https://stackoverflow.com/questions/2566010/fastest-way-to-calculate-a-128-bit-integer-modulo-a-64-bit-integer
        /// </summary>
        private static ulong DivideUInt64(ulong dividend, ulong divisor)
        {
            ulong result = 0;

            // the result value will be shift by 60
            // we need 4 bits margin since result of division would take as high as 10 (b1010)
            int shift = 60;

            // remove trailing zeros of divisor
            int diff = CountTrailingZeros(divisor);

            divisor >>= diff;
            shift -= diff;

            // do partial division iteration
            while (true)
            {
                // this will be at least 7
                diff = CountLeadingZeros(dividend);

                // shift dividend to leftmost place
                dividend <<= diff;
                shift -= diff;

                // there is no Math.DivRem for UInt64?
                ulong quotient = dividend / divisor;
                dividend -= quotient * divisor;

                if (shift >= 0)
                    quotient <<= shift;
                else
                    quotient >>= -shift;

                // write to result
                result += quotient;

                if (dividend == 0 || shift <= 0)
                    break;
            }

            // do the 128 bit multiplication
            // 7 is maximum shift as the multiplier will be Unit * 10 * 128
            // value will be shifted left by 60 + 7, then take upper 64 bit
            result = MultiplyUInt64(result, ((ulong)Unit * 10) << 7);

            // now value is shifted by 3, round bit by adding 4 (0b100) then shift back
            return (result + 4) >> 3;
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

        /// <summary>
        /// Calculate logarithm with Taylor series.
        /// x must be a value between 0 and 2.
        /// </summary>
        private static Incremental LogTaylorSeries(in Incremental x)
        {
            var y = (x - One) / (x + One);
            var ySquare = y * y;

            int iteration = 1;
            var exp = y + y;
            var result = exp;

            while (true)
            {
                exp *= ySquare;
                var next = exp / (iteration * 2 + 1);

                // value is not significant anymore
                if (result.Exponent - next.Exponent > Precision)
                    break;

                result += next;
                iteration++;
            }

            return result;
        }

        /// <summary>
        /// Calculate exp by squaring, only works for integer power
        /// https://en.wikipedia.org/wiki/Exponentiation_by_squaring
        /// </summary>
        private static Incremental ExpBySquaring(Incremental value, long power)
        {
            var result = One;

            while (power > 0)
            {
                if ((power & 0x1) != 0)
                {
                    result *= value;
                }

                value *= value;
                power >>= 1;
            }

            return result;
        }

        /// <summary>
        /// Calculate exp with Taylor series
        /// Faster when power is closer to 0
        /// power must be between 0 and 1
        /// </summary>
        private static Incremental ExpTaylorSeries(in Incremental power)
        {
            int iteration = 2;
            var result = One + power;
            var next = power;

            while (true)
            {
                next *= power / iteration;

                // value is not significant anymore
                if (result.Exponent - next.Exponent > Precision)
                    break;

                result += next;
                iteration++;
            }

            return result;
        }

        private readonly struct AlreadyNormalized { }

        #endregion
    }
}