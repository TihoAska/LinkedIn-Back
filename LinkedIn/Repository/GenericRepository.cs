using LinkedIn.Data;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkedIn.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IDataContext _context;
        private DbSet<TEntity> _entities;
        protected IQueryable<TEntity> _query;

        public GenericRepository(IDataContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
            _query = _entities;
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? await _query.Where(predicate).ToListAsync() : await _query.ToListAsync(cancellationToken);
        }

        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
            return entity;
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }

        public void Attach(TEntity entity)
        {
            _entities.Attach(entity).State = EntityState.Unchanged;
        }
    }
}
