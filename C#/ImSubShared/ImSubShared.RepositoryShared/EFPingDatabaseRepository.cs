using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    public class EFPingDatabaseRepository<TEntity> : IEFPingDatabaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;

        public EFPingDatabaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This method perfrorms a ping query on the db
        /// </summary>
        /// <returns></returns>
        public async Task PingDatabase()
        {
            await _dbContext.Set<TEntity>().FirstAsync();
        }
    }
}
