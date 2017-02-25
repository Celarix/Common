using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Validation
{
	public sealed class ValidationFailedException : Exception
	{
		private Validation validation;
		public IEnumerable<Exception> Exceptions => validation.Exceptions;

		internal ValidationFailedException(Validation validation) : base()
		{
			this.validation = validation;
		}

		internal ValidationFailedException(Validation validation, string message) : base(message)
		{
			this.validation = validation;
		}

		internal ValidationFailedException(Validation validation, Exception innerException) : base()
		{
			validation.AddException(innerException);
			this.validation = validation;
		}

		internal ValidationFailedException(Validation validation, string message, Exception innerException) : base(message)
		{
			validation.AddException(innerException);
			this.validation = validation;
		}
	}
}
