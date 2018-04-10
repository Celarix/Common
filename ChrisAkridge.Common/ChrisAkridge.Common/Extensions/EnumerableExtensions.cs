using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Extensions
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			// https://stackoverflow.com/a/101313/2709212
			// https://stackoverflow.com/a/101278/2709212
			foreach (T element in enumerable)
			{
				action(element);
			}
		}
	}
}
