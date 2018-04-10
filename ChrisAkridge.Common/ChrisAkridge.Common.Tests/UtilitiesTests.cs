using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class UtilitiesTests
	{
		[TestMethod]
		public void FileSizeText_ZeroBytes()
		{
			Assert.AreEqual(Utilities.GetFileSizeText(0L), "0 bytes");
		}

		[TestMethod]
		public void FileSizeText_Bytes()
		{
			Assert.AreEqual(Utilities.GetFileSizeText(128L), "128 bytes");
		}

		[TestMethod]
		public void FileSizeText_Kilobytes()
		{
			Assert.AreEqual(Utilities.GetFileSizeText(131072L), "128 KB");
		}

		[TestMethod]
		public void FileSizeText_Megabytes()
		{
			Assert.AreEqual(Utilities.GetFileSizeText(134217728L), "128 MB");
		}

		[TestMethod]
		public void FileSizeTest_Gigabytes()
		{
			Assert.AreEqual(Utilities.GetFileSizeText(137438953472L), "128 GB");
		}
	}
}
