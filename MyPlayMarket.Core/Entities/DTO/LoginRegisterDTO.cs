using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Entities.DTO
{
    public class LoginRegisterDTO
    {
        public UserRegisterDTO registerDTO { get; set; }
        public LoginUserDTO loginUserDTO { get; set; }
        public LoginRegisterDTO() {
        }
    }
}
