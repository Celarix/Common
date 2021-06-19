using System;
using System.Numerics;

namespace ChrisAkridge.Common.Extensions
{
	public static class BigIntegerExtensions
	{
		public static int DigitCount(this BigInteger value)
		{
			// https://stackoverflow.com/a/34052627/2709212
			// TODO: add test method

			return (int)Math.Floor(BigInteger.Log10(value) + 1);
		}
	}
}
