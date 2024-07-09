using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Infrastructure.Data.IRepository;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Entities.DTO;
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
        private readonly IGenericService<Tag> _tagService;
        private readonly IGenericService<Platform> _platformService;
        private readonly IGenericService<Genre> _genreService;
        private readonly IGenericService<GameScreenshot> _screenService;
        private readonly ILogger<DataService> _logger;
        public DataService(IGameRepository repository,
            ISortingService sortingService,
            IFilteringService filteringService,
            IPaginationService paginationService,
            IGenericService<Platform> platformService,
            IGenericService<Tag> tagService,
            IGenericService<Genre> genreService,
            IGenericService<GameScreenshot> screenService,
            ILogger<DataService> logger)
        {
            _repository = repository;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _paginationService = paginationService;
            _tagService = tagService;
            _platformService = platformService;
            _genreService = genreService;
            _screenService = screenService;
            _logger = logger;
        }
        public async Task<IndexPaggingDTO> GetGamesAsync<T>(IndexPaggingDTO pageIndexPagging)
        {
            var filtredExpression = await _filteringService.GetFilterExpression<Game>(pageIndexPagging.filterDTO);
            var sortExpression = await _sortingService.GetSortExpression(filtredExpression, pageIndexPagging.sortDTO);
            var pageGamesExpression = await _paginationService.GetGamesByPagging(sortExpression, pageIndexPagging.pageViewDTO);
            var sortedGames = await _repository.GetFiltredGamesAsync(pageGamesExpression);
            IndexPaggingDTO indexPagging = new IndexPaggingDTO
            {
                Games = sortedGames,
                sortDTO = pageIndexPagging.sortDTO,
                filterDTO = pageIndexPagging.filterDTO,
                pageViewDTO = new PageViewDTO(pageIndexPagging.pageViewDTO.CurrentPage, await _repository.GetGamesCountAsync(sortExpression), pageIndexPagging.pageViewDTO.PageItems)
            };

            await UpdateDtoAsync(indexPagging);

            return indexPagging;
        }
        private async Task<IndexPaggingDTO> UpdateDtoAsync(IndexPaggingDTO pageIndexPagging)
        {
            pageIndexPagging.AllGenres = (List<Genre>)await _genreService.GetAllAsync();
            pageIndexPagging.AllPlatforms = (List<Platform>)await _platformService.GetAllAsync();
            pageIndexPagging.AllTags = (List<Tag>)await _tagService.GetAllAsync();
            return pageIndexPagging;
        }
    }
}

