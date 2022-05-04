using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    public interface IEFUnitOfWorkBase
    {
        Task CommitAsync();
    }
}
