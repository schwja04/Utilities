using System;
using System.Runtime.Serialization;

namespace Utilities.Common.Data.Exceptions
{
    public class ColumnNotFoundException : Exception
    {
        public ColumnNotFoundException()
        {
        }

        public ColumnNotFoundException(string message) : base(message)
        {
        }

        public ColumnNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ColumnNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
