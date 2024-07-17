using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Services;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;
using System.Collections;
using System.Globalization;
using System.Xml.Linq;
using System.IdentityModel.Tokens.Jwt;
using NuGet.Common;

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
        private readonly IUserService  _userService;
        public IEnumerable<Game> Games { get; set; }
        public PageViewDTO ViewModel { get; set; }
        public UserController(
            IGameService gameService, 
            ISortingService sortingService, 
            IFilteringService filteringService, 
            IPaginationService paginationService, 
            IDataService dataService,
            IUserService userService,
            ILogger<UserController> logger)
        {
            _gameService = gameService;
            _sortingService = sortingService;
            _filteringService = filteringService;
            _paginationService = paginationService;
            _dataService = dataService;
            _userService = userService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var user = new LoginUserDTO("admin", "admin");
            var token=await _userService.Login(user);
            _logger.LogInformation($"User with {user.Email} tried login called");
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"User with {user.Email} successfully loginned");
                //return token;
                var context = this.HttpContext;
                context.Response.Cookies.Append("tasty-cookies", token);
                return Ok(token);
            }
            else
            {
                _logger.LogError($"User with {user.Email} was failing to log in");
                return Ok(token);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var user = new UserRegisterDTO("admin", "admin", "admin", "admin", "admin");
            await _userService.Register(user);
            if (ModelState.IsValid)
            {
                return Ok(user);
            }
            else { return BadRequest(); }
        }
    }
}

