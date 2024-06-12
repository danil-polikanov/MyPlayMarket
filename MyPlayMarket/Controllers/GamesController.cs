using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Services;
using MyPlayMarket.Infrastructure.Entities;
using MyPlayMarket.Infrastructure.Entities.DTO;
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
        public IEnumerable<Game> Games { get; set; }
        public PageViewDTO ViewModel { get; set; }
        public GamesController(IGameService gameService, ISortingService sortingService, IFilteringService filteringService, IPaginationService paginationService, IDataService dataService)
        {
            _gameService = gameService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _paginationService = paginationService;
            _dataService = dataService;
        }
        [HttpGet]
        public async Task<ActionResult> Index(IndexPaggingDTO indexPagging)
        {

            var SortedGames = await _dataService.GetGamesAsync<Game>(indexPagging);
            if (ModelState.IsValid||indexPagging.Games == null)
            {
                return View(SortedGames);
            }
            else { return BadRequest(); }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            Game game = await _gameService.GetGameAsync(id);
            if (game!=null)
            {
                return View(game);
            }
            else return NotFound();
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
            if (game != null && ModelState.IsValid)
            {
                await _gameService.CreateGameAsync(game);
                ViewBag.Message = "Data Insert Successfully";
            }
            else
            {
                ViewBag.Message = "Data Insert Error";
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            Game game = await _gameService.GetGameAsync(id);
            return View(game);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Game updatedGame)
        {
            if (ModelState.IsValid)
            {
                var GameFromDb = await _gameService.GetGameAsync(id);
                GameFromDb = updatedGame;
                await _gameService.UpdateGameAsync(GameFromDb);
                ViewBag.Message = "Data update Successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = "Data update Failed";
            return View();
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
            if (ModelState.IsValid)
            {
                await _gameService.DeleteGameAsync(id);
                ViewBag.Message = "Data delete Successfully";
                return RedirectToAction(nameof(Index));
            }
            else ViewBag.Message = "Data delete Successfully";
            return View();

        }
    }
}
