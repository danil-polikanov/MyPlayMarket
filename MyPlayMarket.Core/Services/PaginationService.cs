using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Infrastructure.Entities;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class PaginationService: IPaginationService
    {
        private readonly IGameRepository _repository;

        public PaginationService(IGameRepository repository)
        {
            _repository = repository;
        }
        public async Task<IndexPaggingModel> GetGamesByPagging<T>(int currentPage, int pageSize, List<T> games, Func<IQueryable<T>, IQueryable<T>> sortExpression)
        {
            if (typeof(T) == typeof(Game))
            {
                sortExpression = sortExpression.BeginInvoke().Skip((currentPage - 1) * pageSize).Take(pageSize);
                IndexPaggingModel indexPagging = new IndexPaggingModel { Games = pageGames, PageViewModel = viewModel };
                return indexPagging;
            }
            return default;
        }
    }
}
