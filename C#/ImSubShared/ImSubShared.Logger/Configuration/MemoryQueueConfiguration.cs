using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.Logger.Configuration
{
    internal class MemoryQueueConfiguration
    {
        public int QueueSizeLimit { get; set; }

        public bool IsValid()
        { 
            return QueueSizeLimit > 0;
        }
    }
}
