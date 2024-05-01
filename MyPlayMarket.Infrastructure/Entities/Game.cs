using System.ComponentModel.DataAnnotations;

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
        public string urlImage { get; set; }   
    }
}
