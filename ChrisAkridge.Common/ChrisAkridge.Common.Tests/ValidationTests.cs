using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common.Validation;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class ValidationTests
	{
		[TestMethod]
		public void IsNotNull_WithNonNull()
		{
			object obj = new object();
			
			try
			{
				Validate.Begin().IsNotNull(obj, "obj").Check();
			}
			catch (ArgumentNullException)
			{
				Assert.Fail($"Invalid ArgumentNullException thrown. Object {((obj == null) ? "is" : "is not")} null.");
			}
		}

		[TestMethod]
		public void IsNotNull_WithNull()
		{
			object obj = null;

			try
			{
				Validate.Begin().IsNotNull(obj, "obj").Check();
			}
			catch (ValidationFailedException vfex)
			{
				if (!vfex.Exceptions.Any()) { Assert.Fail("No exceptions occurred."); }
				else if (vfex.Exceptions.First().GetType() != typeof(ArgumentNullException))
				{
					Assert.Fail($"Invalid exception of type {vfex.Exceptions.First().GetType().Name} thrown. Expected ArgumentNullException.");
				}
			}
			catch (Exception ex)
			{
				Assert.Fail($"Unexpected exception of type {ex.GetType().Name} thrown.");
			}
		}

		[TestMethod]
		public void AreEqual_WithEqual()
		{
			bool a = true;
			bool b = true;

			try
			{
				Validate.AreEqual(a, b, "a", "b");
			}
			catch (ArgumentException)
			{
				Assert.Fail("Invalid ArgumentException thrown.");
			}
		}

		[TestMethod]
		public void AreEqual_WithUnequal()
		{
			bool a = true;
			bool b = false;
			Exception ex = null;

			try
			{
				Validate.AreEqual(a, b, "a", "b");
			}
			catch (Exception thrownEx)
			{
				ex = thrownEx;
			}

			Assert.IsNotNull(ex);
		}
	}
}
