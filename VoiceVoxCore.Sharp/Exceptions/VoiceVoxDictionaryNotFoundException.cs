using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace VoiceVoxCore.Sharp.Exceptions
{
    public class VoiceVoxDictionaryNotFoundException : Exception
    {
        public VoiceVoxDictionaryNotFoundException()
        {
        }

        public VoiceVoxDictionaryNotFoundException(string message) : base(message)
        {
        }

        public VoiceVoxDictionaryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VoiceVoxDictionaryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

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
