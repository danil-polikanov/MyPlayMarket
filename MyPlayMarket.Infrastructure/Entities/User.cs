using System.ComponentModel.DataAnnotations;

namespace MyPlayMarket.Infrastructure.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public User(Guid id,string name,string surname,string userName,string passwordHash,string email,string role)
        {
            Name = name;
            Surname = surname;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
        }
    }
}
