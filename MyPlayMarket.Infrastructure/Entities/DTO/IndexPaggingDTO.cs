using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities.DTO
{
    public class IndexPaggingDTO
    {
        public IEnumerable<Game> Games { get; set; }
        public PageViewDTO pageViewDTO { get; set; }
        public SortDTO sortDTO { get; set; }
        public FilterDTO filterDTO { get; set; }
        public IndexPaggingDTO()
        {
            pageViewDTO = new PageViewDTO(1, 0, 25);
            sortDTO = new SortDTO { SortBy = "SortBy" };
            filterDTO = new FilterDTO { Name = "", Company = "",Release=1961};
        }

    }
}
