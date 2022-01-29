using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ImSubShared.Logger
{
    /// <summary>
    /// This class takes logs added to the queue and writes the in a RabbitMq. Register an istance of this class as
    /// <see cref="BackgroundService"/> and an instance of <see cref="ScopedImSubLogSender"/> with services.AddScoped(...)
    /// </summary>
    public class ImSubLogSender : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ImSubLogSender(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedImSubLogSender>();

                await scopedProcessingService.DoWork(stoppingToken);
            }
        }
    }
}
