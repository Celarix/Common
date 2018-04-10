using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common.Extensions;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class FloatComparisonTests
	{
		// http://floating-point-gui.de/errors/NearlyEqualsTest.java
		private static bool NearlyEqualDefaultEpsilon(float a, float b) => a.NearlyEqual(b, 0.00001f);

		[TestMethod]
		public void BigEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(1000000f, 1000001f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(1000001f, 1000000f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(10000f, 10001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(10001f, 10000f));
		}

		[TestMethod]
		public void NegativeBigEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-1000000f, -1000001f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-1000001f, -1000000f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-10000f, -10001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-10001f, -10000f));
		}

		[TestMethod]
		public void NumbersAroundOneEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(1.0000001f, 1.0000002f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(1.0000002f, 1.0000001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(1.0002f, 1.0001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(1.0001f, 1.0002f));
		}

		[TestMethod]
		public void NumbersAroundMinusOneEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-1.0000001f, -1.0000002f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-1.0000002f, -1.0000001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-1.0002f, -1.0001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-1.0001f, -1.0002f));
		}

		[TestMethod]
		public void NumbersBetweenOneAndZeroEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0.000000001000001f, 0.000000001000002f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0.000000001000002f, 0.000000001000001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.000000000001002f, 0.000000000001001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.000000000001001f, 0.000000000001002f));
		}

		[TestMethod]
		public void NumbersBetweenMinusOneAndZeroEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-0.000000001000001f, -0.000000001000002f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-0.000000001000002f, -0.000000001000001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-0.000000000001002f, -0.000000000001001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-0.000000000001001f, -0.000000000001002f));
		}

		[TestMethod]
		public void SmallDifferencesFromZeroEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0.3f, 0.30000003f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-0.3f, -0.30000003f));
		}

		[TestMethod]
		public void ZeroEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0f, 0f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0f, -0f));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-0.0f, -0.0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.00000001f, 0.0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.0f, 0.00000001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-0.00000001f, 0.0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.0f, -0.00000001f));

			Assert.IsFalse(0f.NearlyEqual(1e-40f, 0.01f));
			Assert.IsFalse(1e-40f.NearlyEqual(0f, 0.01f));
			Assert.IsFalse(1e-40f.NearlyEqual(0f, 0.000001f));
			Assert.IsFalse(0f.NearlyEqual(1e-40f, 0.000001f));

			Assert.IsFalse(0f.NearlyEqual(-1e-40f, 0.1f));
			Assert.IsFalse((-1e-40f).NearlyEqual(0f, 0.1f));
			Assert.IsFalse((-1e-40f).NearlyEqual(0f, 0.00000001f));
			Assert.IsFalse(0f.NearlyEqual(-1e-40f, 0.00000001f));
		}

		[TestMethod]
		public void ExtremeMaximumEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(float.MaxValue, float.MaxValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.MaxValue, float.MinValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.MinValue, float.MaxValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.MaxValue, float.MaxValue / 2f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.MaxValue, float.MinValue / 2f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.MinValue, float.MaxValue / 2f));
		}

		[TestMethod]
		public void InfinitiesEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(float.PositiveInfinity, float.PositiveInfinity));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(float.NegativeInfinity, float.NegativeInfinity));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NegativeInfinity, float.PositiveInfinity));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.PositiveInfinity, float.MaxValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NegativeInfinity, float.MinValue));
		}

		[TestMethod]
		public void NaNEqualTests()
		{
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NaN, float.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NaN, 0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-0f, float.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NaN, -0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0f, float.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NaN, float.PositiveInfinity));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.PositiveInfinity, float.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NaN, float.NegativeInfinity));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NegativeInfinity, float.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NaN, float.MaxValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.MaxValue, float.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.NaN, float.MinValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.MinValue, float.NaN));
		}

		[TestMethod]
		public void OppositeEqualTests()
		{
			Assert.IsFalse(NearlyEqualDefaultEpsilon(1.000000001f, -1.0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-1.0f, 1.000000001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-1.000000001f, 1.0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(1.0f, -1.000000001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(10 * float.Epsilon, 10 * -float.Epsilon));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(10000 * float.Epsilon, 10000 * -float.Epsilon));
		}

		[TestMethod]
		public void ULPEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(float.Epsilon, float.Epsilon));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.Epsilon, -float.Epsilon));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-float.Epsilon, float.Epsilon));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.Epsilon, 0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0f, float.Epsilon));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-float.Epsilon, 0f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0f, -float.Epsilon));

			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.000000001f, -float.Epsilon));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.000000001f, float.Epsilon));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(float.Epsilon, 0.000000001f));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-float.Epsilon, 0.000000001f));
		}
	}
}
