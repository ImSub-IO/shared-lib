using ImSubShared.RabbitMqManager.QueueConfigurations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ImSubShared.RabbitMqManagers.QueueManager.QueueManagers
{
    public class BasicSenderQueueManager<T> : QueueManagerBase, IBasicSenderQueueManager<T> where T : class
    {

        /// <summary>
        /// Creates a new istance of BasicSenderQueueManager
        /// </summary>
        /// <param name="queueConfiguration">The configuration for the queue</param>
        /// <exception cref="InvalidConfigurationException"></exception>
        public BasicSenderQueueManager(BasicQueueConfiguration queueConfiguration): base(queueConfiguration)
        {

        }

        /// <summary>
        /// Send a message to the queue
        /// </summary>
        /// <param name="elem">Element to send</param>
        public void Send(T elem)
        {
            if (ConnectionExists())
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                string elemJsonString = JsonConvert.SerializeObject(elem, serializerSettings);

                _channel.ExchangeDeclarePassive(_queueConfiguration.Exchange);
                _channel.QueueDeclarePassive(_queueConfiguration.QueueName);

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: _queueConfiguration.QueueName,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes(elemJsonString)
                );
            }
            else
            {
                Console.WriteLine("Unable to create connection or channel");
            }
        }
    }
}

