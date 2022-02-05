using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.Logger.Configuration
{
    public class ImSubLoggerGlobalConfiguration
    {
        public ImSubLoggerConfiguration ImSubLoggerConfiguration { get; set; }
        public MemoryQueueConfiguration MemoryQueueConfiguration { get; set; }
    }
}
