using ImSubShared.RabbitMqManager.Exceptions;
using ImSubShared.RabbitMqManager.QueueConfigurations;
using RabbitMQ.Client;
using System;

namespace ImSubShared.RabbitMqManagers.QueueManager.QueueManagers
{
    public abstract class QueueManagerBase
    {
        protected BasicQueueConfiguration _queueConfiguration;
        protected IConnection _connection;
        protected IModel _channel;

        public QueueManagerBase(BasicQueueConfiguration queueConfiguration)
        {
            if (queueConfiguration != null)
                _queueConfiguration = queueConfiguration;
            else
                throw new InvalidConfigurationException("Configuration is null");
        }
        protected virtual void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _queueConfiguration.Hostname,
                    UserName = _queueConfiguration.Username,
                    Password = _queueConfiguration.Password,
                    RequestedHeartbeat = new TimeSpan(0, 0, 15)
                };

                factory.AutomaticRecoveryEnabled = false;
                factory.TopologyRecoveryEnabled = false;

                _connection = factory.CreateConnection();
                _connection.ConnectionShutdown += (object sender, ShutdownEventArgs e) =>
                {
                    Console.WriteLine("Connection has been closed");
                    Close(false);
                };
            }
            catch (Exception ex)
            {
                throw new UnresponsiveServiceException("Unable to connect to the queue", ex);
            }
        }

        protected virtual void CreateChannel()
        {
            if (_connection != null)
            {
                _channel = _connection.CreateModel();
                _channel.ModelShutdown += (object sender, ShutdownEventArgs e) =>
                {
                    Console.WriteLine("Channel has been closed");
                    Close(true);
                };
                _channel.ExchangeDeclare(_queueConfiguration.Exchange, "direct", durable: true);
                _channel.QueueDeclare(_queueConfiguration.QueueName, durable: true, exclusive: false, autoDelete: false);
                _channel.QueueBind(_queueConfiguration.QueueName, _queueConfiguration.Exchange, _queueConfiguration.RoutingKey, null);
            }
        }

        public virtual void Close(bool onlyChannel)
        {
            if (_channel != null)
            {
                if (_channel.IsOpen)
                {
                    try
                    {
                        _channel.Close();
                    }
                    catch (Exception ex)
                    {
                        // Just print message
                        Console.WriteLine(ex.Message);
                    }
                }
                _channel = null;
            }

            if (!onlyChannel && _connection != null)
            {
                if (_connection.IsOpen)
                {
                    try
                    {
                        _connection.Close();
                    }
                    catch (Exception ex)
                    {
                        // Just print message
                        Console.WriteLine(ex.Message);
                    }
                }
                _connection = null;
            }
        }

        protected bool ConnectionExists()
        {
            if (_connection == null)
            {
                CreateConnection();
            }
            if (_channel == null)
            {
                CreateChannel();
            }

            return _connection != null && _channel != null;
        }
    }
}
