using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public UserRegisterDTO(string name, string surname, string userName, string password, string email)
        {
            Name = name;
            Surname = surname;
            UserName = userName;
            Password = password;
            Email = email;
        }
    }
}
