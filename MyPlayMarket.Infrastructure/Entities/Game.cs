using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }
        public string Company { get; set; }
        public string UrlImage { get; set; }
        public DateTime Release { get; set; }

        //[Range(typeof(DateTime), "1-Jan-1910", "1-Jan-2017")]
    }
}
