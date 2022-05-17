using System;
using System.Runtime.Serialization;

namespace VoiceVoxCore.Sharp.Exceptions
{
    public class VoiceVoxVersionNotFoundException : Exception
    {
        public VoiceVoxVersionNotFoundException()
        {
        }

        public VoiceVoxVersionNotFoundException(string message) : base(message)
        {
        }

        public VoiceVoxVersionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VoiceVoxVersionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
