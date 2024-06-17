using System.ComponentModel.DataAnnotations;

namespace MyPlayMarket.Infrastructure.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
    }
}
