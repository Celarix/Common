using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChrisAkridge.Common.Validation;

namespace ChrisAkridge.Common.Validation
{
	public static class ValidationExtensions
	{
		public static Validation Check(this Validation validation)
		{
			if (validation == null || validation.Exceptions == null)
            {
                return validation;
            }

			throw new ValidationFailedException(validation);
		}

        public static Validation IsTrue(this Validation validation, bool value,
            string failureMessage)
        {
            if (!value)
            {
                var ex = new ValidationException(failureMessage);
                return (validation ?? new Validation()).AddException(ex);
            }

            return validation;
        }

        public static Validation IsFalse(this Validation validation, bool value,
            string failureMessage)
        {
            if (value)
            {
                var ex = new ValidationException(failureMessage);
                return (validation ?? new Validation()).AddException(ex);
            }

            return validation;
        }

		public static Validation IsNotNull<T>(this Validation validation, T obj, 
            string paramName) where T : class
		{
			if (obj == null)
			{
				ArgumentNullException ex = new ArgumentNullException(paramName, $"The parameter {paramName} was null.");
				return (validation ?? new Validation()).AddException(ex);
			}
			else { return validation; }
		}

        public static Validation SequenceHasElements<T>(this Validation validation, 
            IEnumerable<T> sequence, string paramName )
        {
            if (!sequence.Any())
            {
                var ex = new ArgumentException($"The sequence {paramName} has no elements.", paramName);
                return (validation ?? new Validation()).AddException(ex);
            }
            else { return validation; }
        }

        public static Validation NumberIsNotNegative(this Validation validation, int value, 
            string valueName)
        {
            if (value < 0)
            {
                var ex = new ArgumentOutOfRangeException(valueName, $"{valueName} not positive (actual: {value})");
                return (validation ?? new Validation()).AddException(ex);
            }
            else { return validation; }
        }

        public static Validation NumberWithinRangeInclusive(this Validation validation,
            int minInclusive, int maxInclusive, int param, string paramName)
        {
            if (param < minInclusive || param > maxInclusive)
            {
                var ex = new ArgumentOutOfRangeException($"{paramName} is out of range (Value: {param}, Range: [{minInclusive}-{maxInclusive}]");
                return (validation ?? new Validation()).AddException(ex);
            }
            return validation;
        }
	}
}
