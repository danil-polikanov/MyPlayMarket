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
    public class UserController:Controller
    {
        private readonly IGameService _gameService;
        private readonly ISortingService _sortingService;
        private readonly IFilteringService _filteringService;
        private readonly IPaginationService _paginationService;
        private readonly IDataService _dataService;
        private readonly ILogger<UserController> _logger;
        public IEnumerable<Game> Games { get; set; }
        public PageViewDTO ViewModel { get; set; }
        public UserController(IGameService gameService, ISortingService sortingService, IFilteringService filteringService, IPaginationService paginationService, IDataService dataService, ILogger<UserController> logger)
        {
            _gameService = gameService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _paginationService = paginationService;
            _dataService = dataService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult> Login(UserRegisterDTO user)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else { return BadRequest(); }
        }
        [HttpGet]
        public async Task<ActionResult> Register(UserRegisterDTO user)
        {

            await 
            if (ModelState.IsValidl)
            {
                return View(SortedGames);
            }
            else { return BadRequest(); }
        }
    }
}

