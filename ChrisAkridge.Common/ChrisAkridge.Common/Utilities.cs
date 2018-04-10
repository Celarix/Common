using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common
{
	public static class Utilities
	{
		/// <summary>
		/// Converts a file size in bytes to a textual form (for instance, 128 MB).
		/// </summary>
		/// <param name="fileSize">The size of the file or files in bytes.</param>
		/// <returns>A string stating the size of the file or files.</returns>
		/// <remarks>Each unit is equal to 1,024 times the size of the last unit.</remarks>
		public static string GetFileSizeText(long fileSize)
		{
			bool negative = fileSize < 0L;
			fileSize = (negative) ? -fileSize : fileSize;
			string result = null;

			if (fileSize < 1024L)
			{
				result = $"{fileSize} bytes";
				if (negative) { return $"-{result}"; }
				else { return result; }
			}

			char[] prefixes = { 'K', 'M', 'G', 'T', 'P', 'E', 'Z', 'Y' };
			double fileSizeAsDouble = fileSize;
			fileSizeAsDouble /= 1024d;
			int prefixIndex = 0;

			while (fileSizeAsDouble > 1024d)
			{
				fileSizeAsDouble /= 1024d;
				prefixIndex++;
			}

			fileSizeAsDouble = Math.Round(fileSizeAsDouble, 2);
			result = $"{fileSizeAsDouble} {prefixes[prefixIndex]}B";
			if (negative) { return $"-{result}"; }
			else { return result; }
		}

		public static string Pluralize(int count, string singularForm, string pluralForm)
		{
			if (count == 1) { return singularForm; }
			return pluralForm;
		}
	}
}
