using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Services;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(IndexPaggingDTO indexPagging)
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
        public async Task<IActionResult> Details(int id)
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

        [Authorize("AdminPolicy")]
        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("Create GET action called.");
            return View();
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateGameDTO createdGame)
        {
            _logger.LogInformation("Create POST action called with game: {@Game}", createdGame);

            if (createdGame != null && ModelState.IsValid)
            {
                if (await _gameService.CreateGameAsync(createdGame))
                {
                    _logger.LogInformation("Create action succeeded. Game created: {@Game}", createdGame);
                    SetTempDataMessage("Game created successfully!", "success");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("Create action failed. Model state invalid or game is null.");
                    SetTempDataMessage("Game already exists with this name or invalid data!", "error");
                    return View(createdGame);
                }
            }
            else
            {
                _logger.LogWarning("Create action failed. Model state invalid or game is null.");
                SetTempDataMessage("Game already exists with this name or invalid data!", "error");
                return View(createdGame); ;
            }
        }

        [Authorize("AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Edit GET action called with id: {Id}", id);

            Game game = await _gameService.GetGameAsync(id);
            var createGameDTO = new CreateUpdateGameDTO
            {
                Game = game,
                GenresDTO = game.GameGenres.Select(x => x.Genre.Name).ToList(),
                PlatformsDTO = game.GamePlatforms.Select(x => x.Platform.Name).ToList(),
                TagsDTO = game.GameTags.Select(x => x.Tag.Name).ToList(),
                ScreenshotsDTO = game.Screenshots.Select(x => x.Url).ToList()
            };
            return View(createGameDTO);
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateGameDTO updatedGame)
        {
            _logger.LogInformation("Edit POST action called with id: {Id} and updated game: {@UpdatedGame}", id, updatedGame);

            if (ModelState.IsValid)
            {
                if (await _gameService.UpdateGameAsync(updatedGame))
                {
                    _logger.LogInformation("Edit action succeeded. Game updated: {@UpdatedGame}", updatedGame);
                    SetTempDataMessage("Game updated successfully!", "success");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("Edit action failed. Model state invalid or game is null.");
                    SetTempDataMessage("Game already exists with this name or invalid data!", "error");
                    return View(updatedGame);
                }
            }
            else
            {
                _logger.LogWarning("Edit action failed. Model state invalid or game is null.");
                SetTempDataMessage("Game already exists with this name or invalid data!", "error");
                return View(updatedGame);
            }
        }

        [Authorize("AdminPolicy")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Delete GET action called with id: {Id}", id);
            return View();
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Game game)
        {
            _logger.LogInformation("Delete POST action called with id: {Id} and game: {@Game}", id, game);

            try
            {
                if (await _gameService.DeleteGameAsync(id))
                {
                    _logger.LogInformation("Delete action succeeded for id: {Id}", id);
                    SetTempDataMessage("Game deleted successfully!", "success");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                _logger.LogWarning("Delete action failed due to an exception.");
                SetTempDataMessage("Game wasn't deleted", "error");
                return RedirectToAction(nameof(Index));
            }
        }

        private void SetTempDataMessage(string message, string messageType)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = messageType;
        }
    }
}
