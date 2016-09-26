using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ChrisAkridge.Common.Validation;

namespace ChrisAkridge.Common.Validation
{
    public static class Validate
    {
		// This class and methodology is inspired by Rick Brewster's fluent C#
		// parameter validation, which he uses extensively across his landmark
		// image editor, Paint.NET.
		//
		// The validation system provides a fluent way of validating conditions
		// on objects, parameters, and return values. Additionally, it validates
		// all the conditions provided, even if some conditions are not met,
		// thus allowing developers to narrow down exactly which conditions are
		// not met, instead of just failing as soon as a condition fails.
		//
		// For more information, check
		// https://blog.getpaint.net/2008/12/06/a-fluent-approach-to-c-parameter-validation/.


		public static Validation Begin()
		{
			return null;
		}

		public static void AreEqual(bool a, bool b, string aName, string bName)
		{
			if (a != b)
			{
				throw new ArgumentException($"{aName} must equal {bName}. {aName} is {a}, {bName} is {b}.");
			}
		}
	}
}
