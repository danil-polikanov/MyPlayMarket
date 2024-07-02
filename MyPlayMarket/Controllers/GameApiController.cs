using Microsoft.AspNetCore.Mvc;
using MyPlayMarket.Core.IServices;
using MyPlayMarket.Infrastructure.Entities;

namespace MyPlayMarket.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GameApiController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IApiService _apiService;

        public GameApiController(IGameService gameService, IApiService apiService)
        {
            _gameService = gameService;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            List<string> games = null;
            return Ok(games);
        }

        public async Task<IActionResult> ImportGames()
        {
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
