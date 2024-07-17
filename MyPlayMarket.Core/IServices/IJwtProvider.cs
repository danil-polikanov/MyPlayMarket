using MyPlayMarket.Core.Entities;

namespace MyPlayMarket.Core.IServices
{
    public interface IJwtProvider
    {
       public string GenerateToken(User user);
    }
}