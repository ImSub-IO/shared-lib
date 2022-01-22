using QueueManager.QueueConfigurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace QueueManager.QueueManagers
{
    public class BasicReceiverQueueManager<T> : QueueManagerBase, IBasicReceiverQueueManager<T> where T : class
    {
        private EventingBasicConsumer _consumer;

        /// <summary>
        /// Creates a new istance of BasicReceiverQueueManager
        /// </summary>
        /// <param name="queueConfiguration">The configuration for the queue</param>
        /// <exception cref="InvalidConfigurationException"></exception>
        public BasicReceiverQueueManager(BasicQueueConfiguration queueConfiguration) : base(queueConfiguration)
        {
        }

        protected override void CreateChannel()
        {
            base.CreateChannel();
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += OnReceive;
        }

        /// <summary>
        /// Start reading messages from the queue
        /// </summary>
        public virtual void Consume()
        {
            if (ConnectionExists())
            {
                _channel.BasicConsume(
                    queue: _queueConfiguration.QueueName,
                    autoAck: true,
                    consumer: _consumer
                );
            }
        }

        protected virtual void OnReceive(object obj, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        }

        /// <summary>
        /// Check the queue state
        /// </summary>
        public void CheckState()
        {
            if (ConnectionExists())
            {
                _channel.ExchangeDeclarePassive(_queueConfiguration.Exchange);
                _channel.QueueDeclarePassive(_queueConfiguration.QueueName);
            }
        }
    }
}
