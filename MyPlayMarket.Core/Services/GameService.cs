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
        public async Task<IEnumerable> GetGamesOrderBy(string property,List<Game> games)
        {
            var sortingProperties = new Dictionary<string, Func<Game,object>>
            {
                { "Name",item => item.Name },
                { "Company",item => item.Company },
               { "Release", item => item.Release },
               { "Cost", item => item.Cost },
                {"",item=>item.Id }
            };
            return property.EndsWith("Desc") ? games.OrderByDescending(sortingProperties[property.Substring(0, property.Length - 4)]).ToList():games.OrderBy(sortingProperties[property]).ToList();
        }
        public async Task<IndexPaggingModel> GetGamesByPagging(int currentPage, int pageSize,List<Game> games)
        {
            PageViewModel viewModel = new PageViewModel(currentPage, games.Count(), pageSize);
            List<Game> pageGames = games.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
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
