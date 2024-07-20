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
using Microsoft.AspNetCore.Authorization;
using MyPlayMarket.Controllers;

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
        public IActionResult Login()
        {
            _logger.LogInformation("Create Login action called.");
            return View();
        }
        [HttpPost] 
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {;
            var token=await _userService.Login(loginUser);
            _logger.LogInformation($"User with {loginUser.Email} tried login called");
            if (ModelState.IsValid&&token!=null)
            {
                _logger.LogInformation($"User with {loginUser.Email} successfully loginned");
                var context = this.HttpContext;
                context.Response.Cookies.Append("tasty-cookies", token);
                SetTempDataMessage("You loginned successfully!", "success");
                return RedirectToAction(nameof(Index), nameof(HomeController));
            }
            else
            {
                _logger.LogError($"User with {loginUser.Email} was failing to log in");
                SetTempDataMessage(token, "error");
                return Redirect(nameof(Login));
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation("Create Login action called.");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO registerDTO)
        {
            var message = await _userService.Register(registerDTO);
            if (ModelState.IsValid&& message != null)
            {
                SetTempDataMessage("You registered successfully!", "success");
                return RedirectToAction(nameof(Index), nameof(HomeController));
            }
            else {
                SetTempDataMessage(message, "error");
                return Redirect(nameof(Register));
            }
        }
        private void SetTempDataMessage(string message, string messageType)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = messageType;
        }
    }
}

