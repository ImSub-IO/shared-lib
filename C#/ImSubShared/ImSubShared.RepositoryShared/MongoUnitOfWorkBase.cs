using ImSubShared.RepositoryShared.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    /// <summary>
    /// *** Register this class as SINGLETON ***
    /// Add as many properties as <see cref="IMongoRepositoryBase"/> you need
    /// </summary>
    public abstract class MongoUnitOfWorkBase
    {
        protected readonly IMongoClient _mongoClient;
        protected readonly IMongoDatabase _mongoDatabase;

        /// <summary>
        /// Creates a new instance of <see cref="MongoUnitOfWorkBase"/>
        /// </summary>
        /// <param name="mongoConfiguration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MongoUnitOfWorkBase(IOptions<MongoConfiguration> mongoConfiguration)
        {
            if (mongoConfiguration == null)
                throw new ArgumentNullException(nameof(mongoConfiguration));

            _mongoClient = new MongoClient(mongoConfiguration.Value.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(mongoConfiguration.Value.DatabaseName);
        }
    }
}
