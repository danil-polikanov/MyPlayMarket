using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Data;
using System.Collections;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPlayMarket.Core.IServices;

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
