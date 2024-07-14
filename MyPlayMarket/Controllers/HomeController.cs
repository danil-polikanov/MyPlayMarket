using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Infrastructure;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core;
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
            _logger.LogInformation("Main view called");
            var games = await _gameService.GetGamesByQueryAsync();
            return View(games);
        }
    }
}
