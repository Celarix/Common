using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common
{
    /// <summary>
    /// Represents an exception that occurs when code thought to be unreachable is reached.
    /// </summary>
    [Serializable]
    public class UnreachableCodeException : Exception
    {
        public UnreachableCodeException() { }
        public UnreachableCodeException(string message) : base(message) { }
        public UnreachableCodeException(string message, Exception inner) : base(message, inner) { }
        protected UnreachableCodeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
