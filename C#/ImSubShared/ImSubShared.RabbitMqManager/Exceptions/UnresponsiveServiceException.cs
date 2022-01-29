using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.RabbitMqManager.Exceptions
{
    public class UnresponsiveServiceException : Exception
    {
        public UnresponsiveServiceException() : base()
        {

        }

        public UnresponsiveServiceException(string message) : base(message)
        {

        }

        public UnresponsiveServiceException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
