﻿
namespace ImSubShared.RabbitMqManager.QueueConfigurations
{
    public  class BasicQueueConfiguration
    {
        public string Hostname { get; set; }

        public string Password { get; set; }
        public string QueueName { get; set; }
        public string Username { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
    }
}
