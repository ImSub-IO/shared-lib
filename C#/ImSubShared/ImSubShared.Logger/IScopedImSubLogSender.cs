using System.Threading;
using System.Threading.Tasks;

namespace ImSubShared.Logger
{
    public interface IScopedImSubLogSender
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
