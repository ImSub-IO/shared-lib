using AutoMapper;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    public class MongoRepositoryBaseDTO<TEntity, TDto> : IMongoRepositoryBaseDTO<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        protected readonly IMapper _mapper;
        protected readonly IMongoCollection<TEntity> _mongoCollection;

        /// <summary>
        /// Creates a new instance of <see cref="MongoRepositoryBaseDTO{TEntity, TDto}"/>
        /// </summary>
        /// <param name="mongoCollection"></param>
        /// <param name="mapper"></param>
        public MongoRepositoryBaseDTO(IMongoCollection<TEntity> mongoCollection, IMapper mapper)
        {
            _mongoCollection = mongoCollection;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new element
        /// </summary>
        /// <param name="newElement"></param>
        /// <returns></returns>
        public virtual async Task CreateAsync(TDto newElement)
        {
            await _mongoCollection.InsertOneAsync(_mapper.Map<TEntity>(newElement));
        }

        /// <summary>
        /// Get the element with the specified id if exists. If the element doesn't exists, it returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TDto?> GetAsync(string id)
        {
            return _mapper.Map<TDto>(await _mongoCollection.Find(Builders<TEntity>.Filter.Eq("_id", id)).FirstOrDefaultAsync());
        }

        /// <summary>
        /// Delets the element with the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(string id)
        {
            await _mongoCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }

        /// <summary>
        /// Update the element with specified id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedElement"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(string id, TDto updatedElement)
        {
            await _mongoCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), _mapper.Map<TEntity>(updatedElement));
        }
    }
}
