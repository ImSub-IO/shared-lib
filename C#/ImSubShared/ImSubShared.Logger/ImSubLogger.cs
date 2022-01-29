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
        private readonly IMemoryLogQueue _memoryLogQueue;

        /// <summary>
        /// Create a new istance of <see cref="ImSubLogger"/>
        /// </summary>
        /// <param name="conf"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidImSubLoggerConfigurationException"></exception>
        public ImSubLogger(IOptions<ImSubLoggerConfiguration> conf, IMemoryLogQueue memoryLogQueue)
        {
            if(conf == null)
                throw new ArgumentNullException(nameof(conf));
            if(memoryLogQueue == null)
                throw new ArgumentNullException(nameof(memoryLogQueue));

            ImSubLoggerConfiguration loggerConfiguration = conf.Value;
            if (!loggerConfiguration.IsValid())
                throw new InvalidImSubLoggerConfigurationException("Configuration is not valid");

            _serviceName = loggerConfiguration.ServiceName;
            _memoryLogQueue = memoryLogQueue;

        }

        public void Debug(string telegramId, string message, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, string.Empty, LogMessage.Types.Severity.Debug);
        }

        public void Debug(string telegramId, string message, string details, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, details, LogMessage.Types.Severity.Debug);
        }

        public void Error(string telegramId, string message, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, string.Empty, LogMessage.Types.Severity.Error);
        }

        public void Error(string telegramId, string message, string details, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, details, LogMessage.Types.Severity.Error);
        }

        public void Error(string telegramId, string message, Exception ex, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, ex.ToString(), LogMessage.Types.Severity.Error);
        }

        public void Fatal(string telegramId, string message, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, string.Empty, LogMessage.Types.Severity.Fatal);
        }

        public void Fatal(string telegramId, string message, string details, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, details, LogMessage.Types.Severity.Fatal);
        }

        public void Fatal(string telegramId, string message, Exception ex, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, ex.ToString(), LogMessage.Types.Severity.Fatal);
        }

        public void Info(string telegramId, string message, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, string.Empty, LogMessage.Types.Severity.Info);
        }

        public void Info(string telegramId, string message, string details, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, details, LogMessage.Types.Severity.Info);
        }

        public void Warn(string telegramId, string message, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, string.Empty, LogMessage.Types.Severity.Warning);
        }

        public void Warn(string telegramId, string message, string details, [CallerMemberName] string methodName = "")
        {
            WriteLog(telegramId, methodName, message, details, LogMessage.Types.Severity.Warning);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.UtcNow} - ImSubLogger.WriteLog - {ex}");
            }
        }
    }
}