using System.ComponentModel.DataAnnotations;

namespace MyPlayMarket.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get;  set; }
        public string Role { get;  set; }
        public User() { }
        public User(string name,string surname,string userName,string passwordHash,string email,string role)
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
