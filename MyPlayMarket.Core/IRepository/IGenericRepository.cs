using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayMarket.Core.Entities;

namespace MyPlayMarket.Core.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<List<T>> GetEntitiesAsync(Func<IQueryable<T>, IQueryable<T>> expression);
        public Task<T> GetEntityAsync(Func<IQueryable<T>, IQueryable<T>> expression);
        public Task<T> GetByIdAsync(int id);
        public Task<bool> AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> AddRangeAsync(List<T> entity);
    }
}
