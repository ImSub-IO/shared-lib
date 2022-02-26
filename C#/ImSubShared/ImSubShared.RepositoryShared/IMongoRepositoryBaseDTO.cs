using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    public interface IMongoRepositoryBaseDTO<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        /// <summary>
        /// Get the element with the specified id if exists. If the element doesn't exists, it returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TDto?> GetAsync(string id);

        /// <summary>
        /// Creates a new element
        /// </summary>
        /// <param name="newElement"></param>
        /// <returns></returns>
        Task CreateAsync(TDto newElement);

        /// <summary>
        /// Update the element with specified id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedElement"></param>
        /// <returns></returns>
        Task UpdateAsync(string id, TDto updatedElement);

        /// <summary>
        /// Delets the element with the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveAsync(string id);
    }
}
