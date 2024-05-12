using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPlayMarket.Core.Services;
using MyPlayMarket.Infrastructure.Entities;
using System.Collections;
using System.Xml.Linq;

namespace MyPlayMarket.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        public IEnumerable<Game> Games { get; set; }
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable games = await _gameService.GetGamesAsync();
            return View(games);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            Game game=await _gameService.GetGameAsync(id);
            return View(game);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Game game)
        {
            if (game != null)
            {
                await _gameService.CreateGameAsync(game);
                ViewBag.Message = "Data Insert Successfully";
            }
            else
            {
                ViewBag.Message = "Data Insert Error";
            }
            return View();

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Game updatedGame)
        {
            try
            {
                var GameFromDb = await _gameService.GetGameAsync(id);
                GameFromDb = updatedGame;
                await _gameService.UpdateGameAsync(GameFromDb);
                ViewBag.Message = "Data update Successfully";
                return RedirectToAction(nameof(Index));              
            }
            catch
            {
                ViewBag.Message = "Data update Failed";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Game game)
        {
            try
            {
                await _gameService.DeleteGameAsync(id);
                ViewBag.Message = "Data delete Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = "Data delete Successfully";
                return View();
            }
        }
    }
}
