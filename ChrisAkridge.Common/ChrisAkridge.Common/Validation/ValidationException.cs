using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Validation
{
    /// <summary>
    /// Represents a general-case exception for validations.
    /// Check the content of the message to find the fault.
    /// </summary>
    public sealed class ValidationException : Exception
    {  
        private ValidationException()
        {
            // I'm forcing the programmer to call the overload with the message.
            // That way you always know what happened.
        }

        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
