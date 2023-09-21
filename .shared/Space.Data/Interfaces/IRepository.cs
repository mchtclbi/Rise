using System;
using System.Linq;
using Space.Data.Entities;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Space.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public Task Add(T entity);
        public Task Delete(T entity);
        public Task Update(T entity);

        public Task<IQueryable<T>> GetQueryable();
        public Task<T> Get(Expression<Func<T, bool>> filter);
        public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}