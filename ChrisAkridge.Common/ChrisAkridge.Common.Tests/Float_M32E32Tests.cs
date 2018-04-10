using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common.Numerics;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class Float_M32E32Tests
	{
		[TestMethod]
		public void ParameterlessConstructorTest()
		{
			var value = new Float_M32E32();
			Assert.AreEqual(0, value.Mantissa);
			Assert.AreEqual(0, value.Exponent);
		}

		[TestMethod]
		public void TestConstructorsWithSmallNumbers()
		{
			var fromByte = new Float_M32E32((byte)4);
			var fromSByte = new Float_M32E32((sbyte)8);
			var fromShort = new Float_M32E32((short)16);
			var fromUShort = new Float_M32E32((ushort)32);
			var fromInt = new Float_M32E32(64);

			Assert.AreEqual(4, fromByte.Mantissa);
			Assert.AreEqual(0, fromByte.Exponent);

			Assert.AreEqual(8, fromSByte.Mantissa);
			Assert.AreEqual(0, fromSByte.Exponent);

			Assert.AreEqual(16, fromShort.Mantissa);
			Assert.AreEqual(0, fromShort.Exponent);

			Assert.AreEqual(32, fromUShort.Mantissa);
			Assert.AreEqual(0, fromUShort.Exponent);

			Assert.AreEqual(64, fromInt.Mantissa);
			Assert.AreEqual(0, fromInt.Exponent);
		}

		[TestMethod]
		public void TestConstructorWithUInt()
		{
			var fromSmallUInt = new Float_M32E32(65535u);
			var fromLargeUInt = new Float_M32E32(3_000_000_000u);

			Assert.AreEqual(65535, fromSmallUInt.Mantissa);
			Assert.AreEqual(0, fromSmallUInt.Exponent);

			Assert.AreEqual(1_500_000_000, fromLargeUInt.Mantissa);
			Assert.AreEqual(1, fromLargeUInt.Exponent);
		}
	}
}
