using MyPlayMarket.Core.Entities;

namespace MyPlayMarket.Core.IServices
{
    public interface ICartService
    {
        public Task<Cart> AddItemAsync(Cart cart, CartItem cartItem);
        public Task Clear(Cart cart);
        public Task<Cart> GetCartAsync();
        public Task<long> GetTotalPriceAsync(Cart Cart);
        public Task<Cart> RemoveItemAsync(Cart cart, int id);
        public Task AddCartDbAsync(int id);    
        public Task SaveCartAsync(Cart cart);
    }
}