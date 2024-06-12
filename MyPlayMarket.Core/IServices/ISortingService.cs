using MyPlayMarket.Core.Services;
using MyPlayMarket.Infrastructure.Entities.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.IServices
{
    public interface ISortingService
    {
        public Task<Func<IQueryable<T>, IQueryable<T>>> GetSortExpression<T>(Func<IQueryable<T>, IQueryable<T>> FiltredExpression, SortDTO sortModel);
    }
}
