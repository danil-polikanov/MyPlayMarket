﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPlayMarket.Core.IServices;
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
        private readonly ISortingService _sortingService;
        private readonly IFilteringService _filteringService;
        private readonly IPaginationService _paginationService;
        public IEnumerable<Game> Games { get; set; }
        public PageViewModel ViewModel { get; set; }
        public GamesController(IGameService gameService,ISortingService sortingService,IFilteringService filteringService,IPaginationService paginationService)
        {
            _gameService = gameService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _paginationService = paginationService;
        }
        [HttpGet]
        public async Task<ActionResult> Index(int currentPage=1,int pageSize=25,string SortBy="")
        {
            var SortedGames = await _sortingService.GetSortProperty(SortBy);
            var indexPagging = await _gameService.GetGamesByPagging(currentPage,pageSize, (List<Game>)SortedGames);
            return View(indexPagging);
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
