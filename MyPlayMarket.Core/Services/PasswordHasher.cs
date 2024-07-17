using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayMarket.Core.IServices;

namespace MyPlayMarket.Core.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
}
