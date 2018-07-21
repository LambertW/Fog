using System;
using System.Runtime.Serialization;

namespace Fog
{
    public class FogException : Exception
    {
        public FogException()
        {
        }

        public FogException(string message) : base(message)
        {
        }

        public FogException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
