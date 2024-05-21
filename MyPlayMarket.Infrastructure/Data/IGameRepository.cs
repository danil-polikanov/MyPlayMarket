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
        public Task<IEnumerable> GetAllGamesAsync();
        public Task<IEnumerable> GetFiltredGamesAsync();
        public Task<Game> GetGameAsync(int id);
        public Task<bool> CreateGameAsync(Game entity);
        public Task<bool> UpdateGameAsync(Game entity);
        public Task<bool> DeleteGameAsync(int id);

    }
}
