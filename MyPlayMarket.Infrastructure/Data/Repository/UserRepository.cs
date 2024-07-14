using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Core.IRepository;
using MyPlayMarket.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext db, ILogger<UserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<User>> GetUsersQueryable(Func<IQueryable<User>, IQueryable<User>> expression)
        {
            try
            {
                var query = expression(_db.LocalUsers);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't retrieve users.");
                return new List<User>();
            }
        }

        public async Task<int> GetGamesCountAsync(Func<IQueryable<Game>, IQueryable<Game>> sortPageExpression)
        {
            try
            {
                return await sortPageExpression(_db.Games).CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't retrieve count of users.");
                return 0;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _db.LocalUsers.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't retrieve users.");
                return Enumerable.Empty<User>();
            }
        }

        public async Task<User> GetUserByEmailAsync(User entity)
        {
            try
            {
                return await _db.LocalUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Email == entity.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't retrieve user.");
                return null;
            }
        }

        public async Task<bool> UserAddAsync(User entity)
        {
            try
            {
                if (await _db.LocalUsers.FirstOrDefaultAsync(x => x.UserName == entity.UserName) != null)
                {
                    await _db.LocalUsers.AddAsync(entity);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("User already exists.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(entity)} could not be saved.");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User entity)
        {
            try
            {
                _db.LocalUsers.Update(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(entity)} could not be updated.");
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _db.LocalUsers.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    _logger.LogWarning($"{id} does not exist.");
                    return false;
                }
                _db.LocalUsers.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{id} could not be deleted.");
                return false;
            }
        }
    }

}

