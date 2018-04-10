using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Extensions
{
	public static class CollectionExtensions
	{
		public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
			Func<TValue, bool> predicate)
		{
			// Credit to aku http://stackoverflow.com/a/469262/2709212
			var keys = dictionary.Keys.Where(k => predicate(dictionary[k])).ToList();
			foreach (var key in keys)
			{
				dictionary.Remove(key);
			}
		}
	}
}
