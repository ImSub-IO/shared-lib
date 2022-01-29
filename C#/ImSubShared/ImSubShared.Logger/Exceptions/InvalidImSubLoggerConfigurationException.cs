using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.Logger.Exceptions
{
    public class InvalidImSubLoggerConfigurationException : Exception
    {
        public InvalidImSubLoggerConfigurationException() : base()
        {

        }

        public InvalidImSubLoggerConfigurationException(string message) : base(message)
        {

        }

        public InvalidImSubLoggerConfigurationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
