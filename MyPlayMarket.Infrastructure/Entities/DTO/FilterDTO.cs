using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities.DTO
{
    public class FilterDTO
    {
        public string Name { get; set; }
        public string Company { get; set; }
        
        public List<Tag> Tags { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<Genre> Genres { get; set; }

        
    }
}
