using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Validation
{
	public static class EnumerableValidationExtensions
	{
		public static Validation HasElements<T>(this Validation validation,
				IEnumerable<T> enumerable, string paramName)
		{
			if (!enumerable.Any())
			{
				var ex = new ArgumentException($"The enumerable {paramName} has no elements.", paramName);
				return (validation ?? new Validation()).AddException(ex);
			}
			else { return validation; }
		}

		public static Validation DoesNotContainNull<T>(this Validation validation,
			IEnumerable<T> enumerable, string paramName) where T : class
		{
			int nullCount = 0;

			foreach (T item in enumerable)
			{
				if (item == null) { nullCount++; }
			}

			if (nullCount > 0)
			{
				string elementsWord = Utilities.Pluralize(nullCount, "element", "elements");
				string message = $"The enumerable {paramName} has {nullCount} {elementsWord} that are null.";
				var ex = new ArgumentException(message);
				return (validation ?? new Validation()).AddException(ex);
			}
			else { return validation; }
		}

		/// <summary>
		/// Validates that a given enumberable method is not an infinite sequence.
		/// </summary>
		/// <param name="validation">
		///		An object containing any validation exceptions that have occurred, or null if no 
		///		validation exceptions have occurred.
		///	</param>
		/// <param name="d">
		///		A delegate containing the method to check. Be sure to cast your method group to the
		///		proper Action or Func type first.
		///	</param>
		/// <param name="methodName">The name of the method being checked.</param>
		/// <returns>
		///		The validation passed in if the sequence is finite, or the validation with an
		///		<see cref="InvalidOperationException"/> added to it if the sequence is infinite.
		/// </returns>
		public static Validation IsFiniteSequence(this Validation validation, Delegate d, string methodName)
		{
			// Turns out it's not easy to get a method info on any arbitrary method
			// https://stackoverflow.com/a/9469822/2709212
			var methodInfo = d.Method;

			if (methodInfo.GetCustomAttributes(typeof(InfiniteSequenceAttribute), false).Any())
			{
				string message = $"The enumerable method {methodName} is an infinite sequence.";
				var ex = new InvalidOperationException(message);
				return (validation ?? new Validation()).AddException(ex);
			}

			return validation;
		}
	}
}
