using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Data;
using System.Collections;
using System.Drawing.Printing;

namespace MyPlayMarket.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<IndexPaggingModel> GetFiltredGamesAsync(int currentPage,int pageSize)
        {
            var games=await _repository.GetFiltredGamesAsync();
            List<Game> result = (List<Game>)games;
            PageViewModel viewModel = new PageViewModel(currentPage, result.Count(), pageSize);
            List<Game> pageGames = result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            IndexPaggingModel indexPagging = new IndexPaggingModel { Games = pageGames, PageViewModel = viewModel };
            return indexPagging;
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
