using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Services;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Entities.DTO;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;
using System.Xml.Linq;

namespace MyPlayMarket.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ISortingService _sortingService;
        private readonly IFilteringService _filteringService;
        private readonly IPaginationService _paginationService;
        private readonly IDataService _dataService;
        private readonly ILogger<GamesController> _logger;
        public IEnumerable<Game> Games { get; set; }
        public PageViewDTO ViewModel { get; set; }

        public GamesController(IGameService gameService, ISortingService sortingService, IFilteringService filteringService, IPaginationService paginationService, IDataService dataService, ILogger<GamesController> logger)
        {
            _gameService = gameService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _paginationService = paginationService;
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Index(IndexPaggingDTO indexPagging)
        {
            _logger.LogInformation("Index action called with parameters: {@IndexPagging}", indexPagging);
            var sortedGames = await _dataService.GetGamesAsync<Game>(indexPagging);
            if (ModelState.IsValid || indexPagging.Games == null)
            {
                _logger.LogInformation("Index action succeeded.");
                return View(sortedGames);
            }
            else
            {
                _logger.LogWarning("Index action failed due to invalid model state.");
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Details action called with id: {Id}", id);

            Game game = await _gameService.GetGameAsync(id);
            if (game != null)
            {
                _logger.LogInformation("Details action succeeded for id: {Id}", id);
                return View(game);
            }
            else
            {
                _logger.LogWarning("Details action failed. Game not found for id: {Id}", id);
                return NotFound();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            _logger.LogInformation("Create GET action called.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateGameDTO createdGame)
        {
            _logger.LogInformation("Create POST action called with game: {@Game}", createdGame);

            if (createdGame != null && ModelState.IsValid)
            {
                await _gameService.CreateGameAsync(createdGame);
                _logger.LogInformation("Create action succeeded. Game created: {@Game}", createdGame);
                ViewBag.Message = "Data Insert Successfully";
            }
            else
            {
                _logger.LogWarning("Create action failed. Model state invalid or game is null.");
                ViewBag.Message = "Data Insert Error";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Edit GET action called with id: {Id}", id);

            Game game = await _gameService.GetGameAsync(id);
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Game updatedGame)
        {
            _logger.LogInformation("Edit POST action called with id: {Id} and updated game: {@UpdatedGame}", id, updatedGame);

            if (ModelState.IsValid)
            {
                var gameFromDb = await _gameService.GetGameAsync(id);
                gameFromDb = updatedGame;
                await _gameService.UpdateGameAsync(gameFromDb);
                _logger.LogInformation("Edit action succeeded. Game updated: {@UpdatedGame}", updatedGame);
                ViewBag.Message = "Data update Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogWarning("Edit action failed due to invalid model state.");
                ViewBag.Message = "Data update Failed";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation("Delete GET action called with id: {Id}", id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Game game)
        {
            _logger.LogInformation("Delete POST action called with id: {Id} and game: {@Game}", id, game);

            if (ModelState.IsValid)
            {
                await _gameService.DeleteGameAsync(id);
                _logger.LogInformation("Delete action succeeded for id: {Id}", id);
                ViewBag.Message = "Data delete Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogWarning("Delete action failed due to invalid model state.");
                ViewBag.Message = "Data delete Failed";
                return View();
            }
        }
    }
}
