using MyPlayMarket.Core;
using MyPlayMarket.Core.DTO;

namespace MyPlayMarket.Core.IServices
{
    public interface IUserService
    {
        string Generate(string password);
        Task Register(User user);
    }
}