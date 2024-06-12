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
        [Required(ErrorMessage ="Incorrect name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Incorrect description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Incorrect price")]
        public string Cost { get; set; }
        [Required(ErrorMessage = "Incorrect company name")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Incorrect image url")]
        public string UrlImage { get; set; }
        [Required(ErrorMessage = "Incorrect date")]
        public DateTime Release { get; set; }

        //[Range(typeof(DateTime), "1-Jan-1910", "1-Jan-2017")]
    }
}
