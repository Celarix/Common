namespace ChrisAkridge.Common.Extensions
{
	public static class BitwiseExtensions
	{
		/// <summary>
		/// Returns the position of the highest '1' bit in a byte, where 0 is the highest bit, and
		/// 7 is the lowest bit.
		/// </summary>
		/// <param name="value">The byte to get the highest bit for.</param>
		/// <returns>A number between 0 to 7, or -1 if no bit is set.</returns>
		public static int HighestSetBit(this byte value)
		{
			if (value == 0) { return -1; }

			for (int i = 0; i < 8; i++)
			{
				byte bitToCheck = (byte)(1 << (7 - i));
				if ((value & bitToCheck) != 0) { return i; }
			}

			throw new UnreachableCodeException();
		}

		/// <summary>
		/// Returns the position of the highest '1' bit in a unsigned short, where 0 is the highest
		///	bit, and 15 is the lowest bit.
		/// </summary>
		/// <param name="value">The byte to get the highest bit for.</param>
		/// <returns>A number between 0 to 15, or -1 if no bit is set.</returns>
		public static int HighestSetBit(this ushort value)
		{
			if (value == 0) { return -1; }

			for (int i = 0; i < 16; i++)
			{
				ushort bitToCheck = (ushort)(1 << (15 - i));
				if ((value & bitToCheck) != 0) { return i; }
			}

			throw new UnreachableCodeException();
		}

		/// <summary>
		/// Returns the position of the highest '1' bit in a unsigned integer, where 0 is the highest
		///	bit, and 31 is the lowest bit.
		/// </summary>
		/// <param name="value">The byte to get the highest bit for.</param>
		/// <returns>A number between 0 to 31, or -1 if no bit is set.</returns>
		public static int HighestSetBit(this uint value)
		{
			if (value == 0) { return -1; }

			for (int i = 0; i < 32; i++)
			{
				uint bitToCheck = (uint)(1 << (31 - i));
				if ((value & bitToCheck) != 0) { return i; }
			}

			throw new UnreachableCodeException();
		}

		/// <summary>
		/// Returns the position of the highest '1' bit in a unsigned long, where 0 is the highest
		///	bit, and 63 is the lowest bit.
		/// </summary>
		/// <param name="value">The byte to get the highest bit for.</param>
		/// <returns>A number between 0 to 31, or -1 if no bit is set.</returns>
		public static int HighestSetBit(this ulong value)
		{
			if (value == 0) { return -1; }

			for (int i = 0; i < 64; i++)
			{
				ulong bitToCheck = 1UL << (63 - i);
				if ((value & bitToCheck) != 0) { return i; }
			}

			throw new UnreachableCodeException();
		}
	}
}
