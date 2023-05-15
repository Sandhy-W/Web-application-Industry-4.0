using Industry_4._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Industry_4._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Comor()
        {
            return View();
        }

        public IActionResult Emtech ()
        {
            return View();
        }

        public IActionResult Conab ()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}