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
        public Task<Func<IQueryable<T>, IQueryable<T>>> GetGamesByPagging<T>(Func<IQueryable<T>, IQueryable<T>> sortExpression,int currentPage, int pageSize);
    }
}
