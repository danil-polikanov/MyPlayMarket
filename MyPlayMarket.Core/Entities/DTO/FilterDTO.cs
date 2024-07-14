using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities.DTO
{
    public class FilterDTO
    {
        public FilterDTO()
        {
            SelectedTags = new List<int>();
            SelectedPlatforms = new List<int>();
            SelectedGenres = new List<int>();
        }
        public string Name { get; set; }
        public string Company { get; set; }

        public List<int> SelectedTags { get; set; }
        public List<int> SelectedPlatforms { get; set; }
        public List<int> SelectedGenres { get; set; }


    }
}
