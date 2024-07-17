using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;

namespace MyPlayMarket.Core.IServices
{
    public interface IUserService
    {
        public Task<bool> Register(UserRegisterDTO user);
        public Task<string> Login(LoginUserDTO loginUserDTO);
    }
}