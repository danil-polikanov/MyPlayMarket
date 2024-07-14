using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;

namespace MyPlayMarket.Core.IServices
{
    public interface IUserService
    {
        string Generate(string password);
        Task Register(User user);
    }
}