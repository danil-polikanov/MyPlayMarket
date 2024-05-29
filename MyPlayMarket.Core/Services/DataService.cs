using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class DataService:IDataService
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
        public async Task GetData<T>(string property,int currentPage,int pageSize)
        {
            var sortExpression=await _sortingService.GetSortExpression<T>(property);
            sortExpression.
            var pageGames = await _paginationService.GetGamesByPagging<T>(sortExpression, currentPage, pageSize);
            return null;
        }
    }
}
