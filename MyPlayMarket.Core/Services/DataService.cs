using Microsoft.Extensions.Logging;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core;
using MyPlayMarket.Core.IRepository;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;
using NLog.Filters;
using NuGet.Protocol.Core.Types;
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
        //to do logger services and rep
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
        public async Task<IndexPaggingDTO> GetGamesAsync<T>(IndexPaggingDTO pageIndexPagging)
        {
            var filtredExpression = await _filteringService.GetFilterExpression<Game>(pageIndexPagging.filterDTO);
            var sortExpression=await _sortingService.GetSortExpression(filtredExpression, pageIndexPagging.sortDTO);
            var pageGamesExpression = await _paginationService.GetGamesByPagging(sortExpression, pageIndexPagging.pageViewDTO);
            var sortedGames = await _repository.GetFiltredGamesAsync(pageGamesExpression);
            IndexPaggingDTO indexPagging = new IndexPaggingDTO { Games = sortedGames, sortDTO=pageIndexPagging.sortDTO, filterDTO= pageIndexPagging.filterDTO, pageViewDTO = new PageViewDTO(pageIndexPagging.pageViewDTO.CurrentPage, await _repository.GetGamesCountAsync(sortExpression), pageIndexPagging.pageViewDTO.PageItems) };
            return indexPagging;
        }
    }
}
