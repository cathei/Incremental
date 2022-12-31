// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.ComponentModel;
using System.Globalization;

namespace Cathei.Mathematics
{
    /// <summary>
    /// 16-byte deterministic floating point decimal type.
    /// </summary>
    [TypeConverter(typeof(IncrementalTypeConverter))]
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
        /// Maximum precision under decimal point with long type mantissa.
        /// </summary>
        public const int Precision = 16;

        /// <summary>
        /// Mantissa of 1.
        /// </summary>
        public const long Unit = 1_0000_0000_0000_0000L;

        /// <summary>
        /// Value of number 0.
        /// </summary>
        public static readonly Incremental Zero = new Incremental(0, 0, new AlreadyNormalized());

        /// <summary>
        /// Value of number 1.
        /// </summary>
        public static readonly Incremental One = new Incremental(Unit, 0, new AlreadyNormalized());

        /// <summary>
        /// Value of Ln 10.
        /// </summary>
        public static readonly Incremental Ln10 = new Incremental(
            23_025_850_929_940_457, 0, new AlreadyNormalized());

        /// <summary>
        /// Value of E (base of natural log).
        /// </summary>
        public static readonly Incremental E = new Incremental(
            27_182_818_284_590_452, 0, new AlreadyNormalized());

        public bool IsNegative => Mantissa < 0;

        public bool IsZero => Mantissa == 0;

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

            bool isNegative = mantissa < 0;
            ulong abs = (ulong)(isNegative ? -mantissa : mantissa);

            // normalize
            if (abs < Unit || abs >= Unit * 10)
            {
                var adjustment = Precision - Log10Int(abs);
                abs = MultiplyPow10(abs, adjustment);
                exponent -= adjustment;
            }

            Mantissa = isNegative ? -(long)abs : (long)abs;
            Exponent = exponent;
        }

        // Private constructor for the case we already know it's normalized
        private Incremental(long mantissa, long exponent, AlreadyNormalized _)
        {
            Mantissa = mantissa;
            Exponent = exponent;
        }

        #region Arithmetic operations

        public static Incremental Add(Incremental a, Incremental b)
        {
            // b should have bigger exponent
            if (a.Exponent > b.Exponent)
                (a, b) = (b, a);

            // zero has -Infinity exponent
            if (b.IsZero)
                return a;

            var exponentDiff = b.Exponent - a.Exponent;

            // over max significant digits, a will be ignored
            if (exponentDiff > Precision)
                return b;

            // match to bigger exponent
            long mantissa = b.Mantissa;

            if (a.IsNegative)
                mantissa -= (long)MultiplyPow10((ulong)-a.Mantissa, -(int)exponentDiff);
            else
                mantissa += (long)MultiplyPow10((ulong)a.Mantissa, -(int)exponentDiff);

            return new Incremental(mantissa, b.Exponent);
        }

        public static Incremental Subtract(in Incremental a, in Incremental b)
        {
            return Add(a, -b);
        }

        public static Incremental Multiply(in Incremental a, in Incremental b)
        {
            // rule out zero first
            if (a.IsZero || b.IsZero)
                return Zero;

            bool aNegative = a.IsNegative;
            bool bNegative = b.IsNegative;

            // since we use only 57 bits of 64 as mantissa we shift the bits for calculation
            // first we generate value of A / Unit
            // this point InverseUnitShift53 is shifted left by 53 + 64
            // we can safely shift A 7 bits for more precise result
            ulong result = MultiplyUInt64(
                (ulong)(aNegative ? -a.Mantissa : a.Mantissa) << 7,
                InverseUnitShift53);

            // we takes upper 64 bit, result is shifted left by 53 + 7
            // now safely shift B as well and multiply with result, take upper 64 bit
            result = MultiplyUInt64(
                (ulong)(bNegative ? -b.Mantissa : b.Mantissa) << 7,
                result);

            // now the result is shifted left by 3
            // round the result first: should be enough to just add constant 4 (0b100), to round from third bit
            // because we will shift down the result, exact lower bits wouldn't matter (not significant)
            long mantissa = (long)((result + 4) >> 3);
            long exponent = a.Exponent + b.Exponent;

            if (mantissa >= Unit * 10)
            {
                // in this case mantissa abs is between [Unit * 10, Unit * 100)
                mantissa /= 10;
                exponent++;
            }

            // apply sign
            if (aNegative ^ bNegative)
                mantissa = -mantissa;

            return new Incremental(mantissa, exponent, new AlreadyNormalized());
        }

        public static Incremental Divide(in Incremental a, in Incremental b)
        {
            if (b.IsZero)
                throw new DivideByZeroException();

            // rule out zero first
            if (a.IsZero)
                return Zero;

            bool aNegative = a.IsNegative;
            bool bNegative = b.IsNegative;

            long mantissa = (long)DivideUInt64(
                (ulong)(aNegative ? -a.Mantissa : a.Mantissa),
                (ulong)(bNegative ? -b.Mantissa : b.Mantissa));

            long exponent = a.Exponent - b.Exponent - 1;

            if (mantissa >= Unit * 10)
            {
                // in this case mantissa abs is between [Unit * 10, Unit * 100)
                mantissa /= 10;
                exponent++;
            }

            // apply sign
            if (aNegative ^ bNegative)
                mantissa = -mantissa;

            return new Incremental(mantissa, exponent, new AlreadyNormalized());
        }

        public static Incremental Negate(in Incremental value)
        {
            return new Incremental(-value.Mantissa, value.Exponent, new AlreadyNormalized());
        }

        public static Incremental operator +(in Incremental value) => value;
        public static Incremental operator -(in Incremental value) => Negate(value);

        public static Incremental operator +(in Incremental a, in Incremental b) => Add(a, b);
        public static Incremental operator -(in Incremental a, in Incremental b) => Subtract(a, b);
        public static Incremental operator *(in Incremental a, in Incremental b) => Multiply(a, b);
        public static Incremental operator /(in Incremental a, in Incremental b) => Divide(a, b);

        public static Incremental operator ++(in Incremental value) => value + One;
        public static Incremental operator --(in Incremental value) => value - One;

        public static Incremental operator >>(in Incremental value, int shift)
        {
            if (value.IsZero || shift < 0)
                return value;
            return new Incremental(value.Mantissa, value.Exponent - shift, new AlreadyNormalized());
        }

        public static Incremental operator <<(in Incremental value, int shift)
        {
            if (value.IsZero || shift < 0)
                return value;
            return new Incremental(value.Mantissa, value.Exponent + shift, new AlreadyNormalized());
        }

        #endregion

        #region Comparing operations

        public static bool operator ==(in Incremental a, in Incremental b)
        {
            return a.Mantissa == b.Mantissa && a.Exponent == b.Exponent;
        }

        public static bool operator !=(in Incremental a, in Incremental b) => !(a == b);

        public static bool operator <(in Incremental a, in Incremental b)
        {
            if (a.Mantissa > 0)
            {
                if (b.Mantissa <= 0)
                    return false;

                // both positive
                if (a.Exponent == b.Exponent)
                    return a.Mantissa < b.Mantissa;
                return a.Exponent < b.Exponent;
            }

            if (a.Mantissa < 0)
            {
                if (b.Mantissa >= 0)
                    return true;

                // both negative
                if (a.Exponent == b.Exponent)
                    return a.Mantissa < b.Mantissa;
                return a.Exponent > b.Exponent;
            }

            return b.Mantissa > 0;
        }

        public static bool operator >(in Incremental a, in Incremental b) => b < a;
        public static bool operator <=(in Incremental a, in Incremental b) => !(a > b);
        public static bool operator >=(in Incremental a, in Incremental b) => !(a < b);

        #endregion

        #region Conversion operators

        /// <summary>
        /// Convert from decimal.
        /// </summary>
        public static Incremental FromDecimal(decimal value)
        {
            const int scaleMask = 0x00FF0000;
            const int scaleShift = 16;

            var bits = decimal.GetBits(value);
            var exponent = (bits[3] & scaleMask) >> scaleShift; // extract exponent
            bool isNegative = value < 0;

            // create new decimal using same integer bits, but new scale
            var mantissa = new decimal(bits[0], bits[1], bits[2], false, 0);

            byte scale = 0;
            decimal threshold = Unit * 100L;

            // numbers with very long precision to fit in long type
            while (mantissa > threshold)
            {
                scale++;

                // 10 is max scale possible here
                if (scale >= 10)
                    break;

                threshold *= 10;
            }

            if (scale > 0)
            {
                mantissa = new decimal(bits[0], bits[1], bits[2], false, scale);
            }

            return new Incremental(
                isNegative ? -(long)mantissa : (long)mantissa,
                Precision - exponent + scale);
        }

        /// <summary>
        /// Conversion to decimal.
        /// Can throw OverflowException.
        /// </summary>
        public static decimal ToDecimal(in Incremental value)
        {
            if (value.Exponent < 0)
                return ToDecimalNormalized(value.Mantissa) / PowersOf10[-value.Exponent];
            if (value.Exponent <= Precision)
                return ToDecimalNormalized(value.Mantissa, (byte)(Precision - value.Exponent));
            if (value.Exponent < MaxPowersOf10Range + Precision + 1)
                return ToDecimalNormalized(value.Mantissa, 0) * PowersOf10[value.Exponent - Precision];
            throw new OverflowException();
        }

        /// <summary>
        /// Conversion to Int64.
        /// Can throw OverflowException.
        /// </summary>
        public static long ToInt64(in Incremental value)
        {
            if (value.Exponent < 0)
                return 0L;
            if (value.Exponent > 18)
                throw new OverflowException();
            if (value.Exponent >= Precision)
                return value.Mantissa * (long)PowersOf10[value.Exponent - Precision];
            return value.Mantissa / (long)PowersOf10[Precision - value.Exponent];
        }

        /// <summary>
        /// Convert from double.
        /// Keep in mind that you will lose determinism when you operate with double.
        /// For that reason, double conversions will be explicit.
        /// </summary>
        public static Incremental FromDouble(double value)
        {
            if (double.IsInfinity(value))
                throw new ArgumentException("Infinity is not supported", nameof(value));

            if (double.IsNaN(value))
                throw new ArgumentException("NaN is not supported", nameof(value));

            if (value == 0)
                return Zero;

            bool isNegative = value < 0;

            if (isNegative)
                value = -value;

            long exponent = (long)Math.Log10(value);
            long mantissa = (long)(value * Math.Pow(10, Precision - exponent));

            return new Incremental(isNegative ? -mantissa : mantissa, exponent);
        }

        /// <summary>
        /// Conversion to double.
        /// Keep in mind that you will lose determinism when you operate with double.
        /// Can throw OverflowException.
        /// </summary>
        public static double ToDouble(in Incremental value)
        {
            return value.Mantissa * Math.Pow(10, value.Exponent - Precision);
        }

        public static implicit operator Incremental(in decimal value) => FromDecimal(value);
        public static explicit operator decimal(in Incremental value) => ToDecimal(value);

        public static implicit operator Incremental(long value) => new Incremental(value, Precision);
        public static explicit operator long(in Incremental value) => ToInt64(value);

        public static implicit operator Incremental(int value) => new Incremental(value, Precision);
        public static explicit operator int(in Incremental value) => (int)ToInt64(value);

        public static explicit operator Incremental(double value) => FromDouble(value);
        public static explicit operator double(in Incremental value) => ToDouble(value);

        public static explicit operator Incremental(float value) => FromDouble(value);
        public static explicit operator float(in Incremental value) => (float)ToDouble(value);

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
            => new Incremental(Math.Abs(value.Mantissa), value.Exponent, new AlreadyNormalized());

        /// <summary>
        /// Returns natural logarithm of the value.
        /// </summary>
        public static Incremental Log(in Incremental value)
        {
            if (value.IsNegative)
                throw new ArgumentException("Log of negative value is complex number", nameof(value));

            if (value.IsZero)
                throw new OverflowException("Log of 0 is -Infinity");

            var offset = Ln10 * value.Exponent;

            // Log of 1 is 0, result is calculated with Ln10
            if (value.Mantissa == Unit)
                return offset;

            var x = new Incremental(value.Mantissa, 0, new AlreadyNormalized());

            // Fit value into 0 < x < 2 range
            if (value.Mantissa >= 2 * Unit)
            {
                x = new Incremental(x.Mantissa, -1, new AlreadyNormalized());
                offset += Ln10;

                // it is faster when x is closer to 1, but cannot exceed 2
                if (x.Mantissa <= 6 * Unit)
                {
                    x *= E;
                    offset--;
                }
            }

            return LogTaylorSeries(x) + offset;
        }

        /// <summary>
        /// Returns logarithm of the value in specific base.
        /// </summary>
        public static Incremental Log(in Incremental value, in Incremental @base)
        {
            if (@base == One)
                throw new ArgumentException("Log for base 1 is undefined", nameof(@base));

            return Log(value) / Log(@base);
        }

        /// <summary>
        /// Returns e raised to the power.
        /// </summary>
        public static Incremental Exp(Incremental power)
        {
            if (power.IsZero)
                return One;

            bool isNegative = power.IsNegative;

            if (isNegative)
                power = -power;

            Incremental result;

            if (power == One)
            {
                result = E;
            }
            else if (power < One)
            {
                result = ExpTaylorSeries(power);
            }
            else
            {
                var truncated = Truncate(power);

                if (truncated == power)
                {
                    result = ExpBySquaring(E, (long)truncated);
                }
                else
                {
                    result = ExpBySquaring(E, (long)truncated);
                    result *= ExpTaylorSeries(power - truncated);
                }
            }

            // reciprocal for negative value
            if (isNegative)
                return One / result;
            return result;
        }

        /// <summary>
        /// Returns value raised to the power.
        /// </summary>
        public static Incremental Pow(in Incremental value, in Incremental power)
        {
            if (value.IsNegative)
                throw new ArgumentException("Power of negative value is complex number");

            if (value == One || power.IsZero)
                return One;

            if (power == One)
                return value;

            if (value.IsZero)
            {
                if (power.IsNegative)
                    throw new ArgumentException("Negative power of 0 is Infinity");
                return Zero;
            }

            // a ^ b == e ^ (b ln a)
            return Exp(power * Log(value));
        }

        /// <summary>
        /// Returns power of 10.
        /// </summary>
        public static Incremental Pow10(long power)
            => new Incremental(Unit, power, new AlreadyNormalized());

        #endregion

        #region Rounding methods

        /// <summary>
        /// Truncate the value. If exponent is specified, the digit of 1E+exponent will be least significant.
        /// </summary>
        public static Incremental Truncate(in Incremental value, long exponent = 0)
        {
            if (exponent > value.Exponent)
                return Zero;

            long exponentDiff = Precision - value.Exponent + exponent;

            // over max significant digits
            if (exponentDiff < 0)
                return value;

            bool isNegative = value.IsNegative;
            ulong mantissa = (ulong)(isNegative ? -value.Mantissa : value.Mantissa);

            mantissa = MultiplyPow10(mantissa, -(int)exponentDiff);
            mantissa = MultiplyPow10(mantissa, (int)exponentDiff);

            return new Incremental(isNegative ? -(long)mantissa : (long)mantissa,
                value.Exponent, new AlreadyNormalized());
        }

        /// <summary>
        /// Round the value. If exponent is specified, the digit of 1E+exponent will be least significant.
        /// </summary>
        public static Incremental Round(in Incremental value, long exponent = 0)
        {
            // rounding away from zero
            var offset = new Incremental(
                (value.IsNegative ? -5 : 5) * Unit,
                exponent - 1, new AlreadyNormalized());

            return Truncate(value + offset, exponent);
        }

        /// <summary>
        /// Floor the value. If exponent is specified, the digit of 1E+exponent will be least significant.
        /// </summary>
        public static Incremental Floor(in Incremental value, long exponent = 0)
        {
            var truncated = Truncate(value, exponent);
            if (value.Mantissa >= 0)
                return truncated;

            if (value.Mantissa == truncated.Mantissa)
                return truncated;

            var offset = new Incremental(Unit, exponent, new AlreadyNormalized());
            return truncated - offset;
        }

        /// <summary>
        /// Ceiling the value. If exponent is specified, the digit of 1E+exponent will be least significant.
        /// </summary>
        public static Incremental Ceiling(in Incremental value, long exponent = 0)
        {
            var truncated = Truncate(value, exponent);
            if (value.Mantissa <= 0)
                return truncated;

            if (value.Mantissa == truncated.Mantissa)
                return truncated;

            var offset = new Incremental(Unit, exponent, new AlreadyNormalized());
            return truncated + offset;
        }

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

        public int CompareTo(Incremental other)
        {
            if (this == other)
                return 0;
            return this < other ? -1 : 1;
        }

        #endregion

        #region Format and parsing

        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        public string ToString(IFormatProvider provider)
        {
            // following same rule as
            // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
            // fixed-point notation
            if (Precision >= Exponent && Exponent > -5)
            {
                decimal converted = ToDecimal(this);
                return converted.ToString("G17", provider);
            }

            // scientific notation
            decimal mantissa = (decimal)Mantissa / Incremental.Unit;
            return mantissa.ToString("G17", provider) + Exponent.ToString("e+#;e-#;#", provider);
        }

        public static Incremental Parse(string value)
        {
            return Parse(value, CultureInfo.CurrentCulture);
        }

        public static Incremental Parse(string value, IFormatProvider provider)
        {
            string[] tokens = value.Split('e');

            // fixed-point notation
            if (tokens.Length == 1)
                return decimal.Parse(tokens[0], provider);

            // scientific notation
            long mantissa = (long)(decimal.Parse(tokens[0], provider) * Unit);
            long exponent = long.Parse(tokens[1], provider);
            return new Incremental(mantissa, exponent);
        }

        #endregion
    }
}