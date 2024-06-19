using MyPlayMarket.Infrastructure.Entities;

namespace MyPlayMarket.Core.IServices
{
    public interface IUserService
    {
        string Generate(string password);
        Task Register(User user);
    }
}