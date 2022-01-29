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
    public class ScopedImSubLogSender : IScopedImSubLogSender
    {
        private readonly IMemoryLogQueue _memoryLogQueue;
        private readonly BasicQueueConfiguration _basicQueueConfiguration;
        private readonly IBasicSenderQueueManager<LogMessage> _basicSenderQueueManager;

        public ScopedImSubLogSender(IOptions<BasicQueueConfiguration> basicQueueConfiguration, IMemoryLogQueue memoryLogQueue)
        {

            _basicQueueConfiguration = basicQueueConfiguration == null ? throw new ArgumentNullException(nameof(basicQueueConfiguration)) : basicQueueConfiguration.Value;
            _basicSenderQueueManager = new BasicSenderQueueManager<LogMessage>(_basicQueueConfiguration);
            _memoryLogQueue = memoryLogQueue ?? throw new ArgumentNullException(nameof(memoryLogQueue));
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
                    await Task.Delay(sleepTime, stoppingToken);
                }
            }
        }
    }
}
