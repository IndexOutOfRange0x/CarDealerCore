using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarDealerCore.Models;

namespace CarDealerCore.Controllers
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
        
        public IActionResult Car()
        {
            return View();
        }
    
        public IActionResult MySales()
        {
            return View();
        }
        
        public IActionResult AllSales()
        {
            return View();
        }
    
        public IActionResult UserPage()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddCar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Buy()
        {
            return RedirectToAction("MySales");
        }
        
        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["statusCode"] = HttpContext.Request.Query["code"];
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
