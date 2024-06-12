using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Data
{
    public interface IGameRepository
    {
        public Task<IEnumerable<Game>> GetAllGamesAsync();
        public Task<List<Game>> GetFiltredGamesAsync(Func<IQueryable<Game>, IQueryable<Game>> expression);
        public Task<int> GetGamesCountAsync(Func<IQueryable<Game>, IQueryable<Game>> sortPageExpression);
        public Task<Game> GetGameAsync(int id);
        public Task<bool> CreateGameAsync(Game entity);
        public Task<bool> UpdateGameAsync(Game entity);
        public Task<bool> DeleteGameAsync(int id);

    }
}
