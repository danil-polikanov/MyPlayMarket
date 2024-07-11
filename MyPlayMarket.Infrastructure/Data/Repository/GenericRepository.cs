using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPlayMarket.Infrastructure.Data.IRepository;
using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<T> _logger;

        public GenericRepository(ApplicationDbContext db,ILogger<T> logger)
        {
            _db = db;
            _dbSet = _db.Set<T>();
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<List<T>> GetEntitiesAsync(Func<IQueryable<T>, IQueryable<T>> expression)
        {
            try
            {
                var query = expression(_dbSet);
                return await query.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
        public async Task<T> GetEntityAsync(Func<IQueryable<T>, IQueryable<T>> expression)
        {
            try
            {
                var query = expression(_dbSet);
                return await query.FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
