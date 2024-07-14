using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<List<T>> GetEntitiesAsync(Func<IQueryable<T>, IQueryable<T>> expression)
        {
            return await _repository.GetEntitiesAsync(expression);
        }
        public async Task<T> GetEntityAsync(Func<IQueryable<T>, IQueryable<T>> expression)
        {
            return await _repository.GetEntityAsync(expression);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            return true;
           
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
