using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPlayMarket.Core;
using MyPlayMarket.Core.IRepository;
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
                _logger.LogError($"Couldn't retrieve entities: {ex.Message}");
                return null;
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
                _logger.LogError($"Couldn't retrieve entities: {ex.Message}");
                return null;
            }
        }
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {           
                return await _dbSet.FindAsync(id);
            }
            catch(Exception ex) {
                _logger.LogError($"Couldn't retrieve entity: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddAsync(T entity)
        {       
            try
            {
                await _dbSet.AddAsync(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't add entity: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {        
            try
            {
                _dbSet.Update(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't update entity: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {     
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning($"{nameof(entity)} with id {id} does not exist.");
                    return false;
                }
                _dbSet.Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"entity with id {id} could not be deleted: {ex.Message}");
                return false;
            }
        }
    }
}
