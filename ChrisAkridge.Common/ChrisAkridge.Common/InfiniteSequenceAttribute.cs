using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common
{
	/// <summary>
	/// Decorates methods that yield an infinite sequence in an enumerable.
	/// </summary>
	[System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class InfiniteSequenceAttribute : Attribute
	{
		public InfiniteSequenceAttribute() { }
	}
}
