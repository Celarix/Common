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
		public static void Check(this Validation validation)
		{
			if (validation == null) { return; }

			throw new ValidationFailedException(validation);
		}

		public static Validation IsNotNull<T>(this Validation validation, T obj, string paramName) where T : class
		{
			if (obj == null)
			{
				ArgumentNullException ex = new ArgumentNullException(paramName, $"The parameter {paramName} was null.");
				return (validation ?? new Validation()).AddException(ex);
			}
			else { return validation; }
		}
	}
}
