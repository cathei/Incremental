// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Globalization;

namespace Cathei.Mathematics
{
    /// <summary>
    /// 16-byte deterministic floating point decimal type.
    /// </summary>
    public readonly struct Incremental : IEquatable<Incremental>, IComparable<Incremental>
    {
        public readonly long Mantissa;
        public readonly long Exponent;

        public static readonly Incremental Zero = new Incremental(0, 0);
        public static readonly Incremental One = new Incremental(Unit, 0);

        /// <summary>
        /// Maximum precision under decimal point with long type mantissa.
        /// </summary>
        public const int Precision = 16;

        /// <summary>
        /// Mantissa of 1.
        /// </summary>
        public const long Unit = 1_0000_0000_0000_0000;

        /// <summary>
        /// Square root of Unit.
        /// </summary>
        public const long UnitSqrt = 1_0000_0000;

        public const int MaxPowersOf10Range = 20;

        /// <summary>
        /// Lookup table for power of 10s.
        /// </summary>
        public static readonly long[] PowersOf10 = new long[MaxPowersOf10Range];

        static Incremental()
        {
            long value = 1;

            for (int i = 0; i < MaxPowersOf10Range; ++i)
            {
                PowersOf10[i] = value;
                value *= 10;
            }
        }

        public Incremental(long mantissa, long exponent)
        {
            // zero
            if (mantissa == 0)
            {
                this.Mantissa = 0;
                this.Exponent = 0;
                return;
            }

            long abs = Math.Abs(mantissa);

            // normalize
            if (abs < Unit)
            {
                var adjustment = Precision - Log10(abs);
                mantissa *= PowersOf10[adjustment];
                exponent -= adjustment;
            }
            else if (abs >= Unit * 10)
            {
                var adjustment = Log10(abs) - Precision;
                mantissa /= PowersOf10[adjustment];
                exponent += adjustment;
            }

            this.Mantissa = mantissa;
            this.Exponent = exponent;
        }

        #region Arithmetic operations

        public static Incremental Add(Incremental a, Incremental b)
        {
            // b should have bigger exponent
            if (a.Exponent > b.Exponent)
                (a, b) = (b, a);

            var exponentDiff = b.Exponent - a.Exponent;

            // over max significant digits, a will be ignored
            if (exponentDiff > Precision)
                return b;

            // match to bigger exponent
            var mantissa = b.Mantissa;
            mantissa += a.Mantissa / PowersOf10[exponentDiff];

            return new Incremental(mantissa, b.Exponent);
        }

        public static Incremental Subtract(in Incremental a, in Incremental b)
        {
            return Add(a, -b);
        }

        public static Incremental Multiply(in Incremental a, in Incremental b)
        {
            var mantissa = (a.Mantissa / UnitSqrt) * (b.Mantissa / UnitSqrt);
            var exponent = a.Exponent + b.Exponent;

            return new Incremental(mantissa, exponent);
        }

        public static Incremental Divide(in Incremental a, in Incremental b)
        {
            if (b.Mantissa == 0)
                throw new DivideByZeroException();

            var mantissa = a.Mantissa / (b.Mantissa / UnitSqrt);
            var exponent = a.Exponent - b.Exponent + Precision / 2;

            return new Incremental(mantissa, exponent);
        }

        public static Incremental Negate(in Incremental value)
        {
            return new Incremental(-value.Mantissa, value.Exponent);
        }

        public static Incremental operator +(in Incremental value) => value;
        public static Incremental operator -(in Incremental value) => Negate(value);

        public static Incremental operator +(in Incremental a, in Incremental b) => Add(a, b);
        public static Incremental operator -(in Incremental a, in Incremental b) => Subtract(a, b);
        public static Incremental operator *(in Incremental a, in Incremental b) => Multiply(a, b);
        public static Incremental operator /(in Incremental a, in Incremental b) => Divide(a, b);

        #endregion

        #region Comparing operations

        public static bool operator ==(in Incremental a, in Incremental b)
        {
            return a.Mantissa == b.Mantissa && a.Exponent == b.Exponent;
        }

        public static bool operator !=(in Incremental a, in Incremental b) => !(a == b);

        public static bool operator <(in Incremental a, in Incremental b)
        {
            if (a.Mantissa >= 0)
            {
                if (b.Mantissa <= 0)
                    return false;

                // both positive
                if (a.Exponent == b.Exponent)
                    return a.Mantissa < b.Mantissa;
                return a.Exponent < b.Exponent;
            }
            else
            {
                if (b.Mantissa >= 0)
                    return true;

                // both negative
                if (a.Exponent == b.Exponent)
                    return a.Mantissa > b.Mantissa;
                return a.Exponent > b.Exponent;
            }
        }

        public static bool operator >(in Incremental a, in Incremental b) => b < a;
        public static bool operator <=(in Incremental a, in Incremental b) => !(a > b);
        public static bool operator >=(in Incremental a, in Incremental b) => !(a < b);

        #endregion

        #region Conversion operators

        public static implicit operator Incremental(int value) => new Incremental(value, Precision);
        public static implicit operator Incremental(long value) => new Incremental(value, Precision);

        public static implicit operator Incremental(decimal value)
        {
            // not preserving precision but quick
            return new Incremental((long)(value * UnitSqrt), Precision / 2);
        }

        #endregion

        #region Math utilities

        /// <summary>
        /// Returns smaller value.
        /// </summary>
        public static Incremental Min(in Incremental a, in Incremental b) => a < b ? a : b;

        /// <summary>
        /// Returns bigger value.
        /// </summary>
        public static Incremental Max(in Incremental a, in Incremental b) => a > b ? a : b;

        #endregion

        #region Override methods

        public bool Equals(Incremental other) => this == other;

        public override bool Equals(object obj)
        {
            return obj is Incremental other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Mantissa.GetHashCode() ^ Exponent.GetHashCode();
        }

        public override string ToString()
        {
            // For debug purpose
            return (Mantissa * (decimal)Math.Pow(10, Exponent - Precision)).ToString(CultureInfo.InvariantCulture);
        }

        public int CompareTo(Incremental other)
        {
            if (this == other)
                return 0;
            return this < other ? -1 : 1;
        }

        #endregion

        private static int Log10(long value)
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
    }
}