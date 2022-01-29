
namespace ImSubShared.RabbitMqManager.QueueConfigurations
{
    public class RPCQueueConfiguration : BasicQueueConfiguration
    {
        public string ReceiveQueueName { get; set; }

        public int Timeout { get; set; }
    }
}
