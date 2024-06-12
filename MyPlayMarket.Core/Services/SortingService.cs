using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Entities.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
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
        public async Task<Func<IQueryable<T>, IQueryable<T>>> GetSortExpression<T>(Func<IQueryable<T>, IQueryable<T>> filtredExpression, SortDTO sortModel)
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
                    { "SortBy", q => q.OrderBy(g => g.Id) }                   
                };
                var sortExpression = sortingProperties[sortModel.SortBy];
                Func<IQueryable<T>, IQueryable<T>> sortAndFiltredExpression = q =>
                {
                    var sortAndFiltredQuery = filtredExpression(q);
                    var sortedQuery = sortExpression((IQueryable<Game>)sortAndFiltredQuery);
                    return (IQueryable<T>)sortedQuery;
                };
                return await Task.FromResult(sortAndFiltredExpression); ;
            }
            else return default;
        }
    }
}
