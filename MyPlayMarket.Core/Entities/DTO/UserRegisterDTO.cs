using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public UserRegisterDTO() { }

        public UserRegisterDTO(string name, string surname, string userName, string password, string confirmPassword, string email)
        {
            Name = name;
            Surname = surname;
            UserName = userName;
            Password = password;
            ConfirmPassword = confirmPassword;
            Email = email;
        }
    }
}
