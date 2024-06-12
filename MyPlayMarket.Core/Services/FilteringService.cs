using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Hosting;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyPlayMarket.Core.Services
{
    public class FilteringService:IFilteringService
    {
        private readonly IGameRepository _repository;

        public FilteringService(IGameRepository repository)
        {
            _repository = repository;
        }
        public async Task<Func<IQueryable<T>, IQueryable<T>>> GetFilterExpression<T>(FilterDTO filterModel)
        {
            if (typeof(T) == typeof(Game))
            {
                var filter = BuildGameFilter(filterModel);
                return (Func<IQueryable<T>, IQueryable<T>>)await Task.FromResult(filter);
            }
            else
            {
                return await Task.FromResult<Func<IQueryable<T>, IQueryable<T>>>(null);
            }
        }
        private Func<IQueryable<Game>, IQueryable<Game>> BuildGameFilter(FilterDTO filterModel)
        {
            return query =>
            {
                if (!string.IsNullOrEmpty(filterModel.Name))
                    query = query.Where(g => g.Name.Contains(filterModel.Name));

                if (!string.IsNullOrEmpty(filterModel.Company))
                    query = query.Where(g => g.Company.Contains(filterModel.Company));

                if (filterModel.Release>0)
                    query = query.Where(g => g.Release.Year >= filterModel.Release);

                return query;
            };
        }
    }
}
