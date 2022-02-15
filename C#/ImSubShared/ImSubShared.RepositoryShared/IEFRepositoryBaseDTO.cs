using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    public interface IEFRepositoryBaseDTO<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        /// <summary>
        /// Add the given element to the context
        /// </summary>
        /// <param name="elem"></param>
        /// <returns>The new added entity</returns>
        void Add(TDto elem);

        /// <summary>
        /// Delete the given element
        /// </summary>
        /// <param name="elem"></param>
        void Delete(TDto elem);

        /// <summary>
        /// Update the given element
        /// </summary>
        /// <param name="elem"></param>
        void Update(TDto elem);

        /// <summary>
        /// Get the element with the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The elem with the specified id if exists, null otherwise</returns>
        Task<TDto?> Get(long Id);
    }
}
