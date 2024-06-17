using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using MyPlayMarket.Core.IServices;

namespace MyPlayMarket.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ILogger<HomeController> _logger;
        public IEnumerable<Game> Games { get; set; }
        public HomeController(IGameService gameService, ILogger<HomeController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var games = await _gameService.GetGamesByQueryAsync();
            return View(games);
        }
    }
}
