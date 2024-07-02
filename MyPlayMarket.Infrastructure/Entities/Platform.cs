using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities
{
    public class Platform
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}
