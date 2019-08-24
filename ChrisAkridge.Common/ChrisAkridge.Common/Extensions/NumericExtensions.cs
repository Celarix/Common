using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChrisAkridge.Common.Validation;

namespace ChrisAkridge.Common.Extensions
{
	public static class NumericExtensions
	{
		private static readonly float MinNormalFloat = BitConverter.ToSingle(new byte[] {0x00, 0x80, 0x00, 0x00}, 0);
		private static readonly double MinNormalDouble = BitConverter.Int64BitsToDouble(0x0010000000000000L);

		public static void Times(this int value, Action action)
		{
			Validate.Begin().IsPositive(value, nameof(value)).Check();

			for (int i = 0; i < value; i++)
			{
				action();
			}
		}

		public static void Times(this int value, Action<int> action)
		{
			Validate.Begin().IsPositive(value, nameof(value)).Check();

			for (int i = 0; i < value; i++)
			{
				action(i);
			}
		}

		public static bool NearlyEqual(this float a, float b, float epsilon)
		{
			// https://stackoverflow.com/a/4915891/2709212
			float absA = Math.Abs(a);
			float absB = Math.Abs(b);
			float diff = Math.Abs(a - b);

			if (a == b)
			{
				// Shortcut, handles infinities
				return true;
			}
			else if (a == 0 || b == 0 || diff < MinNormalFloat)
			{
				// a or b is zero or both are extremely close to it
				// relative error is less meaningful here
				return diff < (epsilon * MinNormalFloat);
			}
			else
			{
				if (float.IsInfinity(absA + absB))
				{
					return diff < epsilon;
				}
				return diff / (absA + absB) < epsilon;
			}
		}

		public static bool NearlyEqual(this double a, double b, double epsilon)
		{
			double absA = Math.Abs(a);
			double absB = Math.Abs(b);
			double diff = Math.Abs(a - b);

			if (a == b) { return true; }
			else if (a == 0d || b == 0d || diff < MinNormalDouble)
			{
				return diff < (epsilon * MinNormalDouble);
			}
			else
			{
				if (double.IsInfinity(absA + absB))
				{
					return diff < epsilon;
				}
				return diff / (absA + absB) < epsilon;
			}
		}

		public static bool BetweenInclusive(this float x, float a, float b)
		{
			if (a > b) { return (x >= a) && (x <= b); }
			else if (a < b) { return (x <= b) && (x >= a); }
			else if (a.NearlyEqual(b, 0.00001f))
			{
				return (x.NearlyEqual(a, 0.00001f)) && (x.NearlyEqual(b, 0.00001f));
			}
			return false;
		}

		public static int Clamp(this int value, int min, int max)
		{
			Validate.Begin().IsTrue(min < max, $"The minimum ({min}) is more than the maximum ({max}).")
				.Check();
			
			if (value < min) { return min; }
			else if (value > max) { return max; }
			return value;
		}

		public static uint Clamp(this uint value, uint min, uint max)
		{
			Validate.Begin().IsTrue(min < max, $"The minimum ({min}) is more than the maximum ({max}).")
				.Check();

			if (value < min) { return min; }
			else if (value > max) { return max; }
			return value;
		}

		public static long Clamp(this long value, long min, long max)
		{
			Validate.Begin().IsTrue(min < max, $"The minimum ({min}) is more than the maximum ({max}).")
				.Check();

			if (value < min) { return min; }
			else if (value > max) { return max; }
			return value;
		}

		public static ulong Clamp(this ulong value, ulong min, ulong max)
        {
            Validate.Begin().IsTrue(min < max, $"The minimum ({min}) is more than the maximum ({max}).")
                .Check();

            if (value < min) { return min; }
            else if (value > max) { return max; }
            return value;
        }

        public static float Clamp(this float value, float min, float max)
        {
            Validate.Begin().IsTrue(min < max, $"The minimum ({min}) is more than the maximum ({max}).")
                .Check();

            if (value < min) { return min; }
            else if (value > max) { return max; }
            return value;
        }

        public static double Clamp(this double value, double min, ulong max)
        {
            Validate.Begin().IsTrue(min < max, $"The minimum ({min}) is more than the maximum ({max}).")
                .Check();

            if (value < min) { return min; }
            else if (value > max) { return max; }
            return value;
        }

        public static decimal Clamp(this decimal value, decimal min, decimal max)
        {
            Validate.Begin().IsTrue(min < max, $"The minimum ({min}) is more than the maximum ({max}).")
                .Check();

            if (value < min) { return min; }
            else if (value > max) { return max; }
            return value;
        }

        /// <summary>
		///   Corrects the value of a single-precision float to the nearest
		///   integral value if the float is very close to that value.
		/// </summary>
		/// <param name="value">The value to correct.</param>
		/// <returns>
		///   A whole-number corrected value, or the value if it was not close
		///   enough to the nearest integers.
		/// </returns>
		public static float CorrectPrecision(this float value)
        {
            const float Epsilon = 0.0001f;

            int ceiling = (int)(value + 1f);
            int floor = (int)value;

            if (Math.Abs(ceiling - value) < Epsilon)
            {
                return ceiling;
            }
            else if (Math.Abs(value - floor) < Epsilon)
            {
                return floor;
            }
            else
            {
                return value;
            }
        }
    }
}
