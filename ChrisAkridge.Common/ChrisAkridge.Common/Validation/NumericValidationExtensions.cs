using System;

namespace ChrisAkridge.Common.Validation
{
	public static class NumericValidationExtensions
	{
		public static Validation AreEqual(this Validation validation, 
			long param1, string param1Name, long param2, string param2Name)
		{
			if (param1 != param2)
			{
				var ex =  new ArgumentException($"{param1Name} ({param1}) is not equal to {param2Name} ({param2}).");
				return (validation ?? new Validation()).AddException(ex);
			}

			return validation;
		}

		public static Validation IsFinite(this Validation validation, float param, string paramName)
		{
			if (float.IsInfinity(param))
			{
				var ex = new ArgumentOutOfRangeException($"{paramName} is {param}.");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation IsFinite(this Validation validation, double param, string paramName)
		{
			if (double.IsInfinity(param))
			{
				var ex = new ArgumentOutOfRangeException($"{paramName} is {param}.");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation IsNotNaN(this Validation validation, float param, string paramName)
		{
			if (float.IsNaN(param))
			{
				var ex = new ArgumentException($"{paramName} is NaN.");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation IsNotNaN(this Validation validation, double param, string paramName)
		{
			if (double.IsNaN(param))
			{
				var ex = new ArgumentException($"{paramName} is NaN.");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation IsNotNegative(this Validation validation, long value,
			string valueName)
		{
			if (value < 0)
			{
				var ex = new ArgumentOutOfRangeException(valueName, $"{valueName} is negative (actual: {value}).");
				return (validation ?? new Validation()).AddException(ex);
			}
			else { return validation; }
		}

		public static Validation IsNotNegative(this Validation validation, double value,
			string valueName)
		{
			if (value < 0d)
			{
				var ex = new ArgumentOutOfRangeException(valueName, $"{valueName} is negative (actual: {value}).");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation IsPositive(this Validation validation, long value,
			string valueName)
		{
			if (value <= 0L)
			{
				var ex = new ArgumentOutOfRangeException(valueName, $"{valueName} is not positive (actual: {value}).");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation IsPositive(this Validation validation, double value,
			string valueName)
		{
			if (value <= 0d)
			{
				var ex = new ArgumentOutOfRangeException(valueName, $"{valueName} is not positive (actual: {value}).");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation IsNotZero(this Validation validation, long value,
			string valueName)
		{
			if (value == 0)
			{
				var ex = new ArgumentOutOfRangeException(valueName, $"{valueName} is zero.");
				return (validation ?? new Validation()).AddException(ex);
			}
			else { return validation; }
		}

		public static Validation InRangeInclusive(this Validation validation,
			long minInclusive, long maxInclusive, long param, string paramName)
		{
			if (param < minInclusive || param > maxInclusive)
			{
				var ex = new ArgumentOutOfRangeException(paramName, $"{paramName} is out of range (Value: {param}, Range: [{minInclusive}-{maxInclusive}].");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation InRangeInclusive(this Validation validation,
			double minInclusive, double maxInclusive, double param, string paramName)
		{
			if (param < minInclusive || param > maxInclusive)
			{
				var ex = new ArgumentOutOfRangeException(paramName, $"{paramName} is out of range (Value: {param}, Range: [{minInclusive}-{maxInclusive}].");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation InRangeExclusive(this Validation validation,
			long minExclusive, long maxExclusive, long param, string paramName)
		{
			if (param <= minExclusive || param >= maxExclusive)
			{
				var ex = new ArgumentOutOfRangeException(paramName, $"{paramName} is out of range (Value: {param}, Range: ({minExclusive}-{maxExclusive})");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}

		public static Validation InRangeExclusive(this Validation validation,
			double minExclusive, double maxExclusive, double param, string paramName)
		{
			if (param <= minExclusive || param >= maxExclusive)
			{
				var ex = new ArgumentOutOfRangeException(paramName, $"{paramName} is out of range (Value: {param}, Range: ({minExclusive}-{maxExclusive})");
				return (validation ?? new Validation()).AddException(ex);
			}
			return validation;
		}
	}
}
