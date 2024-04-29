using Microsoft.AspNetCore.Mvc;

namespace MyPlayMarket.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
