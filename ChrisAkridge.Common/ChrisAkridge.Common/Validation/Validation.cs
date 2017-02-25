using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Validation
{
	public sealed class Validation
	{
		private List<Exception> exceptions;

		public IEnumerable<Exception> Exceptions => exceptions.AsReadOnly();

		public Validation AddException(Exception ex)
		{
			lock (exceptions)
			{
				exceptions.Add(ex);
			}

			return this;
		}

		public Validation()
		{
			exceptions = new List<Exception>(1);
		}
	}
}
