using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class FormatTests
	{
		[TestMethod]
		public void FileSize_SIBitsUnabbreviated()
		{
			long fileSize = 1048576L;
			string expected = "8.39 megabits";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Bit, FileSizeDivisorType.SI, false, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_SIBitsAbbreviated()
		{
			long fileSize = 1048576L;
			string expected = "8.39 mb";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Bit, FileSizeDivisorType.SI, true, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_SIBytesUnabbreviated()
		{
			long fileSize = 1048576L;
			string expected = "1.05 megabytes";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Byte, FileSizeDivisorType.SI, false, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_SIBytesAbbreviated()
		{
			long fileSize = 1048576L;
			string expected = "1.05 mB";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Byte, FileSizeDivisorType.SI, true, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_PowerOf2BitsUnabbreviated()
		{
			long fileSize = 1048576L;
			string expected = "8 megabits";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Bit, FileSizeDivisorType.PowerOfTwo, false, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_PowerOf2BitsAbbreviated()
		{
			long fileSize = 1048576L;
			string expected = "8 Mb";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Bit, FileSizeDivisorType.PowerOfTwo, true, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_PowerOf2BytesUnabbreviated()
		{
			long fileSize = 1048576L;
			string expected = "1 megabytes";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Byte, FileSizeDivisorType.PowerOfTwo, false, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_PowerOf2BytesAbbreviated()
		{
			long fileSize = 1048576L;
			string expected = "1 MB";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Byte, FileSizeDivisorType.PowerOfTwo, true, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_IECBitsUnabbreviated()
		{
			long fileSize = 1048576L;
			string expected = "8 mebibits";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Bit, FileSizeDivisorType.IECPowerOfTwo, false, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_IECBitsAbbreviated()
		{
			long fileSize = 1048576L;
			string expected = "8 Mb";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Bit, FileSizeDivisorType.IECPowerOfTwo, true, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_IECBytesUnabbreviated()
		{
			long fileSize = 1048576L;
			string expected = "1 mebibytes";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Byte, FileSizeDivisorType.IECPowerOfTwo, false, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FileSize_IECBytesAbbreviated()
		{
			long fileSize = 1048576L;
			string expected = "1 MB";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Byte, FileSizeDivisorType.IECPowerOfTwo, true, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NegativeFileSize()
		{
			long fileSize = -1048576L;
			string expected = "-1 MB";
			string actual = Format.FileSize(fileSize, FileSizeUnit.Byte, FileSizeDivisorType.PowerOfTwo, true, 2);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void PositiveOutOfRangeFileSizeFails()
		{
			long fileSize = (long.MaxValue / 8) + 1;
			Format.FileSize(fileSize, FileSizeUnit.Bit, FileSizeDivisorType.IECPowerOfTwo, true, 2);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void NegativeOutOfRangeFileSizeFails()
		{
			long fileSize = (long.MinValue / 8) - 1;
			Format.FileSize(fileSize, FileSizeUnit.Bit, FileSizeDivisorType.IECPowerOfTwo, true, 2);
		}
	}
}
