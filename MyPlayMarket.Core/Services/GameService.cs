using System.Collections;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.IRepository;
using MyPlayMarket.Core.DTO;
using Microsoft.Extensions.Logging;

namespace MyPlayMarket.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable> GetGamesAsync()
        {
            return await _repository.GetAllGamesAsync();
        }
        public async Task<List<Game>> GetGamesByQueryAsync()
        {
            var games =await _repository.GetFiltredGamesAsync(q => q.Take(25));
            return games;
        }
        public async Task<Game> GetGameAsync(int id)
        {
            return await _repository.GetGameAsync(id);
        }
        public async Task<bool> CreateGameAsync(Game entity)
        {
            return await _repository.CreateGameAsync(entity);
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            return await _repository.DeleteGameAsync(id);
        }
        public async Task<bool> UpdateGameAsync(Game entity)
        {
            return await _repository.UpdateGameAsync(entity);
        }

    }
}
