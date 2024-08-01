using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities
{
    public class UserGame
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string GameName { get; set; }
    }
}
