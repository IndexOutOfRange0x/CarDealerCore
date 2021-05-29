using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarDealerCore.Models;
using CarDealerCore.Data;
using Microsoft.EntityFrameworkCore;

namespace CarDealerCore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _db = context;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.Cars = await _db.Cars.Where(X => X.IsSold == false).ToListAsync();
            return View();
        }
      
        public IActionResult Privacy()
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
