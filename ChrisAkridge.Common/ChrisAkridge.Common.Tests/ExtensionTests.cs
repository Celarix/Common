using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common.Extensions;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class ExtensionTests
	{
		[TestMethod]
		public void TimesWithoutParam()
		{
			int i = 0;
			5.Times(() => i++);
			Assert.AreEqual(5, i);
		}

		[TestMethod]
		public void TimesWithParam()
		{
			int[] expected = { 0, 1, 2, 3, 4 };
			int[] actual = new int[5];

			5.Times(i => actual[i] = i);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void BetweenInclusiveTests()
		{
			Assert.IsTrue(1f.BetweenInclusive(0f, 2f));
			Assert.IsTrue(1f.BetweenInclusive(1f, 2f));
			Assert.IsFalse((-1f).BetweenInclusive(0f, 2f));
			Assert.IsFalse((-float.Epsilon).BetweenInclusive(0f, 2f));
		}

		[TestMethod]
		public void ClampIntTests()
		{
			Assert.AreEqual(2, 2.Clamp(0, 5));
			Assert.AreEqual(0, (-500).Clamp(0, 5));
			Assert.AreEqual(5, 500.Clamp(0, 5));
		}

		[TestMethod]
		public void ClampUIntTests()
		{
			Assert.AreEqual(2u, 2u.Clamp(0u, 5u));
			Assert.AreEqual(200u, (500u).Clamp(100u, 200u));
			Assert.AreEqual(5u, 500u.Clamp(0u, 5u));
		}

		[TestMethod]
		public void HighestSetBitByteTests()
		{
			Assert.AreEqual(7, ((byte)1).HighestSetBit());
			Assert.AreEqual(0, ((byte)255).HighestSetBit());
			Assert.AreEqual(3, ((byte)31).HighestSetBit());
			Assert.AreEqual(-1, ((byte)0).HighestSetBit());
		}

		[TestMethod]
		public void HighestSetBitUShortTests()
		{
			Assert.AreEqual(15, ((ushort)1).HighestSetBit());
			Assert.AreEqual(0, ((ushort)65535).HighestSetBit());
			Assert.AreEqual(6, ((ushort)543).HighestSetBit());
			Assert.AreEqual(-1, ((byte)0).HighestSetBit());
		}

		[TestMethod]
		public void HighestSetBitUIntTests()
		{
			Assert.AreEqual(31, 1u.HighestSetBit());
			Assert.AreEqual(0, uint.MaxValue.HighestSetBit());
			Assert.AreEqual(1, 1073742367u.HighestSetBit());
			Assert.AreEqual(-1, 0u.HighestSetBit());
		}

		[TestMethod]
		public void HighestSetBitULongTests()
		{
			Assert.AreEqual(63, 1UL.HighestSetBit());
			Assert.AreEqual(0, uint.MaxValue.HighestSetBit());
			Assert.AreEqual(2, 2305843010287436319UL.HighestSetBit());
			Assert.AreEqual(-1, 0u.HighestSetBit());
		}
	}
}
