using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common
{
	/// <summary>
	/// Enumerates the unit used to format file sizes.
	/// </summary>
	public enum FileSizeUnit
	{
		/// <summary>
		/// Formatted file sizes will be displayed in bits.
		/// </summary>
		Bit,

		/// <summary>
		/// Formatted file sizes will be displayed in bytes.
		/// </summary>
		Byte
	}

	/// <summary>
	/// Enumerates the divisor used to display file sizes in scientific notation.
	/// </summary>
	public enum FileSizeDivisorType
	{
		/// <summary>
		/// Formatted file sizes will be divided by a factor of 1,000.
		/// </summary>
		SI,

		/// <summary>
		/// Formatted file sizes will be divided by a factor of 1,024.
		/// </summary>
		PowerOfTwo,

		/// <summary>
		/// Formatted file sizes will be divided by a factor of 1,024 and will use the IEC-approved
		/// names (kibi-, mebi-, etc.).
		/// </summary>
		IECPowerOfTwo
	}
}
