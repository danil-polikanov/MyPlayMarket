using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class SortingService : ISortingService
    {
        private readonly IGameRepository _repository;

        public SortingService(IGameRepository repository)
        {
            _repository = repository;
        }
        public async Task<Func<IQueryable<T>, IQueryable<T>>> GetSortExpression<T>(string property)
        {
            if (typeof(T) == typeof(Game))
            {
                var sortingProperties = new Dictionary<string, Func<IQueryable<Game>, IQueryable<Game>>>
                {
                    { "Name", q => q.OrderBy(g => g.Name) },
                    { "NameDesc", q => q.OrderByDescending(g => g.Name) },
                    { "Company", q => q.OrderBy(g => g.Company) },
                    { "CompanyDesc", q => q.OrderByDescending(g => g.Company) },
                    { "Release", q => q.OrderBy(g => g.Release) },
                    { "ReleaseDesc", q => q.OrderByDescending(g => g.Release) },
                    { "Cost", q => q.OrderBy(g => g.Cost) },
                    { "CostDesc", q => q.OrderByDescending(g => g.Cost) },
                    { "Id", q => q.OrderBy(g => g.Id) },
                    { "IdDesc", q => q.OrderByDescending(g => g.Id) }
                };
                return (Func<IQueryable<T>, IQueryable<T>>)sortingProperties[property];
            }
            else return default;
        }
    }
}
