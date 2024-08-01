using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;

namespace MyPlayMarket.Core.IServices
{
    public interface IUserService
    {
        public Task<string> Register(UserRegisterDTO user);
        public Task<string> Login(LoginUserDTO loginUserDTO);
        public Task<User> GetUserByIdAsync(int id);
    }
}