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

		public static Validation AreEqual<T>(this Validation validation,
			T param1, string param1Name, T param2, string param2Name) 
		{
			if (!param1.Equals(param2))
			{
				var ex = new ArgumentException($"{param1Name} ({param1}) is not equal to {param2Name} ({param2}).");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
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

        public static Validation IsValidEnumValue<TEnum>(this Validation validation,
            TEnum param, string paramName) where TEnum : struct
        {
            if (!Validators.IsValid(param))
            {
                string enumTypeName = typeof(TEnum).Name;
                var ex = new ArgumentException($"{paramName} does not have a valid value in the {enumTypeName} enum.");
                return (validation ?? new Validation()).AddException(ex);
            }
            return validation;
        }
	}
}
