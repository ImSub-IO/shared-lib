using ImSubShared.RabbitMqManager.QueueConfigurations;
using ImSubShared.RabbitMqManagers.QueueManager.QueueManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.Logger
{
    /// <summary>
    /// Used for creating a singleton <see cref="BasicSenderQueueManager"/> for the logger
    /// </summary>
    internal class LogQueueRabbitMqSingleton
    {
        private static LogQueueRabbitMqSingleton _insance;

        public BasicSenderQueueManager<LogMessage> LogQueue { get; private set; }

        private LogQueueRabbitMqSingleton(BasicQueueConfiguration basicQueueConfiguration)
        { 
            LogQueue = new BasicSenderQueueManager<LogMessage>(basicQueueConfiguration);
        }

        /// <summary>
        /// Returns a singleton instance of <see cref="LogQueueRabbitMqSingleton"/>
        /// </summary>
        /// <param name="basicQueueConfiguration"></param>
        /// <returns></returns>
        public static LogQueueRabbitMqSingleton GetInstance(BasicQueueConfiguration basicQueueConfiguration)
        {
            if (_insance == null)
                _insance = new LogQueueRabbitMqSingleton(basicQueueConfiguration);

            return _insance;
        }

        public void EmergencyWriteLogInRabbitMq(string message, string serviceName, string details = "")
        {
            try
            {
                LogQueue.Send(new LogMessage
                {
                    TelegramId = "-1",
                    MethodName = nameof(EmergencyWriteLogInRabbitMq),
                    Message = message,
                    ServiceName = serviceName,
                    Severity = LogMessage.Types.Severity.Fatal,
                    Details = details
                });
            }
            catch (Exception) { } // What can I do... nothing
        }
    }
}
