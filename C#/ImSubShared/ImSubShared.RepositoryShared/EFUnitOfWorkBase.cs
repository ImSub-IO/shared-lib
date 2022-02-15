using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    /// <summary>
    /// This class is used as a base for a more specific Unit Of Work. Add one or more fields in the 
    /// concrete class in order to register one or more Repositories to the Unit Of Work
    /// </summary>
    public abstract class EFUnitOfWorkBase : IDisposable, IEFUnitOfWorkBase
    {
        protected readonly DbContext _dbContext;

        public EFUnitOfWorkBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
