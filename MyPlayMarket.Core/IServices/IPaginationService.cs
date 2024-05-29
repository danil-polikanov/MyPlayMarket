using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.IServices
{
    public interface IPaginationService
    {
        public Task<IndexPaggingModel> GetGamesByPagging<T>(int currentPage, int pageSize, List<T> games, Func<IQueryable<T>, IQueryable<T>> sortExpression);
    }
}
