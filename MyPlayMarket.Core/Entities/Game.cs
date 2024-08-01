using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities
{
    public class Game
    {
        public ICollection<UserGame> UserGames { get; set; }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Incorrect name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Incorrect description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Incorrect price")]
        [Range(0,double.MaxValue)]
        public double Cost { get; set; }
        [Required(ErrorMessage = "Incorrect company name")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Incorrect image url")]
        public string UrlImage { get; set; }
        [Required(ErrorMessage = "Incorrect date")]
        public DateTime Release { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; }
        public ICollection<GameScreenshot> Screenshots { get; set; }
        public ICollection<GameTag> GameTags { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; }
        public Game()
        {
            GameGenres = new List<GameGenre>();
            Screenshots = new List<GameScreenshot>();
            GameTags = new List<GameTag>();
            GamePlatforms = new List<GamePlatform>();
        }


        //[Range(typeof(DateTime), "1-Jan-1910", "1-Jan-2017")]
    }
}
