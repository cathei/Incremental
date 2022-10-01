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
        
        /// <summary>
        /// Pre-calculated 9,007,199,254,740,992 / 10,000,000,000,000,000 (Unit) value under decimal point.
        /// Used for multiplication.
        /// https://stackoverflow.com/questions/41183935/why-does-gcc-use-multiplication-by-a-strange-number-in-implementing-integer-divi
        /// </summary>
        private const ulong InverseUnitShift53 = 0xE695_94BE_C44D_E15B;

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
        
        // /// <summary>
        // /// Full multiply two ulong and takes upper 128 bits
        // /// Formula = (a * b) = ((aUpper + aLower) * (bUpper + bLower)) =
        // /// ((aUpper * bUpper) + (aUpper * bLower) + (aLower * bUpper) + (aLower * bLower)).
        // /// https://stackoverflow.com/questions/28868367/getting-the-high-part-of-64-bit-integer-multiplication
        // /// </summary>
        // private static (ulong hi, ulong lo) MultiplyUInt128(ulong a, ulong b)
        // {
        //     uint aUpper = (uint)(a >> 32);
        //     uint aLower = (uint)a;
        //     
        //     uint bUpper = (uint)(b >> 32);
        //     uint bLower = (uint)b;
        //
        //     ulong hi = MultiplyUInt32(aUpper, bUpper);
        //     ulong mid1 = MultiplyUInt32(aUpper, bLower);
        //     ulong mid2 = MultiplyUInt32(aLower, bUpper);
        //     ulong lo = MultiplyUInt32(aLower, bLower);
        //     
        //     ulong carry = ((ulong)(uint)mid1 + (ulong)(uint)mid2 + (lo >> 32)) >> 32;
        //
        //     lo += (mid1 << 32) + (mid2 << 32);
        //     hi += (mid1 >> 32) + (mid2 >> 32) + carry;
        //
        //     return (hi, lo);
        // }
        //
        // private static void Divide128(out (ulong upper, ulong lower) quotient, ref (ulong upper, ulong lower) value, UInt64 divider)
        // {
        //     quotient.upper = quotient.lower = 0;
        //     // var dneg = GetBitLength((UInt32)(divider >> 32));
        //     // var d = 32 - dneg;
        //     var vPrime = divider << d;
        //     var v1 = (UInt32)(vPrime >> 32);
        //     var v2 = (UInt32)vPrime;
        //     var r0 = value.r0;
        //     var r1 = value.r1;
        //     var r2 = value.r2;
        //     var r3 = value.r3;
        //     var r4 = (UInt32)0;
        //     // if (d != 0)
        //     // {
        //     //     r4 = r3 >> dneg;
        //     //     r3 = r3 << d | r2 >> dneg;
        //     //     r2 = r2 << d | r1 >> dneg;
        //     //     r1 = r1 << d | r0 >> dneg;
        //     //     r0 <<= d;
        //     // }
        //     quotient.lower = DivRem(r4, ref r3, ref r2, v1, v2);
        //     var q1 = DivRem(r3, ref r2, ref r1, v1, v2);
        //     var q0 = DivRem(r2, ref r1, ref r0, v1, v2);
        //     quotient.upper = (UInt64)q1 << 32 | q0;
        //     Debug.Assert((BigInteger)quotient == (BigInteger)value / divider);
        // }

        
        /// <summary>
        /// For normalized ulong, there is only 4 case
        /// </summary>
        private static short GetLeadingBitShiftLeft(ulong value)
        {
            if (value >= 0x0080_0000_0000_0000)
            {
                if (value >= 0x0100_0000_0000_0000)
                    return 56;
                else
                    return 55;
            }
            else
            {
                if (value >= 0x0040_0000_0000_0000)
                    return 54;
                else
                    return 53;
            }
        }
        
        /// <summary>
        /// Divide two normalized ulong and returns result mantissa.
        /// https://stackoverflow.com/questions/71440466/how-can-i-quickly-and-accurately-multiply-a-64-bit-integer-by-a-64-bit-fraction
        /// </summary>
        private static ulong DivideUInt64(ulong a, ulong b)
        {
            // int shiftA = GetLeadingBitShiftLeft(a);
            // int shiftB = GetLeadingBitShiftLeft(b);
            //
            // int shift;
            //
            // // alignment
            // if (shiftA < shiftB)
            // {
            //     a <<= shiftB - shiftA;
            //     shift = shiftB;
            // }
            // else
            // {
            //     b <<= shiftA - shiftB;
            //     shift = shiftA;
            // }
            //
            // ulong result = 0;
            //
            // while (shift > 0)
            // {
            //     if (a < b)
            //     {
            //         shift--;
            //         continue;
            //     }
            //
            //     a -= b;
            //     result |= (1u << shift);
            //
            //     if (a == 0)
            //         break;
            //
            //     a <<= 1;
            //     shift--;
            // }
            //
            // return result;


            ulong result = 0;
            int exponent = Precision;
            
            while (a > 0 && exponent > 0)
            {
                a *= 100;
                exponent -= 2;

                // mul and rem would be single operation in CPU
                ulong q = a / b;
                a %= b;
                
                result += q * (ulong)PowersOf10[exponent];
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