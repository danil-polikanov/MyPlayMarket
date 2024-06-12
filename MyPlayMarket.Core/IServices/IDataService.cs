using MyPlayMarket.Infrastructure.Entities.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.IServices
{
    public interface IDataService
    {
        public Task<IndexPaggingDTO> GetGamesAsync<T>(IndexPaggingDTO pageIndexPagging);
    }
}
