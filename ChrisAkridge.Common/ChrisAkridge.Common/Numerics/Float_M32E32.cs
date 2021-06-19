using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Numerics
{
	/// <summary>
	/// Represents a non-IEEE-compliant floating point number with 31-bit signed mantissa and a
	/// 31-bit signed exponent.
	/// </summary>
	public struct Float_M32E32 : IComparable<Float_M32E32>, IEquatable<Float_M32E32>
	{
		// Constants

		// Layout Fields

        // Properties
		public int Mantissa { get; }

        public int Exponent { get; }

        // Constructors
		public Float_M32E32(int mantissa, int exponent)
		{
			Mantissa = mantissa;
			Exponent = exponent;
		}

		public Float_M32E32(byte value) : this(value, 0) { }
		public Float_M32E32(sbyte value) : this(value, 0) { }
		public Float_M32E32(short value) : this(value, 0) { }
		public Float_M32E32(ushort value) : this(value, 0) { }
		public Float_M32E32(int value) : this(value, 0) { }

		public Float_M32E32(uint value)
		{
			if (value > int.MaxValue)
			{
				Mantissa = (int)(value >> 1);
				Exponent = 1;
			}
			else
			{
				Mantissa = (int)value;
				Exponent = 0;
			}
		}

		// Arithmetical Operations

		// Comparison and Equality Operations

		// Object Overrides

		// ToString/Parse/TryParse

		// Arithmetic Operator Overloads

		// Comparison Operator Overloads

		// Type Conversions

		// Interface Implementations
		public int CompareTo(Float_M32E32 other)
		{
			throw new NotImplementedException();
		}

		public bool Equals(Float_M32E32 other)
		{
			throw new NotImplementedException();
		}
	}
}
