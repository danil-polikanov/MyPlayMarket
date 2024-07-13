using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPlayMarket.Infrastructure.Data.IRepository;
using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<GameRepository> _logger;
        public GameRepository(ApplicationDbContext db, ILogger<GameRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<List<Game>> GetFiltredGamesAsync(Func<IQueryable<Game>, IQueryable<Game>> expression)
        {
            try
            {
                var query = expression(_db.Games.IncludeDependencies());
                return await query.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't retrieve entities: {ex.Message}");
                return new List<Game>();
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
                _logger.LogError($"Couldn't retrieve entities: {ex.Message}");
                return 0;
            }
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            try
            {
                return await _db.Games.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't retrieve entities: {ex.Message}");
                return Enumerable.Empty<Game>();
            }
        }

        public async Task<Game> GetGameAsync(int id)
        {
            try
            {
                return await _db.Games.IncludeDependencies().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't retrieve entity with id {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateGameAsync(Game entity)
        {
            try
            {
                var game=_db.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Name == entity.Name)??throw new Exception("Game with this name is already exist");
                await _db.Games.AddAsync(entity);
                await _db.SaveChangesAsync();
                _logger.LogWarning($"Game with name {entity.Name} created.");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(entity)} could not be saved: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateGameAsync(Game entity)
        {
            try
            {
                var game = _db.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Name == entity.Name) ?? throw new Exception("Game with this name is already exist");
                _db.Games.Update(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(entity)} could not be updated: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            try
            {
                var game = await _db.Games.FirstOrDefaultAsync(x => x.Id == id);
                if (game == null)
                {
                    _logger.LogWarning($"Game with id {id} does not exist.");
                    return false;
                }
                _db.Games.Remove(game);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Game with id {id} could not be deleted: {ex.Message}");
                return false;
            }
        }
    }

    public static class GameQueryExtensions
    {
        public static IQueryable<Game> IncludeDependencies(this IQueryable<Game> query)
        {
            return query.Include(q => q.GameGenres)
                .ThenInclude(qq => qq.Genre)
                .Include(r => r.Screenshots)
                .Include(t => t.GamePlatforms)
                .ThenInclude(tt => tt.Platform)
                .Include(f => f.GameTags)
                .ThenInclude(ff => ff.Tag);
        }
    }
}
