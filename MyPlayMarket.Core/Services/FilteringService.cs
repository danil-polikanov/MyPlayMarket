using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class FilteringService:IFilteringService
    {
        private readonly IGameRepository _repository;

        public FilteringService(IGameRepository repository)
        {
            _repository = repository;
        }
    }
}
