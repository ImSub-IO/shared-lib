using ImSubShared.RabbitMqManager.QueueConfigurations;

namespace ImSubShared.Logger.Configuration
{
    public class ImSubLoggerGlobalConfiguration
    {
        public ImSubLoggerConfiguration ImSubLoggerConfiguration { get; set; }
        public MemoryQueueConfiguration MemoryQueueConfiguration { get; set; }
        public BasicQueueConfiguration LogQueueConfiguration { get; set; }
    }
}
