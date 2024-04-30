using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace MyPlayMarket.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;
        public IEnumerable<Game> Games { get; set; }

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Game> games = await _db.Games.ToListAsync();
            return View(games);
        }



    }
}
