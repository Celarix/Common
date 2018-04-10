using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common.Extensions;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class DoubleComparisonTests
	{
		// http://doubleing-point-gui.de/errors/NearlyEqualsTest.java
		private static bool NearlyEqualDefaultEpsilon(double a, double b) => a.NearlyEqual(b, 0.00001d);

		[TestMethod]
		public void BigEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(1000000d, 1000001d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(1000001d, 1000000d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(10000d, 10001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(10001d, 10000d));
		}

		[TestMethod]
		public void NegativeBigEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-1000000d, -1000001d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-1000001d, -1000000d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-10000d, -10001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-10001d, -10000d));
		}

		[TestMethod]
		public void NumbersAroundOneEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(1.0000001d, 1.0000002d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(1.0000002d, 1.0000001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(1.0002d, 1.0001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(1.0001d, 1.0002d));
		}

		[TestMethod]
		public void NumbersAroundMinusOneEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-1.0000001d, -1.0000002d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-1.0000002d, -1.0000001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-1.0002d, -1.0001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-1.0001d, -1.0002d));
		}

		[TestMethod]
		public void NumbersBetweenOneAndZeroEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0.000000001000001d, 0.000000001000002d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0.000000001000002d, 0.000000001000001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.000000000001002d, 0.000000000001001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.000000000001001d, 0.000000000001002d));
		}

		[TestMethod]
		public void NumbersBetweenMinusOneAndZeroEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-0.000000001000001d, -0.000000001000002d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-0.000000001000002d, -0.000000001000001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-0.000000000001002d, -0.000000000001001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-0.000000000001001d, -0.000000000001002d));
		}

		[TestMethod]
		public void SmallDifferencesFromZeroEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0.3d, 0.30000003d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-0.3d, -0.30000003d));
		}

		[TestMethod]
		public void ZeroEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0d, 0d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(0d, -0d));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(-0.0d, -0.0d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.00000001d, 0.0d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.0d, 0.00000001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-0.00000001d, 0.0d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0.0d, -0.00000001d));

			Assert.IsFalse(0d.NearlyEqual(1e-40d, 0.01d));
			Assert.IsFalse(1e-40d.NearlyEqual(0d, 0.01d));
			Assert.IsFalse(1e-40d.NearlyEqual(0d, 0.000001d));
			Assert.IsFalse(0d.NearlyEqual(1e-40d, 0.000001d));

			Assert.IsFalse(0d.NearlyEqual(-1e-40d, 0.1d));
			Assert.IsFalse((-1e-40d).NearlyEqual(0d, 0.1d));
			Assert.IsFalse((-1e-40d).NearlyEqual(0d, 0.00000001d));
			Assert.IsFalse(0d.NearlyEqual(-1e-40d, 0.00000001d));
		}

		[TestMethod]
		public void ExtremeMaximumEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(double.MaxValue, double.MaxValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.MaxValue, double.MinValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.MinValue, double.MaxValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.MaxValue, double.MaxValue / 2d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.MaxValue, double.MinValue / 2d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.MinValue, double.MaxValue / 2d));
		}

		[TestMethod]
		public void InfinitiesEqualTests()
		{
			Assert.IsTrue(NearlyEqualDefaultEpsilon(double.PositiveInfinity, double.PositiveInfinity));
			Assert.IsTrue(NearlyEqualDefaultEpsilon(double.NegativeInfinity, double.NegativeInfinity));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NegativeInfinity, double.PositiveInfinity));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.PositiveInfinity, double.MaxValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NegativeInfinity, double.MinValue));
		}

		[TestMethod]
		public void NaNEqualTests()
		{
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NaN, double.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NaN, 0d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-0d, double.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NaN, -0d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(0d, double.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NaN, double.PositiveInfinity));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.PositiveInfinity, double.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NaN, double.NegativeInfinity));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NegativeInfinity, double.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NaN, double.MaxValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.MaxValue, double.NaN));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.NaN, double.MinValue));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(double.MinValue, double.NaN));
		}

		[TestMethod]
		public void OppositeEqualTests()
		{
			Assert.IsFalse(NearlyEqualDefaultEpsilon(1.000000001d, -1.0d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-1.0d, 1.000000001d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(-1.000000001d, 1.0d));
			Assert.IsFalse(NearlyEqualDefaultEpsilon(1.0d, -1.000000001d));
		}

		//[TestMethod]
		//public void ULPEqualTests()
		//{
		//	Assert.IsTrue(NearlyEqualDefaultEpsilon(double.Epsilon, double.Epsilon));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(double.Epsilon, -double.Epsilon));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(-double.Epsilon, double.Epsilon));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(double.Epsilon, 0d));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(0d, double.Epsilon));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(-double.Epsilon, 0d));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(0d, -double.Epsilon));

		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(0.000000001d, -double.Epsilon));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(0.000000001d, double.Epsilon));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(double.Epsilon, 0.000000001d));
		//	Assert.IsFalse(NearlyEqualDefaultEpsilon(-double.Epsilon, 0.000000001d));
		//}
	}
}
