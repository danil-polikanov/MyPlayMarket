using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Entities.DTO;
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
        public async Task<Func<IQueryable<T>, IQueryable<T>>> GetGamesByPagging<T>(Func<IQueryable<T>, IQueryable<T>> sortExpression,PageViewDTO pageViewModel)
        {
            if (typeof(T) == typeof(Game))
            {
                Func<IQueryable<T>, IQueryable<T>> pagingExpression = q =>
                {
                    var sortedQuery = sortExpression(q);
                    return sortedQuery.Skip((pageViewModel.CurrentPage - 1) * pageViewModel.PageItems).Take(pageViewModel.PageItems);
                };
                return pagingExpression;
            }
            return null;
        }
    }
}
