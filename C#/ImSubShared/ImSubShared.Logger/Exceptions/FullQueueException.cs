using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.Logger.Exceptions
{
    internal class FullQueueException : Exception
    {
        public FullQueueException() : base()
        {

        }

        public FullQueueException(string message) : base(message)
        {

        }

        public FullQueueException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
