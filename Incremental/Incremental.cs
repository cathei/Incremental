// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Globalization;

namespace Cathei.Mathematics
{
    /// <summary>
    /// 16-byte deterministic floating point decimal type.
    /// </summary>
    public readonly partial struct Incremental : IEquatable<Incremental>, IComparable<Incremental>
    {
        /// <summary>
        /// Decimal part of the value.
        /// Mantissa is always normalized in range of [Unit, Unit * 10), unless zero.
        /// </summary>
        public readonly long Mantissa;

        /// <summary>
        /// Exponential part of the value.
        /// If Mantissa is 0, Exponent is always 0.
        /// </summary>
        public readonly long Exponent;

        /// <summary>
        /// Construct Incremental value manually. The value will be normalized.
        /// (Unit, 0) will be same as (1, Precision).
        /// </summary>
        /// <param name="mantissa">Decimal part of the value.</param>
        /// <param name="exponent">Exponential part of the value.</param>
        public Incremental(long mantissa, long exponent)
        {
            // zero
            if (mantissa == 0)
            {
                Mantissa = Exponent = 0;
                return;
            }

            long abs = Math.Abs(mantissa);

            // normalize
            if (abs < Unit)
            {
                var adjustment = Precision - Log10Int(abs);
                mantissa *= PowersOf10[adjustment];
                exponent -= adjustment;
            }
            else if (abs >= Unit * 10)
            {
                var adjustment = Log10Int(abs) - Precision;
                mantissa /= PowersOf10[adjustment];
                exponent += adjustment;
            }

            Mantissa = mantissa;
            Exponent = exponent;
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
            // calculate in decimal
            var mantissa = (decimal)a.Mantissa / Unit * b.Mantissa;
            var exponent = a.Exponent + b.Exponent;

            return new Incremental((long)mantissa, exponent);
        }

        public static Incremental Divide(in Incremental a, in Incremental b)
        {
            if (b.Mantissa == 0)
                throw new DivideByZeroException();

            // calculate in decimal
            var mantissa = (decimal)a.Mantissa / b.Mantissa * Unit;
            var exponent = a.Exponent - b.Exponent;

            return new Incremental((long)mantissa, exponent);
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

        public static Incremental operator ++(in Incremental value) => value + One;
        public static Incremental operator --(in Incremental value) => value - One;

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

        public static implicit operator Incremental(in decimal value)
        {
            const int scaleMask = 0x00FF0000;
            const int scaleShift = 16;

            var bits = decimal.GetBits(value);
            var exponent = (bits[3] & scaleMask) >> scaleShift; // extract exponent

            // create new decimal using same integer bits, but new scale
            var mantissa = new decimal(bits[0], bits[1], bits[2], value < 0, Precision);
            return new Incremental((long)mantissa, exponent);
        }

        /// <summary>
        /// Conversion to decimal.
        /// Can throw OverflowException.
        /// </summary>
        public static decimal ToDecimal(in Incremental value)
        {
            if (value.Exponent >= MaxPowersOf10Range)
                throw new OverflowException();
            return (decimal)value.Mantissa / Unit * PowersOf10[value.Exponent];
        }

        public static explicit operator decimal(in Incremental value) => ToDecimal(value);
        public static explicit operator long(in Incremental value) => (long)ToDecimal(value);
        public static explicit operator int(in Incremental value) => (int)ToDecimal(value);

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


        /// <summary>
        /// Returns absolute value.
        /// </summary>
        public static Incremental Abs(in Incremental value)
            => new Incremental(Math.Abs(value.Mantissa), value.Exponent);

        // /// <summary>
        // /// Returns Log10 value. (undefined when value is not positive)
        // /// </summary>
        // public static Incremental Log10(in Incremental value)
        // {
        //
        // }

        /// <summary>
        /// Returns power of 10.
        /// </summary>
        public static Incremental Pow10(long power) => new Incremental(Unit, power);

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
            return $"{(decimal)Mantissa / Unit:G}{Exponent:e+#;e-#;#}";
        }

        public int CompareTo(Incremental other)
        {
            if (this == other)
                return 0;
            return this < other ? -1 : 1;
        }

        #endregion
    }
}