using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Services;
using Stripe.Checkout;

namespace MyPlayMarket.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly StripeSettings _stripeSettings;

        public CartController(ICartService cartService, IOptions<StripeSettings> stripeSettings)
        {
            _cartService = cartService;
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var cart =await _cartService.GetCartAsync();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartItem cartItem)
        {
            var cart = await _cartService.GetCartAsync();
            var changedCart=await _cartService.AddItemAsync(cart,cartItem);
            await _cartService.SaveCartAsync(changedCart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int gameId)
        {
            var cart =await _cartService.GetCartAsync();
            var changedCart=await _cartService.RemoveItemAsync(cart,gameId);
            await _cartService.SaveCartAsync(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var cart =await _cartService.GetCartAsync();
            var domain = $"{this.Request.Scheme}://{this.Request.Host}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
            {
                "card",
            },
                LineItems = cart.items.Select( item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount =  _cartService.GetTotalPriceAsync(cart).Result, // Stripe requires amount in cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.GameName,
                        },
                    },
                }).ToList(),
                Mode = "payment",
                SuccessUrl = $"{domain}/Cart/Success",
                CancelUrl = $"{domain}/Cart/Cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return Redirect(session.Url);
        }

        public async Task<IActionResult> SuccessAsync()
        {
            var userId=int.Parse(User.Claims.First().Value);
            var cart=await _cartService.GetCartAsync();
            await _cartService.AddCartDbAsync(userId);
            await _cartService.Clear(cart);
            await _cartService.SaveCartAsync(new Cart());
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
