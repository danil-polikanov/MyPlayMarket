using Microsoft.AspNetCore.Identity;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Data;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Entities.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class UserService : IUserService
    {
        public UserService() {
        }

        public async Task Register(UserRegisterDTO userDTO)
        {
            var hashedPassord = Generate(user.Password);
            var user = new User(Guid.NewGuid(), userDTO.Name, userDTO.Surname, userDTO.UserName, hashedPassord, userDTO.Email, "User");
            await _userRepository.Add(user);
        }
        public bool Verify(string password, string hashedPassword)=>BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
}
