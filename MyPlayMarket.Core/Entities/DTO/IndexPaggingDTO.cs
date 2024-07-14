using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities.DTO
{
    public class IndexPaggingDTO
    {
        public List<Game> Games { get; set; }
        public List<Tag> AllTags { get; set; } = new List<Tag>();
        public List<Platform> AllPlatforms { get; set; }=new List<Platform>();
        public List<Genre> AllGenres { get; set; } = new List<Genre>();
        public PageViewDTO pageViewDTO { get; set; }    
        public SortDTO sortDTO { get; set; }
        public FilterDTO filterDTO { get; set; }

        public IndexPaggingDTO()
        {
            pageViewDTO = new PageViewDTO(1, 0, 25);
            sortDTO = new SortDTO { SortBy = "SortBy" };
            filterDTO = new FilterDTO { Name = "", Company="" };
        }

    }
}
