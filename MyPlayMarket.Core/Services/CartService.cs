using Microsoft.AspNetCore.Http;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.IRepository;
using MyPlayMarket.Core.IServices;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IUserService _userService;
        private readonly IGenericRepository<UserGame> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IUserService userService,IGenericRepository<UserGame> repository,IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Cart> AddItemAsync(Cart cart, CartItem cartItem)
        {
            var existingItem = cart.items.FirstOrDefault(item => item.GameId == cartItem.GameId);
            if (existingItem == null)
            {
                cart.items.Add(cartItem);
                return cart;
            }
            return null;
        }
        public async Task<Cart> RemoveItemAsync(Cart cart, int gameId)
        {
            var item = cart.items.FirstOrDefault(i => i.GameId == gameId);
            if (item != null)
            {
                cart.items.Remove(item);
                return cart;
            }
            return null;
        }
        public async Task<long> GetTotalPriceAsync(Cart Cart)
        {
            return (long)Cart.items.Sum(s => s.Price);
        }
        public async Task Clear(Cart cart)
        {
            cart.items.Clear();
        }
        public async Task<Cart> GetCartAsync()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString("Cart");
            return cartJson == null ? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);

        }
        public async Task AddCartDbAsync(int userId)
        {
            var cart = await GetCartAsync();
            var user = await _userService.GetUserByIdAsync(userId);
            var userGames = cart.items.Select(item => new UserGame  
            {
                UserId = userId,
                GameId = item.GameId,
                PurchaseDate = DateTime.UtcNow,
                GameName= item.GameName,
                Price=item.Price
            }).ToList();
            await _repository.AddRangeAsync(userGames);
        }
        public async Task SaveCartAsync(Cart cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString("Cart", cartJson);
        }
    }
}
