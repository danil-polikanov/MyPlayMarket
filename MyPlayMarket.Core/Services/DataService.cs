using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class DataService : IDataService
    {
        private readonly IGameRepository _repository;
        private readonly ISortingService _sortingService;
        private readonly IFilteringService _filteringService;
        private readonly IPaginationService _paginationService;
        public DataService(IGameRepository repository,  ISortingService sortingService, IFilteringService filteringService, IPaginationService paginationService)
        {
            _repository = repository;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _paginationService = paginationService;
        }
        public async Task<IndexPaggingModel> GetGamesAsync<T>(string property,int currentPage,int pageSize)
        {
            var sortExpression=await _sortingService.GetSortExpression<Game>(property);
            var pageGamesExpression = await _paginationService.GetGamesByPagging(sortExpression, currentPage, pageSize);
            var sortedGames = await _repository.GetFiltredGamesAsync(pageGamesExpression);
            IndexPaggingModel indexPagging = new IndexPaggingModel { Games = sortedGames.Games,pageViewModel=new PageViewModel(currentPage,sortedGames.TotalCount, pageSize)};
            return indexPagging;
        }
    }
}
