using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _db;
        public GameRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable> GetFiltredGamesAsync()
        {
            try
            {
                return await _db.Games.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }

        }
        public async Task<IEnumerable> GetAllGamesAsync()
        {
            try
            {
                return await _db.Games.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }

        }
        public async Task<Game> GetGameAsync(int id)
        {
            try
            {
                return await _db.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }

        }
        public async Task<bool> CreateGameAsync(Game entity)
        {
            try
            {   if (await _db.Games.FirstOrDefaultAsync(x => x.Name == entity.Name) != null)
                {
                    await _db.Games.AddAsync(entity);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<bool> UpdateGameAsync(Game entity)
        {
            try
            {
                _db.Games.Update(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
        public async Task<bool> DeleteGameAsync(int id)
        {
            try
            {
                var game = await _db.Games.FirstOrDefaultAsync(x=>x.Id==id);
                if (game == null)
                {
                    throw new Exception($"{id} is not exist");
                }
                _db.Games.Remove(game);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{id} could not be saved: {ex.Message}");
            }
        }
    }
}
