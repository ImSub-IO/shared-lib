using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    public interface IEFPingDatabaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// This method perfrorms a ping query on the db
        /// </summary>
        /// <returns></returns>
        Task PingDatabase();
    }
}
