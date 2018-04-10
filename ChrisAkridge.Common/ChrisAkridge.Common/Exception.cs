using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common
{
    [Serializable]
    public sealed class Exception<TExceptionArgs> : Exception, ISerializable
        where TExceptionArgs : ExceptionArgs
    {
        // Adapted from CLR via C# by Jeffrey Richter
        // Chapter 20 - Exceptions and State Management
        // "Defining Your Own Exception Class"

        private const string ArgsName = "Arguments";

        private readonly TExceptionArgs args;

        public TExceptionArgs Arguments => args;

        public override string Message
        {
            get
            {
                string baseMessage = base.Message;
                return (args == null) ? baseMessage : $"{baseMessage} ({args.Message})";
            }
        }

        public Exception(string message = null, Exception innerException = null)
            : this(null, message, innerException) { }

        public Exception(TExceptionArgs args, string message = null, 
            Exception innerException = null) : base(message, innerException)
        {
            this.args = args;
        }

        [SecurityPermission(SecurityAction.LinkDemand, 
            Flags = SecurityPermissionFlag.SerializationFormatter)]
        private Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            args = (TExceptionArgs)info.GetValue(ArgsName, typeof(TExceptionArgs));
        }

        [SecurityPermission(SecurityAction.LinkDemand,
            Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ArgsName, args);
            base.GetObjectData(info, context);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Exception<TExceptionArgs>;
            if (other == null) { return false; }
            return Equals(args, other.args) && base.Equals(obj);
        }

        public override int GetHashCode() => base.GetHashCode();
    }

    [Serializable]
    public abstract class ExceptionArgs
    {
        public virtual string Message => string.Empty;
    }
}
