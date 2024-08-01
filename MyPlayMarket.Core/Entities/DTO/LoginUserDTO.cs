using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities.DTO
{
    public class LoginUserDTO
    {
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public LoginUserDTO() { }

        public LoginUserDTO(string password, string email)
        {
            Password = password;
            Email = email;
        }
    }
}
