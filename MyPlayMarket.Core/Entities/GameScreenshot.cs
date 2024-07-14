using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities
{
    public class GameScreenshot
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
