using Space.Data.Entities;
using Space.Data.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Space.Data.Concretes
{
    public class EntityFrameworkRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : BaseEntity, new()
        where TContext : DbContext, new()
    {
        public async Task Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IQueryable<TEntity>> GetQueryable()
        {
            IQueryable<TEntity> queryable = null;
            return queryable;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FirstOrDefaultAsync(filter);
            }
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? await context.Set<TEntity>().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }
    }
}