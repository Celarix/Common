using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ChrisAkridge.Common.Extensions;
using ChrisAkridge.Common.Validation;

namespace ChrisAkridge.Common.Numerics
{
	/// <summary>
	/// Represents an arbitrary-precision decimal number as a big integer multiplied by a power
	/// of 10. Based on Jan Christoph Bernack's BigDecimal class (http://stackoverflow.com/a/4524254).
	/// </summary>
	[DebuggerDisplay("{ToInternalRepresentationString()}")]
	public struct BigDecimal : IComparable<BigDecimal>
	{
		private BigInteger mantissa;

        /// <summary>
		/// The maximum number of places that a division will be calculated to.
		/// </summary>
		public static int DivisionPrecision = 50;

		/// <summary>
		/// -1 as a BigDecimal.
		/// </summary>
		public static readonly BigDecimal MinusOne = new BigDecimal(-1, 0);

		/// <summary>
		/// 0 as a BigDecimal.
		/// </summary>
		public static readonly BigDecimal Zero = new BigDecimal(0, 0);

		/// <summary>
		/// 1 as a BigDecimal.
		/// </summary>
		public static readonly BigDecimal One = new BigDecimal(1, 0);

		/// <summary>
		/// Pi to 50 decimal places.
		/// </summary>
		public static readonly BigDecimal Pi =
			new BigDecimal(BigInteger.Parse("314159265358979323846264338327950288419716939937510"), -50);

		/// <summary>
		/// E to 50 decimal places.
		/// </summary>
		public static readonly BigDecimal E =
			new BigDecimal(BigInteger.Parse("271828182845904523536028747135266249775724709369995"), -50);

		/// <summary>
		/// Gets the mantissa - the raw digits of the number without the decimal point.
		/// </summary>
		public BigInteger Mantissa => mantissa;

		/// <summary>
		/// Gets the exponent - the power of 10 the mantissa is raised to.
		/// </summary>
		public int Exponent { get; private set; }

        // Constructors
		public BigDecimal(BigInteger mantissa, int exponent)
		{
			this.mantissa = mantissa;
			Exponent = exponent;

			Normalize();
		}

		// Parse/ToString
		private static bool ValidateParseString(string toParse)
		{
			if (string.IsNullOrEmpty(toParse)) { return false; }

			bool seenDecimalPoint = false;
			
			if (toParse.Contains('-') && toParse.LastIndexOf('-') != 0)
			{
				// Too many minus signs - there can only be one at the front
				return false;
			}

			if (toParse == ".") { return false; }

			foreach (char c in toParse)
			{
				if (!char.IsNumber(c) && c != '.' && c != '-') { return false; }

                if (c != '.') { continue; }

				if (!seenDecimalPoint) { seenDecimalPoint = true; }
                else { return false; }
            }

			return true;
		}

		public static bool TryParse(string s, out BigDecimal value)
		{
            value = new BigDecimal();
            
            if (!ValidateParseString(s))
            {
                value = Zero;
                return false;
            }

			int indexOfDecimalPoint = s.IndexOf('.');
			value.mantissa = BigInteger.Parse(s.Replace(".", ""));
			if (indexOfDecimalPoint == -1 || indexOfDecimalPoint == s.Length - 1)
			{
				// Number is an integer of the form "321" or "321."
				value.Exponent = 0;
			}
			else
			{
				// Number is a decimal of the form "321.987"
				// Each digit to the right of the decimal point is -1 to the exponent
				int decimalPlaces = s.Length - 1 - indexOfDecimalPoint;
				value.Exponent = -decimalPlaces;
			}

			value.Normalize();
			return true;
		}

		public static BigDecimal Parse(string s)
		{
            if (!TryParse(s, out var result))
			{
				throw new ArgumentException($"The input string {s} is not a valid decimal number.");
			}

			return result;
		}

		public override string ToString()
		{
			var mantissaString = mantissa.ToString(CultureInfo.InvariantCulture);
			if (Exponent == 0) { return mantissaString;  }

            if (Exponent > 0)
            {
                string trailingZeroes = new string('0', Exponent);
                return string.Concat(mantissaString, trailingZeroes);
            }

            if (Exponent < 0)
            {
                // Insert a decimal point such that -exponent digits are to the right of it
                string result = mantissaString;
                if (result.Length < -Exponent)
                {
                    result = string.Concat(new string('0', -Exponent - result.Length), result);
                }
                int insertDecimalAt = result.Length + Exponent;
                return result.Insert(insertDecimalAt, ".");
            }

            throw new UnreachableCodeException();
		}

		public string ToInternalRepresentationString() =>
            string.Concat(mantissa.ToString(CultureInfo.InvariantCulture), "e", Exponent);

        /// <summary>
		/// Removes trailing zeroes on the mantissa.
		/// </summary>
		private void Normalize()
		{
			if (mantissa.IsZero)
			{
				Exponent = 0;
			}
			else
			{
				BigInteger remainder = 0;
				while (remainder == 0)
				{
					var shortened = BigInteger.DivRem(mantissa, 10, out remainder);

                    if (remainder != 0) { continue; }
                    
                    mantissa = shortened;
                    Exponent++;
                }
			}
		}

		private BigDecimal Normalized()
		{
			Normalize();
			return this;
		}

		private static BigInteger AlignExponent(BigDecimal a, BigDecimal b) => 
			a.Mantissa * BigInteger.Pow(10, a.Exponent - b.Exponent);

		public static int NumberOfDigits(BigInteger value) =>
			(value * value.Sign).ToString().Length;

		// Arithmetic, mathematical methods
		public static BigDecimal Identity(BigDecimal value) => value;

		public static BigDecimal Inverse(BigDecimal value) => new BigDecimal(-value.mantissa, value.Exponent);

		public static BigDecimal Increment(BigDecimal value) => Add(value, 1);

		public static BigDecimal Decrement(BigDecimal value) => Subtract(value, 1);

		public static BigDecimal Factorial(BigDecimal value)
		{
			BigDecimal result = One;
			while (value.CompareTo(0) == 1)
			{
				result = Multiply(result, value);
				value = Decrement(value);
			}

			result.Normalize();
			return result;
		}

		public static BigDecimal Add(BigDecimal a, BigDecimal b) =>
            a.Exponent > b.Exponent
                ? new BigDecimal(AlignExponent(a, b) + b.Mantissa, b.Exponent)
                : new BigDecimal(AlignExponent(b, a) + a.Mantissa, a.Exponent);

        public static BigDecimal Subtract(BigDecimal a, BigDecimal b) => Add(a, Inverse(b));

		public static BigDecimal Multiply(BigDecimal a, BigDecimal b) =>
			new BigDecimal(a.Mantissa * b.Mantissa, a.Exponent + b.Exponent);

		public static BigDecimal Divide(BigDecimal a, BigDecimal b)
		{
			var exponentDifference = DivisionPrecision - (NumberOfDigits(a.Mantissa) - NumberOfDigits(b.Mantissa));
			if (exponentDifference < 0)
			{
				exponentDifference = 0;
			}

			a.mantissa *= BigInteger.Pow(10, exponentDifference);
			return new BigDecimal(a.mantissa / b.mantissa, a.Exponent - b.Exponent - exponentDifference);
		}

		public static BigDecimal Pow(double value, double power)
		{
			var tmp = One;
			while (Math.Abs(power) > 100)
			{
				var diff = power > 0 ? 100 : -100;
				tmp *= Math.Pow(value, diff);
				power -= diff;
			}
			return tmp * Math.Pow(value, power);
		}

		public static BigDecimal Exp(double value) => Pow(value, Math.E);
		
		public static double Log(BigDecimal value, double logBase)
		{
			var log10 = Log10(value);
			return log10 / Math.Log10(logBase);
		}
		
		public static double Log10(BigDecimal value)
		{
			var mantissaLog = BigInteger.Log10(value.Mantissa);
			return mantissaLog + value.Exponent;
		}

		// Ln

		public static BigDecimal Abs(BigDecimal value) =>
			new BigDecimal(value.mantissa < 0 ? -value.mantissa : value.mantissa, value.Exponent);

		// Floor
		// Ceiling

		// Operator overloads
		public static BigDecimal operator +(BigDecimal value) => Identity(value);
		public static BigDecimal operator -(BigDecimal value) => Inverse(value);
		public static BigDecimal operator ++(BigDecimal value) => Increment(value);
		public static BigDecimal operator --(BigDecimal value) => Increment(value);

		public static BigDecimal operator +(BigDecimal a, BigDecimal b) => Add(a, b);
		public static BigDecimal operator -(BigDecimal a, BigDecimal b) => Subtract(a, b);
		public static BigDecimal operator *(BigDecimal a, BigDecimal b) => Multiply(a, b);
		public static BigDecimal operator /(BigDecimal a, BigDecimal b) => Divide(a, b);

		public static bool operator <(BigDecimal a, BigDecimal b) => a.CompareTo(b) < 0;
		public static bool operator >(BigDecimal a, BigDecimal b) => a.CompareTo(b) > 0;
		public static bool operator ==(BigDecimal a, BigDecimal b) => a.CompareTo(b) == 0;
		public static bool operator !=(BigDecimal a, BigDecimal b) => a.CompareTo(b) != 0;
		public static bool operator <=(BigDecimal a, BigDecimal b) => a < b || a == b;
		public static bool operator >=(BigDecimal a, BigDecimal b) => a > b || a == b;

		// Interface methods
		public int CompareTo(BigDecimal other)
		{
			if (mantissa.Sign != other.mantissa.Sign)
			{
				return mantissa.Sign.CompareTo(other.mantissa.Sign);
			}

            if (Exponent == other.Exponent) { return mantissa.CompareTo(other.mantissa); }
            
            var thisLogEstimate = Mantissa.DigitCount() + Exponent;
            var otherLogEstimate = other.Mantissa.DigitCount() + other.Exponent;
            return thisLogEstimate.CompareTo(otherLogEstimate);

        }
		// IEquatable

		// Conversions and casts
		public static implicit operator BigDecimal(byte value) => new BigDecimal(value, 0);
		public static implicit operator BigDecimal(sbyte value) => new BigDecimal(value, 0);
		public static implicit operator BigDecimal(short value) => new BigDecimal(value, 0);
		public static implicit operator BigDecimal(ushort value) => new BigDecimal(value, 0);
		public static implicit operator BigDecimal(int value) => new BigDecimal(value, 0);
		public static implicit operator BigDecimal(uint value) => new BigDecimal(value, 0);
		public static implicit operator BigDecimal(long value) => new BigDecimal(value, 0);
		public static implicit operator BigDecimal(ulong value) => new BigDecimal(value, 0);
		public static implicit operator BigDecimal(BigInteger value) => new BigDecimal(value, 0);

		public static implicit operator BigDecimal(double value)
		{
			var mantissa = (BigInteger)value;
			var exponent = 0;
			double scaleFactor = 1d;
			while (Math.Abs((value * scaleFactor) - (double)mantissa) > 0)
			{
				exponent -= 1;
				scaleFactor *= 10;
				mantissa = (BigInteger)(value * scaleFactor);
			}

			return new BigDecimal(mantissa, exponent);
		}

		public static explicit operator double(BigDecimal value)
		{
			double mantissaDouble = (double)value.Mantissa;
			int exponent = value.Exponent;

            if (!double.IsPositiveInfinity(mantissaDouble) && !double.IsNegativeInfinity(mantissaDouble))
            {
                return mantissaDouble * Math.Pow(10d, exponent);
            }

            // Mantissa is too big to fit in the double
            // This is bad; we need to do a more expensive operation to get it to fit
            double mantissaLog10 = BigInteger.Log10(value.Mantissa);
            double valueLog10 = mantissaLog10 + value.Exponent;

            if (valueLog10 < -308) { return 0d; }

            if (valueLog10 > 308) { return double.PositiveInfinity; }

            int placesToGetInRange = (int)mantissaLog10 - 300;
            mantissaDouble = (double)(value.Mantissa / BigInteger.Parse("1" + new string('0', placesToGetInRange)));
            exponent += placesToGetInRange;

            return mantissaDouble * Math.Pow(10d, exponent);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is BigDecimal))
			{
				return false;
			}

			return (BigDecimal)obj == this;
		}

		public override int GetHashCode() => 137 ^ Exponent ^ mantissa.GetHashCode();
	}
}
