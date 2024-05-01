using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Core.Services;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections;

namespace MyPlayMarket.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        public IEnumerable<Game> Games { get; set; }
        public HomeController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable games = await _gameService.GetGamesAsync();
            return View(games);
        }



    }
}
