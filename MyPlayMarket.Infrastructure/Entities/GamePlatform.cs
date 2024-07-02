using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities
{
    public class GamePlatform
    {
        
        public int GameId { get; set; }
        public Game Game { get; set; }
        
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
