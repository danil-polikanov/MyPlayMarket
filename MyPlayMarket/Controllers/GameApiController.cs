using Microsoft.AspNetCore.Mvc;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Entities;

namespace MyPlayMarket.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GameApiController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IApiService _apiService;
        private readonly ILogger<GameApiController> _logger;

        public GameApiController(IGameService gameService, IApiService apiService, ILogger<GameApiController> logger)
        {
            _gameService = gameService;
            _apiService = apiService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            List<string> games = null;
            return Ok(games);
        }

        public async Task<IActionResult> Import()
        {
            _logger.LogInformation("Import games started");
            await _apiService.ImportGamesFromApiAsync();
            return Ok();
        }

        //[HttpGet("genres")]
        //public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        //{
        //    var genres = await _gameService.GetAllGenresAsync();
        //    return Ok(genres);
        //}
    }
}
