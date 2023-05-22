using System;
using System.Runtime.Serialization;

namespace Utilities.Common.Data.Exceptions
{
    internal class ColumnNullException : Exception
    {
        public ColumnNullException()
        {
        }

        public ColumnNullException(string message) : base(message)
        {
        }

        public ColumnNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ColumnNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
