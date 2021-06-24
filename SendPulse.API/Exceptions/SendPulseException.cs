using System;

namespace SendPulse.API.Exceptions
{
    public class SendPulseException : Exception
    {
        public SendPulseException()
        {

        }

        public SendPulseException(string message) : base(message)
        {

        }

        public SendPulseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
