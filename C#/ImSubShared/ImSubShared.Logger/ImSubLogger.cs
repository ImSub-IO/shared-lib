using ImSubShared.Logger.Configuration;
using ImSubShared.Logger.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Runtime.CompilerServices;

namespace ImSubShared.Logger
{
    /// <summary>
    /// This class provides a logger in order to write log messages on a RabbitMq. The messages are written on a <see cref="Queue{T}"/>
    /// and the written on a RabbitMq by <see cref="ImSubLogSender"/>
    /// </summary>
    public class ImSubLogger : IImSubLogger
    {
        private readonly string _serviceName;
        private readonly MemoryLogQueue _memoryLogQueue;
        private readonly LogQueueRabbitMqSingleton _rabbitMqLogQueue;

        /// <summary>
        /// Create a new istance of <see cref="ImSubLogger"/>
        /// </summary>
        /// <param name="conf"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidImSubLoggerConfigurationException"></exception>
        public ImSubLogger(IOptions<ImSubLoggerGlobalConfiguration> conf)
        {
            if(conf == null)
                throw new ArgumentNullException(nameof(conf));

            ImSubLoggerConfiguration loggerConfiguration = conf.Value.ImSubLoggerConfiguration ?? throw new ArgumentNullException(nameof(conf.Value.ImSubLoggerConfiguration));
            if (!loggerConfiguration.IsValid())
                throw new InvalidImSubLoggerConfigurationException();
            _serviceName = loggerConfiguration.ServiceName;

            MemoryQueueConfiguration memoryQueueConfiguration = conf.Value.MemoryQueueConfiguration ?? throw new ArgumentNullException(nameof(conf.Value.MemoryQueueConfiguration));
            if (!memoryQueueConfiguration.IsValid())
                throw new InvalidMemoryLogQueueConfigurationException();
            _memoryLogQueue = MemoryLogQueue.GetInstance(memoryQueueConfiguration);

            var basicQueueConfiguration = conf.Value.LogQueueConfiguration ?? throw new ArgumentNullException(nameof(conf.Value.LogQueueConfiguration));
            _rabbitMqLogQueue = LogQueueRabbitMqSingleton.GetInstance(basicQueueConfiguration);

        }


        public void Debug(string telegramId, string message, string details = null, [CallerMemberName] string methodName = null)
        {
            WriteLog(telegramId, methodName, message, details, LogMessage.Types.Severity.Debug);
        }

        public void Info(string telegramId, string message, string details = null, [CallerMemberName] string methodName = null)
        {
            WriteLog(telegramId, methodName, message, details, LogMessage.Types.Severity.Info);
        }
        public void Warn(string telegramId, string message, string details = null, [CallerMemberName] string methodName = null)
        {
            WriteLog(telegramId, methodName, message, details, LogMessage.Types.Severity.Warning);
        }

        public void Error(string telegramId, string message, Exception ex = null, string details = null, [CallerMemberName] string methodName = null)
        {
            WriteLog(telegramId, methodName, message, details ?? ex?.ToString(), LogMessage.Types.Severity.Error);
        }

        public void Fatal(string telegramId, string message, Exception ex = null, string details = null, [CallerMemberName] string methodName = null)
        {
            WriteLog(telegramId, methodName, message, details ?? ex?.ToString(), LogMessage.Types.Severity.Fatal);
        }

        private void WriteLog(string telegramId, string methodName,  string message, string details, LogMessage.Types.Severity severity)
        {
            try
            {
                var logMessage = new LogMessage
                {
                    TelegramId = telegramId,
                    MethodName = methodName,
                    Message = message,
                    ServiceName = _serviceName,
                    Severity = severity,
                    Details = details
                };

                _memoryLogQueue.EnqueueElement(logMessage);
            }
            catch (FullQueueException)
            {
                Console.WriteLine($"{DateTime.UtcNow} - ImSubLogger.WriteLog - Can't enqueue the message: queue is full");
                _rabbitMqLogQueue.EmergencyWriteLogInRabbitMq("Queue is full", _serviceName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.UtcNow} - ImSubLogger.WriteLog - {ex}");
                _rabbitMqLogQueue.EmergencyWriteLogInRabbitMq(ex.Message, _serviceName, ex.ToString());
            }
        }
    }
}