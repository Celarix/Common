using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common.Validation;
using System.Collections.Generic;

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

            var validation = Validate.Begin().SequenceHasElements(sequence, nameof(sequence)).Check();
            if (validation != null && validation.Exceptions != null) { Assert.Fail(); }
        }

        [TestMethod]
        public void SequenceHasElements_WithoutElements()
        {
            var sequence = new List<string>();

            bool success = false;
            try
            {
                var validation = Validate.Begin().SequenceHasElements(sequence, nameof(sequence)).Check();
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
    }
}
