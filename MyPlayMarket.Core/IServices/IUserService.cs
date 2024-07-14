using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;

namespace MyPlayMarket.Core.IServices
{
    public interface IUserService
    {
        public string Generate(string password);
        public Task Register(User user);
    }
}