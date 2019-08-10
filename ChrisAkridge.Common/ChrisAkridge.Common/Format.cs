using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChrisAkridge.Common.Validation;

namespace ChrisAkridge.Common
{
	/// <summary>
	/// Provides methods that format numeric and textual data.
	/// </summary>
	public static class Format
	{
		private const float SIDivisor = 1000f;
		private const float PowerOfTwoDivisor = 1024f;
		private const float FileSizePrefixCount = 8;

		private static string[] fileSizePrefixes = new[] { "", "kilo", "mega", "giga", "tera", "peta", "exa", "zetta", "yotta" };
		private static string[] fileSizeIECPrefixes = new[] { "", "kibi", "mebi", "gibi", "tebi", "pebi", "exbi", "zebi", "yobi" };
		private static string[] fileSizeAbbreviatedPrefixes = new[] { "", "k", "m", "g", "t", "p", "e", "z", "y" };
		private static string[] fileSizeAbbreviatedCapitalPrefixes = new[] { "", "K", "M", "G", "T", "P", "E", "Z", "Y" };

		public static string FileSize(long fileSize)
		{
			return FileSize(fileSize, FileSizeUnit.Byte, FileSizeDivisorType.PowerOfTwo, true, 2);
		}

		public static string FileSize(long fileSize, FileSizeUnit unit, FileSizeDivisorType divisorType,
			bool abbreviated, int decimalPlaces)
		{
			Validate.Begin().IsValidEnumValue(unit, nameof(unit))
				.IsValidEnumValue(divisorType, nameof(divisorType))
				.InRangeInclusive(0, 28, decimalPlaces, nameof(decimalPlaces)).Check();

			// Given a file of 1,048,576 bytes at 2 decimal places:
			// SI bits:              8.39 megabits  / 8.39 mb
			// SI bytes:             1.05 megabytes / 1.05 mB
			// Power of 2 bits:      8.00 megabits  / 8.00 Mb
			// Power of 2 bytes:     1.00 megabytes / 1.00 MB
			// IEC power of 2 bits:  8.00 mebibits  / 8.00 Mb
			// IEC power of 2 bytes: 1.00 mebibytes / 1.00 MB
			
			// If the file size is to be expressed in bits, we can only format sizes up to
			// +/-1 exabyte.
			if (unit == FileSizeUnit.Bit && ((fileSize > (long.MaxValue / 8)) ||(fileSize < (long.MinValue / 8))))
			{
				throw new ArgumentOutOfRangeException(nameof(unit), $"The provided file size {fileSize} was too large to be expressed as bits (maximum {long.MaxValue / 8} bytes).");
			}

			if (unit == FileSizeUnit.Bit) { fileSize *= 8; }

			// If the file size is negative, we'll flip the sign so the math below works out.
			// Be sure to remember that the file size is negative so we can prepend the dash at the
			// end.
			bool negative = fileSize < 0L;
			if (negative && fileSize == long.MinValue)
			{
				// The inverse of the lowest possible long value cannot be expressed, so throw.
				// (yes, I know there's a better solution)
				// TODO: actually do this better solution - you know it's about -16 EiB, so write it like that
				throw new ArgumentOutOfRangeException(nameof(fileSize), "The file size is too low to express.");
			}
			if (negative) { fileSize = -fileSize; }

			string suffix = FileSizeMakeSuffix(unit, abbreviated);
			float divisor = (divisorType == FileSizeDivisorType.SI) ? SIDivisor : PowerOfTwoDivisor;
			float mantissa = fileSize;
			int divisionCount = 0;

			while (mantissa >= divisor && divisionCount <= FileSizePrefixCount)
			{
				mantissa /= divisor;
				divisionCount++;
			}

			string[] prefixes = null;
			if (divisorType == FileSizeDivisorType.SI)
			{
				if (abbreviated) { prefixes = fileSizeAbbreviatedPrefixes; }
				else { prefixes = fileSizePrefixes; }
			}
			else if (divisorType == FileSizeDivisorType.PowerOfTwo)
			{
				if (abbreviated) { prefixes = fileSizeAbbreviatedCapitalPrefixes; }
				else { prefixes = fileSizePrefixes; }
			}
			else if (divisorType == FileSizeDivisorType.IECPowerOfTwo)
			{
				if (abbreviated) { prefixes = fileSizeAbbreviatedCapitalPrefixes; }
				else { prefixes = fileSizeIECPrefixes; }
			}
			
			var fullSuffix = prefixes[divisionCount] + suffix;
			var formattedMantissa = $"{decimal.Round((decimal)mantissa, decimalPlaces)}";
			if (negative) { formattedMantissa = "-" + formattedMantissa; }

			return $"{formattedMantissa} {fullSuffix}";
		}

		public static string PixelCount(ulong pixelCount, int decimalPlaces = 2)
		{
			float mantissa = pixelCount;
			int divisionCount = 0;

			while (mantissa >= 1000f && divisionCount < 8)
			{
				mantissa /= 1000f;
				divisionCount++;
			}

			string suffix = fileSizePrefixes[divisionCount] + "pixels";
			var formattedMantissa = $"{decimal.Round((decimal)mantissa, decimalPlaces)}";

			return $"{formattedMantissa} {suffix}";
		}

		private static string FileSizeMakeSuffix(FileSizeUnit unit, bool abbreviated)
		{
			switch (unit)
			{
				case FileSizeUnit.Bit:
					return (abbreviated) ? "b" : "bits";
				case FileSizeUnit.Byte:
					return (abbreviated) ? "B" : "bytes";
				default:
					throw new ArgumentException($"Invalid FileSizeUnit value {(int)unit}.");
			}
		}

		// Pixel count formatter
	}
}
