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
        public async Task<Func<IQueryable<T>, IQueryable<T>>> GetGamesByPagging<T>(Func<IQueryable<T>, IQueryable<T>> sortExpression, int currentPage, int pageSize)
        {
            if (typeof(T) == typeof(Game))
            {
                Func<IQueryable<T>, IQueryable<T>> pagingExpression = q =>
                {
                    var sortedQuery = sortExpression(q);
                    return sortedQuery.Skip((currentPage - 1) * pageSize).Take(pageSize);
                };
                return pagingExpression;
            }
            return null;
        }
    }
}
