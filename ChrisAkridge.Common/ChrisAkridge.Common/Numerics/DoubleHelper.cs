using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Numerics
{
	public struct DoubleHelper
	{
		// https://ericlippert.com/2015/11/30/the-dedoublifier-part-one/
		// TODO: write tests for this
		public const int LowestNormalExponent = -1022;
		public const int HighestNormalExponent = 1023;

		public ulong RawBits { get; }

		// RawSign is 1 if zero or negative, 0 if zero or positive
		public int RawSign => (int)(RawBits >> 63);
		public int RawExponent => (int)(RawBits >> 52) & 0x7FF;
		public long RawMantissa => (long)(RawBits & 0x000FFFFFFFFFFFFF);
		public bool IsNaN => RawExponent == 0x7ff && RawMantissa != 0;
		public bool IsInfinity => RawExponent == 0x7ff && RawMantissa == 0;
		public bool IsZero => RawExponent == 0 && RawMantissa == 0;
		public bool IsDenormal => RawExponent == 0 && RawMantissa != 0;

		// Sign is 1 if positive, -1 if negative, 0 if zero
		public int Sign => (IsZero) ? 0 : 1 - RawSign * 2;
		public long Mantissa => (IsZero || IsDenormal || IsNaN || IsInfinity) ?
				RawMantissa :
				RawMantissa | 0x0010000000000000;

		public int Exponent
		{
			get
			{
				if (IsZero) { return 0; }
				else if (IsDenormal) { return -1074; }
				else if (IsNaN || IsInfinity) { return RawExponent; }
				else { return RawExponent - 1075; }
			}
		}

		public DoubleHelper(double d)
		{
			this.RawBits = (ulong)BitConverter.DoubleToInt64Bits(d);
		}
	}
}
