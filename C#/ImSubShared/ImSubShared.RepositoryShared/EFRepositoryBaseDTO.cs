using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ImSubShared.RepositoryShared
{
    public class EFRepositoryBaseDTO<TEntity, TDto> : IEFRepositoryBaseDTO<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        protected readonly IMapper _mapper;
        protected readonly DbContext _dbContext;

        /// <summary>
        /// Creates a new instance of <see cref="EFRepositoryBaseDTO{TEntity, TDto}"/>
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public EFRepositoryBaseDTO(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Add the given element to the context
        /// </summary>
        /// <param name="elem"></param>
        /// <returns>The new added entity</returns>
        public virtual void Add(TDto elem)
        {
            _dbContext.Set<TEntity>().Add(_mapper.Map<TEntity>(elem));
        }

        /// <summary>
        /// Delete the given element
        /// </summary>
        /// <param name="elem"></param>
        public virtual void Delete(TDto elem)
        {
            _dbContext.Set<TEntity>().Remove(_mapper.Map<TEntity>(elem));
        }

        /// <summary>
        /// Update the given element
        /// </summary>
        /// <param name="elem"></param>
        public virtual void Update(TDto elem)
        {
            _dbContext.Set<TEntity>().Update(_mapper.Map<TEntity>(elem));
        }

        /// <summary>
        /// Get the element with the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The elem with the specified id if exists, null otherwise</returns>
        public virtual async Task<TDto?> Get(long id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
                return _mapper.Map<TDto>(entity);
            }
            else
                return null;
        }
    }
}
