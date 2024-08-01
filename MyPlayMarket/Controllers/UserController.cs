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
using Microsoft.AspNetCore.Authentication;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Humanizer;
using NuGet.Protocol.Plugins;

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
        public async Task<IActionResult> Login(LoginRegisterDTO login)
        {          
            var token=await _userService.Login(login.loginUserDTO);
            _logger.LogInformation($"User with {login.loginUserDTO.Email} tried login called");
            if(token!=null)
            {
                _logger.LogInformation($"User with {login.loginUserDTO.Email} successfully loginned");
                var context = this.HttpContext;
                context.Response.Cookies.Append("tasty-cookies", token);
                SetTempDataMessage("You loginned successfully!", "success");
                return RedirectToAction(nameof(Index), "Home");
            } 
            else
            {
                _logger.LogError($"User with {login.loginUserDTO.Email} was failing to log in");
                SetTempDataMessage("Failed to login, user doesn't exist or invalid password", "error");
                return Redirect(nameof(Login));
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation("Create Register action called.");
            return View(nameof(Login));
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterDTO register)
        {
            var message = await _userService.Register(register.registerDTO);
            if (message == null)
            {
                var token=await _userService.Login(new LoginUserDTO {Email=register.registerDTO.Email, Password=register.registerDTO.Password });
                var context = this.HttpContext;
                context.Response.Cookies.Append("tasty-cookies", token);
                SetTempDataMessage("You registered successfully!", "success");
                return RedirectToAction(nameof(Index), "Home");
            }
            else {
                _logger.LogError($"User with {register.registerDTO.Email} was failing to register");
                SetTempDataMessage("Failed to login, user doesn't exist or invalid password", "error");
                return Redirect(nameof(Login));
            }
        } 
        private void SetTempDataMessage(string message, string messageType)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = messageType;
        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            var context = this.HttpContext; 
            context.Response.Cookies.Delete("tasty-cookies");
            _logger.LogInformation("LogOut action called.");
            await Task.CompletedTask;
            SetTempDataMessage("Log out successfully!", "success");
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}

