using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public interface IGameService
    {
        public Task<IndexPaggingModel> GetFiltredGamesAsync(int currentPage, int pageSize);
        public Task<IEnumerable> GetGamesAsync();
        public Task<Game> GetGameAsync(int id);
        public Task<bool> CreateGameAsync(Game entity);
        public Task<bool> UpdateGameAsync(Game entity);
        public Task<bool> DeleteGameAsync(int id);
    }
}
