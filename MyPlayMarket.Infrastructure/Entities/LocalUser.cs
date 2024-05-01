using System.ComponentModel.DataAnnotations;

namespace MyPlayMarket.Infrastructure.Entities
{
    public class LocalUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
