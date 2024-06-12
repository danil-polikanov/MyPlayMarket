using Microsoft.Extensions.Hosting;
using MyPlayMarket.Infrastructure.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace MyPlayMarket.Core.IServices
{
    public interface IFilteringService
    {
        public Task<Func<IQueryable<T>, IQueryable<T>>> GetFilterExpression<T>(FilterDTO filterModel);
    }
    
}
