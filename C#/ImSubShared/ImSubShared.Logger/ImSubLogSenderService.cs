using ImSubShared.Logger.Configuration;
using ImSubShared.Logger.Exceptions;
using ImSubShared.RabbitMqManager.QueueConfigurations;
using ImSubShared.RabbitMqManagers.QueueManager.QueueManagers;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ImSubShared.Logger
{
    /// <summary>
    /// This service take the logs from the <see cref="MemoryLogQueue"/> and writes them into a RabbitMq
    /// </summary>
    public class ImSubLogSenderService : IImSubLogSenderService
    {
        private readonly MemoryLogQueue _memoryLogQueue;
        private readonly BasicQueueConfiguration _basicQueueConfiguration;
        private readonly IBasicSenderQueueManager<LogMessage> _basicSenderQueueManager;

        public ImSubLogSenderService(IOptions<ImSubLoggerGlobalConfiguration> globalConf)
        {
            if (globalConf == null) 
                throw new ArgumentNullException(nameof(globalConf));

            _basicQueueConfiguration = globalConf.Value.LogQueueConfiguration ?? throw new ArgumentNullException(nameof(globalConf.Value.LogQueueConfiguration));
            _basicSenderQueueManager = new BasicSenderQueueManager<LogMessage>(_basicQueueConfiguration);

            var memoryQueueConfig = globalConf.Value.MemoryQueueConfiguration ?? throw new ArgumentNullException(nameof(globalConf.Value.MemoryQueueConfiguration));
            if (!memoryQueueConfig.IsValid())
                throw new InvalidMemoryLogQueueConfigurationException();

            _memoryLogQueue = MemoryLogQueue.GetInstance(memoryQueueConfig);
        }


        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int sleepTime = 500;
                try
                {
                    LogMessage? message = _memoryLogQueue.DequeueElement();
                    if (message != null)
                        _basicSenderQueueManager.Send(message);
                    else
                        sleepTime = 3000;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.UtcNow} - ScopedImSubLogSender.DoWork - {ex}");
                    sleepTime = 3000;
                }
                finally
                {
                    try
                    {
                        await Task.Delay(sleepTime, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{DateTime.UtcNow} - ScopedImSubLogSender.DoWork - {ex}");
                    }
                }
            }
        }
    }
}
