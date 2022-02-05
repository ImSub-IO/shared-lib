using System.Threading;
using System.Threading.Tasks;

namespace ImSubShared.Logger
{
    public interface IImSubLogSenderService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
