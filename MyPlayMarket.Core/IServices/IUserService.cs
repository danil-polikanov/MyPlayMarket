using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Entities.DTO;

namespace MyPlayMarket.Core.IServices
{
    public interface IUserService
    {
        string Generate(string password);
        Task Register(UserRegisterDTO user);
    }
}