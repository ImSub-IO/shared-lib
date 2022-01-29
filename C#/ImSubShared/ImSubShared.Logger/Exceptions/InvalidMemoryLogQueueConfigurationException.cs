using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.Logger.Exceptions
{
    public class InvalidMemoryLogQueueConfigurationException : Exception
    {
        public InvalidMemoryLogQueueConfigurationException() : base()
        {

        }

        public InvalidMemoryLogQueueConfigurationException(string message) : base(message)
        {

        }

        public InvalidMemoryLogQueueConfigurationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
