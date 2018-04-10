using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common.Validation;
using System.Collections.Generic;
using ChrisAkridge.Common.Extensions;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class ValidationTests
	{
        private bool ExpectingException(Type exType, Exception ex)
        {
            return ex.GetType() == exType;
        }

        [TestMethod]
        public void IsNotNull_WithNonNull()
        {
            object obj = new object();
            var validation = Validate.Begin().IsNotNull(obj, "obj").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsNotNull_WithNull()
        {
            object obj = null;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsNotNull(obj, "obj").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentNullException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
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

        [TestMethod]
        public void SequenceHasElements_WithElements()
        {
            var sequence = new int[] { 1, 2, 3 };

            var validation = Validate.Begin().HasElements(sequence, nameof(sequence)).Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void SequenceHasElements_WithoutElements()
        {
            var sequence = new List<string>();

            bool success = false;
            try
            {
                var validation = Validate.Begin().HasElements(sequence, nameof(sequence)).Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentException), vfex.Exceptions.First()));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void NumberIsNotNegative_WithPositive()
        {
            int value = 3;

            var validation = Validate.Begin().IsNotNegative(value, nameof(value)).Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void NumberIsNotNegative_WithNegative()
        {
            int value = -3;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsNotNegative(value, nameof(value)).Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void NumberInRange_InRange()
        {
            int min = 0;
            int max = 100;
            int value = 50;

            var validation = Validate.Begin().InRangeInclusive(min, max, value, "value").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void NumberInRangeInclusive_OutOfRange()
        {
            int min = 0;
            int max = 100;
            int value = 101;

            bool success = false;
            try
            {
                var validation = Validate.Begin().InRangeInclusive(min, max, value, "value").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsTrue_WithTrue()
        {
            bool value = true;

            var validation = Validate.Begin().IsTrue(value, "value isn't true").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsTrue_WithFalse()
        {
            bool value = false;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsTrue(value, "value is true").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ValidationException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsFalse_WithFalse()
        {
            bool value = false;

            var validation = Validate.Begin().IsFalse(value, "value isn't false").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsFalse_WithTrue()
        {
            bool value = true;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsFalse(value, "value is false").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ValidationException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsZero_WithNonZero()
        {
            int value = 1;

            var validation = Validate.Begin().IsNotZero(value, "value").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsZero_WithZero()
        {
            int value = 0;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsNotZero(value, "value").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        public enum TestEnum
        {
            Default = 0,
            CherrySoda,
            DietSprite,
            RootBeer
        }

        [Flags]
        public enum TestFlagsEnum
        {
            NoToppings = 0,
            TomatoSauce = 1,
            Hamburger = 2,
            Mozzarella = 4,
            Pepperoni = 8,
            Pineapple = 16
        }

        [TestMethod]
        public void IsValidEnumValue_GoodValue()
        {
            var value = TestEnum.CherrySoda;

            var validation = Validate.Begin().IsValidEnumValue(value, "value").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsValidEnumValue_GoodFlags()
        {
            var value = TestFlagsEnum.TomatoSauce | TestFlagsEnum.Mozzarella | TestFlagsEnum.Hamburger;

            var validation = Validate.Begin().IsValidEnumValue(value, "value").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsValidEnumValue_BadValue()
        {
            var value = (TestEnum)246;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsValidEnumValue(value, "value").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsValidEnumValue_BadFlags()
        {
            var value = (TestFlagsEnum)0x7FFFFFFF;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsValidEnumValue(value, "value").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsNotNaN_WithNonNaNSingle()
        {
            float f = 1.0f;
            var validation = Validate.Begin().IsNotNaN(f, "f").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsNotNaN_WithNaNSingle()
        {
            float f = float.NaN;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsNotNaN(f, "f").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsFinite_WithFiniteSingle()
        {
            float f = 1f;
            var validation = Validate.Begin().IsFinite(f, "f").Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsFinite_WithPositiveInfinitySingle()
        {
            float f = float.PositiveInfinity;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsFinite(f, "f").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

        [TestMethod]
        public void IsFinite_WithNegativeInfinitySingle()
        {
            float f = float.NegativeInfinity;

            bool success = false;
            try
            {
                var validation = Validate.Begin().IsFinite(f, "f").Check();
            }
            catch (ValidationFailedException vfex)
            {
                Assert.AreEqual(vfex.Exceptions.Count(), 1);
                List<Exception> exceptions = vfex.Exceptions.ToList();
                success = true;
                Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
            }

            if (!success) { Assert.Fail(); }
        }

		[TestMethod]
		public void IsNotNaN_WithNonNaNDouble()
		{
			double d = 1d;

			var validation = Validate.Begin().IsNotNaN(d, "d").Check();
			if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsNotNaN_WithNaNDouble()
		{
			double d = double.NaN;

			bool success = false;
			try
			{
				var validation = Validate.Begin().IsNotNaN(d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsFinite_WithFiniteDouble()
		{
			double d = 1d;

			var validation = Validate.Begin().IsFinite(d, "d").Check();
			if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsFinite_WithPositiveInfinityDouble()
		{
			double d = double.PositiveInfinity;

			bool success = false;
			try
			{
				var validation = Validate.Begin().IsFinite(d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsFinite_WithNegativeInfinityDouble()
		{
			double d = double.NegativeInfinity;

			bool success = false;
			try
			{
				var validation = Validate.Begin().IsFinite(d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsNotNegative_WithPositiveDouble()
		{
			double d = 1d;
			var validation = Validate.Begin().IsNotNegative(d, "d").Check();
			if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsNotNegative_WithZeroDouble()
		{
			double d = 0d;
			var validation = Validate.Begin().IsNotNegative(d, "d").Check();
			if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsNotNegative_WithNegativeDouble()
		{
			double d = -1d;

			bool success = false;
			try
			{
				var validation = Validate.Begin().IsNotNegative(d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsPositive_WithPositiveLong()
		{
			long l = 1L;
			var validation = Validate.Begin().IsNotNegative(l, "l").Check();
			if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsPositive_WithNegativeLong()
		{
			long l = -1L;

			bool success = false;
			try
			{
				var validation = Validate.Begin().IsNotNegative(l, "l").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsPositive_WithPositiveDouble()
		{
			double d = 1d;
			var validation = Validate.Begin().IsNotNegative(d, "d").Check();
			if (validation != null && validation.Exceptions != null)
			{ Assert.Fail(); }
		}

		[TestMethod]
		public void IsPositive_WithNegativeDouble()
		{
			double d = -1L;

			bool success = false;
			try
			{
				var validation = Validate.Begin().IsNotNegative(d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success)
			{ Assert.Fail(); }
		}

		[TestMethod]
		public void InRangeInclusive_InRangeDouble()
		{
			double d = 5d;	

			var validation = Validate.Begin().InRangeInclusive(0d, 10d, d, "d").Check();
			if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
		}

		[TestMethod]
		public void InRangeInclusive_AboveRangeDouble()
		{
			double d = 15d;

			bool success = false;
			try
			{
				var validation = Validate.Begin().InRangeInclusive(0d, 10d, d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		[TestMethod]
		public void InRangeInclusive_BelowRangeDouble()
		{
			double d = -5d;

			bool success = false;
			try
			{
				var validation = Validate.Begin().InRangeInclusive(0d, 10d, d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		[TestMethod]
		public void InRangeExclusive_InRangeDouble()
		{
			double d = 5d;

			var validation = Validate.Begin().InRangeExclusive(0d, 10d, d, "d").Check();
			if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
		}

		[TestMethod]
		public void InRangeExclusive_AboveRangeDouble()
		{
			double d = 15d;

			bool success = false;
			try
			{
				var validation = Validate.Begin().InRangeExclusive(0d, 10d, d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		[TestMethod]
		public void InRangeExclusive_BelowRangeDouble()
		{
			double d = -5d;

			bool success = false;
			try
			{
				var validation = Validate.Begin().InRangeExclusive(0d, 10d, d, "d").Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(ArgumentOutOfRangeException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		private IEnumerable<int> FiniteSequence()
		{
			for (int i = 0; i < 5; i++)
			{
				yield return i;
			}
		}

		[InfiniteSequence]
		private IEnumerable<int> InfiniteSequence()
		{
			while (true)
			{
				yield return 0;
			}
		}

		[TestMethod]
		public void IsFiniteSequence_WithFiniteSequence()
		{
			var validation = Validate.Begin().IsFiniteSequence((Func<IEnumerable<int>>)FiniteSequence,
				nameof(FiniteSequence)).Check();
			if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
		}

		[TestMethod]
		public void IsFiniteSequence_WithInfiniteSequence()
		{
			bool success = false;

			try
			{
				var validation = Validate.Begin().IsFiniteSequence(
					(Func<IEnumerable<int>>)InfiniteSequence, nameof(InfiniteSequence)).Check();
			}
			catch (ValidationFailedException vfex)
			{
				Assert.AreEqual(vfex.Exceptions.Count(), 1);
				List<Exception> exceptions = vfex.Exceptions.ToList();
				success = true;
				Assert.IsTrue(ExpectingException(typeof(InvalidOperationException), exceptions[0]));
			}

			if (!success) { Assert.Fail(); }
		}

		// AreEqual<T>
		// AreEqual with longs
	}
}
